using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    #region var
    [Header("Inventory")]
   public bool toggleInv  =false;
    KeyCode toggle = KeyCode.Tab;
    public List<Item> inv = new List<Item>();
    public int slotX, slotY;
    public Rect invSize;
    Vector2 scr;
    [Header("Dragging")]
    public bool isDragging;
    public int draggedFrom;
    public Item draggedItem;
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
    #endregion
    #region draw item
    #endregion
    #region tooltip
    #endregion
    #region toggle inv
    #endregion
    #region drag inv
    #endregion
    #region start
    // Start is called before the first frame update
    void Start()
    {

    }
    #endregion
    #region update 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggle))
        {
            toggleInv = !toggleInv;

        }
    }
    #endregion
    #region onGUI
    private void OnGUI()
    {
        scr.y = Screen.height / 9;
        scr.x = Screen.width / 16;
        GUI.backgroundColor = Color.cyan;
        GUI.Box(new Rect(4 * scr.x, 4 * scr.y, 4 * scr.x, 4 * scr.y), "ooooo");
    }
    #endregion




}
