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
    public int slotX, slotY;
    public Rect invSize;
    Vector2 scr;
    [Header("Dragging")]
    public bool isDragging;
    public int draggedFrom;
    public Item draggedItem;
    public GameObject droppedItem;
    [Header("ToolTip")]
    public int toolTipItem;
    public bool showToolTip;
    KeyCode toggleTip = KeyCode.Semicolon;
    public Rect toolTipRect;
    [Header("References and Locations")]
    int misc;
    #endregion
    #region Clamp to screen
    //drag inventory around screen but not outside it
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region add item
    public void AddItem(int itemID)
    {
        for (int i = 0; i < inv.Count; i++)
        {
            if (inv[i].Name == null)
            {
                inv[i] = ItemData.CreateItem(itemID);
                Debug.Log("Added Item: " + inv[i].Name);
                return;
            }
        }
    }
    #endregion
    #region drop item
    // drop selected item and instantiate it
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
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * .5f, scr.y * .5f), draggedItem.Icon);
        }
    }
    #endregion
    #region tooltip
    #region tooltipcontent
    // info of the item as we are usually just displaying the item icon
    private string toolTipText(int index)
    {
        string toolTipTxt = inv[index].Name + "\n" + inv[index].Desctiption + "\nValue: " + inv[index].Value;
        return toolTipTxt;
    }
    #endregion
    #region tooltipwindow
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scr.x * 6, scr.y * 2), toolTipText(toolTipItem));
    }
    #endregion
    #endregion
    #region toggle inv
    public void ToggleInv()
    {
        toggleInv = !toggleInv;
        if (toggleInv)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #endregion
    #region drag inv
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, scr.y * .25f, scr.x * 6, scr.y * .5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4.25f, scr.x * 6, scr.y * .5f), "Gold Dispay");
        showToolTip = false;
        #region grid nested for loops
        int i = 0;
        Event e = Event.current;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                #region pickup item
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inv[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inv[i];
                    inv[i] = new Item();
                    isDragging = true;
                    draggedFrom = i;
                    Debug.Log("Currently dragging your " + draggedItem.Name);
                }
                #endregion
                #region swap item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name != null)
                {
                    Debug.Log("Swapped your items" + draggedItem.Name + "&" + inv[i].Name);
                    inv[draggedFrom] = inv[i];
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion
                #region place item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name == null)
                {
                    Debug.Log("Placed your item" + draggedItem.Name + "in slot " + slotLocation.ToString());
                    inv[i] = draggedItem;
                    draggedItem = new Item();
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
    #region start
    // Start is called before the first frame update
    void Start()
    {
        scr.y = Screen.height / 9;
        scr.x = Screen.width / 16;
        invSize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        for (int i = 0; i < slotX * slotY; i++)
        {
            inv.Add(new Item());
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
        if (Input.GetKeyDown(toggle))
        {
            ToggleInv();
        }
        if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.y = Screen.height / 9;
            scr.x = Screen.width / 16;
            invSize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        }
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
            Debug.Log("Dropped item" + draggedItem.Name);
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
