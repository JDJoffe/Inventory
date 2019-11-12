using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HoverInv : MonoBehaviour
{
    #region var
    [Header("Inventory")]
    public bool toggleInv = false;
    KeyCode toggle = KeyCode.Tab;
    public List<CanvasItem> inv = new List<CanvasItem>();
    public List<CanvasItem> armour = new List<CanvasItem>();
    public List<CanvasItem> weapons = new List<CanvasItem>();
    public List<CanvasItem> quest = new List<CanvasItem>();
    public int slotX, slotY;
    Vector2 scr;
    [Header("Dragging")]
    public int imageFilter = 0;
    public bool isDragging;
    public int draggedFrom;
    public Item draggedItem;
    public bool isEquippable;
    public GameObject droppedItem;
    [Header("ToolTip")]
    public int toolTipItem;
    public bool showToolTip;
    KeyCode toggleTip = KeyCode.Semicolon;
    public Rect toolTipRect;
    public GameObject toolTipPanel;
    [Header("Slot lists")]
    #region slots
    GameObject invparent, questparent;
    GameObject[] weaponparent = new GameObject[3], armourparent = new GameObject[4];
    public List<Image> invSlots = new List<Image>();
    public List<Image> questSlots = new List<Image>();
    public List<Image> armourSlots = new List<Image>();
    public List<Image> weaponSlots = new List<Image>();
    #endregion
    [Header("References and Locations")]
    public GameObject inventory;
    Text itemName, itemDesc, itemAmount, itemValue;
    int misc;
    int rand;
    string EquipSlotName;
    public Transform dropLocation;
    // armour
    public Transform head;
    public Transform chest;
    public Transform arms;
    public Transform Legs;
    // weapons
    public Transform[] hands = new Transform[1];
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        inv.Add(CanvasItemData.CreateItem(1));
        #region find parents
        // inv
        invparent = GameObject.Find("BackPack");
        // quest
        questparent = GameObject.Find("QuestItem");
        // weapons
        weaponparent[0] = GameObject.Find("Weapon1img");
        weaponparent[1] = GameObject.Find("Weapon2img");
        weaponparent[2] = GameObject.Find("Relicimg");
        // equip
        armourparent[0] = GameObject.Find("headimg");
        armourparent[1] = GameObject.Find("chestimg");
        armourparent[2] = GameObject.Find("armsimg");
        armourparent[3] = GameObject.Find("legsimg");
        #endregion
        #region add parent children to list of images
        // search and get images
        for (int i = 0; i < invparent.transform.childCount + 1; i++)
        {
            invSlots.Add(invparent.GetComponentsInChildren<Image>()[i]);
        }
        // remove first in list because it is dumb and gets the parent's image
        invSlots.RemoveAt(0);
        for (int i = 0; i < questparent.transform.childCount + 1; i++)
        {
            questSlots.Add(questparent.GetComponentsInChildren<Image>()[i]);
        }
        // remove first in list because it is dumb and gets the parent's image
        questSlots.RemoveAt(0);
        for (int i = 0; i < 4; i++)
        {
            // one parent per image so yeah
            armourSlots.Add(armourparent[i].GetComponentInChildren<Image>());
        }
        for (int i = 0; i < 3; i++)
        {
            // one parent per image so yeah
            weaponSlots.Add(weaponparent[i].GetComponentInChildren<Image>());
        }
        #endregion
        #region tooltip text

        itemName = GameObject.Find("NameTxt").GetComponent<Text>();
        itemDesc = GameObject.Find("DescTxt").GetComponent<Text>();
        itemAmount = GameObject.Find("AmountTxt").GetComponent<Text>();
        itemValue = GameObject.Find("ValueTxt").GetComponent<Text>();
        toolTipPanel.SetActive(false);
        #endregion
        #region addItems
       // for (int i = 0; i < invparent.transform.childCount; i++)
       // {
            inv.Add(CanvasItemData.CreateItem(0));
        inv.Add(CanvasItemData.CreateItem(2));
        inv.Add(CanvasItemData.CreateItem(3));
        inv.Add(CanvasItemData.CreateItem(4));

        invSlots[1].sprite = inv[1].Icon;
          //invSlots[i] = inv[i].Icon;
      //  }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if (showToolTip)
        {
            toolTipPanel.SetActive(true);
        }
        else { toolTipPanel.SetActive(false); }
        if (Input.GetKeyDown(toggle))
        {
            ToggleInv();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddItem(rand = Random.Range(0, 30));
        }
        ItemDrag();
    }
    #region toggle inv
    public void ToggleInv()
    {
        toggleInv = !toggleInv;
        if (toggleInv)
        {
            inventory.SetActive(true);
            // freeze time and allow mouse movement
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            toolTipPanel.SetActive(false);
            inventory.SetActive(false);

            // unfreeze time and lock mouse
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #endregion
    #region add item
    // function to add items to the list and create an item in the inventory
    public void AddItem(int itemID)
    {
        // for i to the size of the inventory
        for (int i = 0; i < inv.Count; i++)
        {
            // if the slot has space create an item there
            if (inv[i].Name == null)
            {
                inv[i] = CanvasItemData.CreateItem(itemID);
                Debug.Log("Added Item: " + inv[i].Name);
                return;
            }
        }
    }
    #endregion
    #region code that i copied and tried to understand
    //public static bool IsPointerOverUIElement()
    //{
    //    return IsPointerOverUIElement(GetEventSystemRaycastResults());
    //}
    /////Returns 'true' if we touched or hovering on Unity UI element.
    //public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    //{
    //    for (int index = 0; index < eventSystemRaysastResults.Count; index++)
    //    {
    //        RaycastResult curRaysastResult = eventSystemRaysastResults[index];
    //        if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
    //            return true;
    //    }
    //    return false;
    //}
    /////Gets all event systen raycast results of current mouse or touch position.
    //static List<RaycastResult> GetEventSystemRaycastResults()
    //{
    //    PointerEventData eventData = new PointerEventData(EventSystem.current);
    //    eventData.position = Input.mousePosition;
    //    List<RaycastResult> raysastResults = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, raysastResults);
    //    return raysastResults;
    //}
    #endregion
    #region abominations that came from me trying to understand the alien code
    public bool EventImgMatchSlotImg(List<RaycastResult> rayResult)
    {
        // int i = invSlots.Count + weaponSlots.Count + armourSlots.Count + questSlots.Count;
        int j = 0;
        // for every entry in the list cycle through
        // for (int j = 0; j < i; j++)
        // {
        // if the raycasted object in the list has the same image as the invslot then they are the same object
        if ( rayResult[j].gameObject.GetComponent<Image>() == invSlots[j] || weaponSlots[j] || armourSlots[j] || questSlots[j])
        {
            // imageFilter = invSlots.IndexOf(invSlots[j]);                           
            Debug.Log(j + " im so confused");
            // ditch
            return true;
        }
        //  }
        // return false cause this code wont be reached if it returns true
        return false;
    }
    public List<RaycastResult> ImgHover()
    {

        // make mouse event that is == to current event
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        // make event position to be the mouse position
        eventData.position = Input.mousePosition;
        // make list of raycasts that will store this  eventdata
        List<RaycastResult> raysastResults = new List<RaycastResult>();

        // return a raycast of the eventdata and the list
        EventSystem.current.RaycastAll(eventData, raysastResults);
        foreach (var item in raysastResults)
        {
            // if (item.isValid)
            //{
            if (item.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                int i = 0;
                imageFilter = raysastResults.IndexOf(item);
                Debug.Log(imageFilter);
                if (imageFilter == 3)
                {

                   
                    // insert tooltip item info here and shit
                    showToolTip = true;
                    itemName.text = inv[0].Name;
                    itemAmount.text = inv[0].Amount.ToString();
                    itemDesc.text = inv[0].Desctiption;
                    itemValue.text = inv[0].Value.ToString();
                    Vector2 tooltipPos = Input.mousePosition;
                    Vector2 mouseOffset = new Vector3(140, 0);
                    tooltipPos += mouseOffset;
                    toolTipPanel.transform.position = tooltipPos;
                }
            }

            //}           
        }

        return raysastResults;
    }
    public bool ImgHoverTrue()
    {
        return EventImgMatchSlotImg(ImgHover());
    }
    #endregion
    #region itemDrag
    private void ItemDrag()
    {
        showToolTip = false;
        int i = 0;
        ImgHover();
        PointerEventData e = new PointerEventData(EventSystem.current);

        GameObject slot = e.selectedObject;

        // drag
        if (e.button == 0 /*&& e.pointerPress == slot*/ && Input.GetMouseButtonDown(0) && ImgHoverTrue()/* && imageFilter == 3*/ && !isDragging /*&& inv[i].Name != null*/)
        {
            Debug.Log("BackPackSlot " + imageFilter);
            // draggedItem = inv[i];
            //  inv[i] = new Item();
            // isDragging = true;
            isEquippable = false;
            draggedFrom = i;
            i++;
        }
        else { return; }
        // swap
        if (e.button == 0 /*&& e.pointerPress == slot*/ && Input.GetMouseButtonUp(0) && ImgHoverTrue() && imageFilter == 3 && !isDragging /*&& inv[i].Name != null*/)
        {
            Debug.Log("BackPackSlot " + imageFilter);
            Debug.Log("Swapped your items " + draggedItem.Name + "&" + inv[i].Name);
            //  inv[draggedFrom] = inv[i];
            //inv[i] = draggedItem;
            //draggedItem = new Item();
            isEquippable = false;
            isDragging = false;
            i++;
        }
        // place item
        if (e.button == 0 /*&& e.pointerPress == slot*/ && Input.GetMouseButtonUp(0) && ImgHoverTrue() && imageFilter == 3 && isDragging /*&& inv[i].Name == null*/)
        {
            Debug.Log("BackPackSlot " + imageFilter);

            //inv[i] = draggedItem;
            draggedItem = new Item();
            isEquippable = false;
            isDragging = false;
            i++;
        }
        // render image
        //#region draw item icon
        //if (inv[i].Name != null)
        //{
        //   // slot.transform. = inv[i].Icon;
        //  //  GUI.DrawTexture(s, inv[i].Icon);
        //    #region set tooltip mousehover
        //    if (ImgHoverTrue() && imageFilter == 3&&!isDragging && toggleInv)
        //    {
        //        toolTipItem = i;
        //        showToolTip = true;
        //    }
        //    #endregion
        //}
        //#endregion



    }
    #endregion
    //#region drag drop
    //private void OnMouseDrag()
    //{
    //    showToolTip = false;
    //    #region grid nested for loops
    //    // 
    //    int i = 0;
    //    Event e = Event.current;
    //    for (int y = 0; y < slotY; y++)
    //    {
    //        for (int x = 0; x < slotX; x++)
    //        {
    //            Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
    //            GUI.Box(slotLocation, "");
    //            #region pickup item
    //            // 
    //            if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
    //            {
    //                draggedItem = inv[i];
    //                inv[i] = new Item();
    //                isDragging = true;
    //                isEquippable = false;
    //                draggedFrom = i;
    //                Debug.Log("Currently dragging your " + draggedItem.Name);
    //            }
    //            #endregion
    //        }
    //    }
    //    #endregion
    //}
    //#endregion
}

