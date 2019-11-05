using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    #region var
    [Header("Inventory")]
    public bool toggleInv = false;
    KeyCode toggle = KeyCode.Tab;
    public List<Item> inv = new List<Item>();
    public List<Item> equip = new List<Item>();
    public int slotX, slotY;
    public Rect invSize;
    public Rect equipSize;
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
    [Header("References and Locations")]
    int misc;
    int rand;
    string EquipSlotName;
    #endregion
    #region Clamp to screen
    //drag inventory around screen but not outside it min is 0 and max is screen max - r
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
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
    //#region remove item
    // remove the most recently randomly added item
    //public void RemoveItem(int itemID)
    //{
    //    for (int i = 0; i < inv.Count; i++)
    //    {
    //        if (inv[i].Name != null)
    //        {
    //            inv.Remove(inv[i]);
    //            Debug.Log("Removed Item: " + inv[i].Name);
    //            return;
    //        }
    //    }
    //}
    //#endregion
    #region drop item
    // drop selected item and instantiate it and reset the var
    public void DropItem()
    {
        droppedItem = draggedItem.ItemMesh;
        droppedItem = Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        droppedItem.name = draggedItem.Name;
        droppedItem = null;
    }
    #endregion
    #region draw item
    // different inv windows have different ids so they dont open at the same time
    // drawtexture of a smaller item under the mouse so you know you are dragging an item
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * .5f, scr.y * .5f), draggedItem.Icon);
        }
    }
    #endregion
    #region tooltip
    // gui box with item info inside it
    #region tooltipcontent
    // info of the item as we are usually just displaying the item icon
    private string toolTipText(int index)
    {
        // strings of item name, desc and value
        // \n to mov the rest of the string to a new line
        string toolTipTxt = inv[index].Name + "\n" + inv[index].Desctiption + "\nValue: " + inv[index].Value;
        return toolTipTxt;
    }
    #endregion
    #region tooltipwindow
    // draw a box to put the string (above) into
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scr.x * 6, scr.y * 2), toolTipText(toolTipItem));
    }
    #endregion
    #endregion
    #region toggle inv
    // toggle inventory
    public void ToggleInv()
    {
        toggleInv = !toggleInv;
        if (toggleInv)
        {
            // freeze time and allow mouse movement
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // unfreeze time and lock mouse
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #endregion
    #region drag inv
    // dragable inventory gui box
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, scr.y * .25f, scr.x * 6, scr.y * .5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4.25f, scr.x * 6, scr.y * .5f), "Gold Display");
        showToolTip = false;
        #region grid nested for loops
        // 
        int i = 0;
        Event e = Event.current;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                #region pickup item
                // 
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {                 
                        draggedItem = inv[i];
                        inv[i] = new Item();
                        isDragging = true;
                        isEquippable = false;
                        draggedFrom = i;
                        Debug.Log("Currently dragging your " + draggedItem.Name);                  
                }
                #endregion
                #region swap item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name != null)
                {
                    Debug.Log("Swapped your items " + draggedItem.Name + "&" + inv[i].Name);
                    inv[draggedFrom] = inv[i];
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isEquippable = false;
                    isDragging = false;
                }
                #endregion
                #region place item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name == null)
                {
                    Debug.Log("Placed your item " + draggedItem.Name + " in slot " + "X:" + x + " Y:" + y);
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isEquippable = false;
                    isDragging = false;
                }
                #endregion

                #region return item
                if (toggleInv)
                {

                }
                #endregion
                #region draw item icon
                if (inv[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inv[i].Icon);
                    #region set tooltip mousehover
                    if (slotLocation.Contains(e.mousePosition) && !isDragging && toggleInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region drag points
        // top
        GUI.DragWindow(new Rect(0, 0, scr.x * 6, scr.y * 0.25f));
        // left
        GUI.DragWindow(new Rect(0, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        // right
        GUI.DragWindow(new Rect(scr.x * 5.5f, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        // bottom
        GUI.DragWindow(new Rect(0, scr.y * 4, scr.x * 6, scr.y * 0.25f));
        #endregion
    }
    #endregion
    #region drag equip
    void EquipDrag(int windowID)
    {
        showToolTip = false;
        #region grid nested for loops
        // 
        int i = 0;
        Event e = Event.current;
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < 1; x++)
            {
                
                if (y == 0)
                {
                    EquipSlotName = "Apparrel";
                }
                if (y == 1)
                {
                    EquipSlotName = "Weapon";
                }
                Rect equipSlotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);


                GUI.Box(equipSlotLocation, EquipSlotName);
               
                #region pickup item
                // 
                if (e.button == 0 && e.type == EventType.MouseDown && equipSlotLocation.Contains(e.mousePosition) && !isDragging && equip[i].Name != null && !Input.GetKey(KeyCode.LeftShift) /*&& equip[i].Type.ToString() == EquipSlotName*/ )
                {
                    draggedItem = equip[i];
                    equip[i] = new Item();
                    isDragging = true;
                    isEquippable = false;
                    draggedFrom = i;
                    Debug.Log("Currently dragging your " + draggedItem.Name);
                }
                #endregion
                #region swap item
                if (e.button == 0 && e.type == EventType.MouseUp && equipSlotLocation.Contains(e.mousePosition) && isDragging && equip[i].Name != null /*&& inv[i].Type.ToString() == EquipSlotName*/)
                {
                    Debug.Log("Swapped your items " + draggedItem.Name + "&" + equip[i].Name);
                    equip[draggedFrom] = equip[i];
                    equip[i] = draggedItem;
                    draggedItem = new Item();
                    isEquippable = false;
                    isDragging = false;
                }
                #endregion
                #region place item
                if (e.button == 0 && e.type == EventType.MouseUp && equipSlotLocation.Contains(e.mousePosition) && isDragging && equip[i].Name == null /*&& inv[i].Type.ToString() == EquipSlotName*/)
                {
                    Debug.Log("Placed your item " + draggedItem.Name + " in slot " + "X:" + x + " Y:" + y);
                    equip[i] = draggedItem;
                    draggedItem = new Item();
                    isEquippable = false;
                    isDragging = false;
                }
                #endregion

                #region return item
                if (toggleInv)
                {

                }
                #endregion
                #region draw item icon
                if (equip[i].Name != null)
                {
                    GUI.DrawTexture(equipSlotLocation, equip[i].Icon);
                    #region set tooltip mousehover
                    if (equipSlotLocation.Contains(e.mousePosition) && !isDragging && toggleInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #region drag points
        // top
        GUI.DragWindow(new Rect(0, 0, scr.x * 6, scr.y * 0.25f));
        // left
        GUI.DragWindow(new Rect(0, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        // right
        GUI.DragWindow(new Rect(scr.x * 5.5f, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        // bottom
        GUI.DragWindow(new Rect(0, scr.y * 4, scr.x * 6, scr.y * 0.25f));
        #endregion
    }
    #endregion
    #endregion

    #region start
    // Start is called before the first frame update
    void Start()
    {
        // set screen size and inv size 
        scr.y = Screen.height / 9;
        scr.x = Screen.width / 16;
        invSize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        equipSize = new Rect(invSize.x *10, scr.y, scr.x * 2, scr.y * 4);       
        // add null items to make inventory slots based on slot x and y set in inspector
        for (int i = 0; i < slotX * slotY; i++)
        {
            inv.Add(new Item());
        }
        for (int i = 0; i < 2; i++)
        {
            equip.Add(new Item());
        }
        AddItem(0);
        AddItem(12);
        AddItem(21);
        AddItem(27);
        AddItem(16);
    }
    #endregion
    #region update 
    // Update is called once per frame
    void Update()
    {
        // toggle inv
        if (Input.GetKeyDown(toggle))
        {
            ToggleInv();
        }
        // just in case the screen changes size
        if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.y = Screen.height / 9;
            scr.x = Screen.width / 16;
            invSize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
            equipSize = new Rect(invSize.x * 2, scr.y, scr.x * 2, scr.y * 4);
            Vector2 invpos = invSize.position;
            equipSize.position = invSize.position + invpos  ;
        }
        // add a random item will make removing possible when its not broken
        addremove();
    }
    void addremove()
    {
        // add a random item
        if (Input.GetKeyDown(KeyCode.A))
        {
            rand = Random.Range(0, 30);
            AddItem(rand);
        }
        // remove items (broken)
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    RemoveItem(rand);
        //}
    }
    #endregion
    #region onGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region inventory when true
        if (toggleInv)
        {
            invSize = ClampToScreen(GUI.Window(1, invSize, InventoryDrag, "Player Inventory"));
            equipSize = ClampToScreen(GUI.Window(2, equipSize, EquipDrag, "Player Equipment"));
            #region tooltipdisplay
            if (showToolTip)
            {
                toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.01f, scr.x * 6, scr.y * 2);
                GUI.Window(7, toolTipRect, DrawToolTip, "");
            }
            #endregion

        }
        #endregion
        #region drop item
        if ((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !toggleInv))
        {
            DropItem();
            Debug.Log("Dropped item " + draggedItem.Name);
        }
        #endregion
        #region draw item on mouse
        if (isDragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                GUI.Window(72, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    #endregion




}
