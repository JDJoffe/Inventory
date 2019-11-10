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
    public List<Item> inv = new List<Item>();
    public List<Item> armour = new List<Item>();
    public List<Item> weapons = new List<Item>();
    public List<Item> quest = new List<Item>();
    public int slotX, slotY;
    Vector2 scr;
    [Header("Dragging")]

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
        for (int i = 0; i < 15; i++)
        {
            invSlots.Add(invparent.GetComponentsInChildren<Image>()[i]);
        }
        // remove first in list because it is dumb and gets the parent's image
        invSlots.RemoveAt(0);
        for (int i = 0; i < 8; i++)
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
                inv[i] = ItemData.CreateItem(itemID);
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
    #region abomination of a bool that came from me trying to understand the alien code
    public bool EventImgMatchSlotImg(int i)
    {
        // make mouse event that is == to current event
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        // make event position to be the mouse position
        eventData.position = Input.mousePosition;
        // make list of raycasts that will store this  eventdata
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        // return a raycast of the eventdata and the list
        EventSystem.current.RaycastAll(eventData, raysastResults);
        // for every entry in the list cycle through
        for ( i = 0; i < raysastResults.Count; i++)
        {
            // if the raycasted object in the list has the same image as the invslot then they are the same object
            if (raysastResults[i].gameObject.GetComponent<Image>() == invSlots[i])
            {
                // ditch
                return true;
            }
        }
        // return false cause this code wont be reached if it returns true
        return false;
    }
    #endregion
    #region itemDrag
    private void ItemDrag()
    {
        showToolTip = false;

        int itm = 0;
        PointerEventData e = new PointerEventData(EventSystem.current);

        for (int i = 0; i < invSlots.Count; i++)
        {
          
            GameObject slot = invSlots[i].gameObject;
            Rect bounds = invSlots[i].rectTransform.rect;
            bounds.position = invSlots[i].transform.position;
            slot = e.pointerPress;
            //if (bounds.Contains(slot.transform.position))
            //{
            //    Debug.Log(i);
            //}

            if (e.button == 0 /*&& e.pointerPress == slot*/ && EventImgMatchSlotImg(i) /*&&*//*slotLocal[i].Contains(e.mousePosition) && *//*!isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift)*/)
            {
                Debug.Log(i + " " + invSlots[i].transform.position + "\n" + e.pointerPress);
            }
            else { return; }
            itm++;


        }

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

