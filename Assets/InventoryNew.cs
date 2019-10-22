using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Lineara
{
    public class InventoryNew : MonoBehaviour
    {
        #region var
        public List<Item> inv = new List<Item>();
        public Item selectedItem;
        public bool showInv;
        public Vector2 scr;
        public Vector2 scrollpos;
        public int money;
        public string sortType = "";
        public Transform dropLocation;
        public Transform hand;
        public Transform head;
        public GUISkin itemTextSkin;
        public GUISkin baseSkin;
        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        };
        GameObject equippedItem;
        public equipment[] equipmentSlots;

        bool invnotloaded = true;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            int o = 0;
            while (invnotloaded)
            {
                inv.Add(ItemData.CreateItem(o));
                o++;
                if (o >= 30)
                {
                    invnotloaded = false;
                }
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                inv[21].Amount += 3;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inv.Add(ItemData.CreateItem(Random.Range(0, 29)));
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                showInv = !showInv;
                if (showInv)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
            }
        }
        private void OnGUI()
        {
            GUI.skin = baseSkin;
            if (showInv)
            {
                // you would normally put this in an options menu
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scr.x * 12, Screen.height), "");
                Display();

                for (int i = 0; i < 11; i++)
                {
                    // var = the itemtype associated with the i e.g. if i is 0 var itemtype will be apparrel
                    var itemType = (ItemType)i;
                    if (GUI.Button(new Rect(50.25f + i * scr.x, 0, scr.x, 0.25f * scr.y), itemType.ToString()))
                    {
                        sortType = itemType.ToString();
                    }
                }
                // display the image of selected item
                if (selectedItem != null)
                {
                    #region gui display selected item info
                    GUI.skin = itemTextSkin;

                    //name
                    GUI.Box(new Rect(4f * scr.x, .5f * scr.y, 3f * scr.x, .275f * scr.y), selectedItem.Name);
                    //tex box
                    GUI.Box(new Rect(4.5f * scr.x, 1f * scr.y, 2f * scr.x, 2f * scr.y), "");
                    //tex
                    GUI.DrawTexture(new Rect(4.75f * scr.x, 1.275f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Icon);
                    //desc
                    GUI.Box(new Rect(4f * scr.x, 3.275f * scr.y, 3f * scr.x, 1f * scr.y), selectedItem.Desctiption);
                    //amount
                    GUI.Box(new Rect(4f * scr.x, 4.3f * scr.y, 1.2f * scr.x, .275f * scr.y), "Amount " + selectedItem.Amount);
                    //value
                    GUI.Box(new Rect(4f * scr.x, 4.6f * scr.y, 1.2f * scr.x, .275f * scr.y), "Value " + selectedItem.Value);
                    //durability
                    GUI.Box(new Rect(4f * scr.x, 4.9f * scr.y, 1.2f * scr.x, .275f * scr.y), "Durability " + selectedItem.Durability);
                    #endregion
                    ItemUse(selectedItem.Type);
                }
                else { return; }
                GUI.skin = baseSkin;
            }
            void Display()
            {
                // amount of that type
                int a = 0;
                // slot position
                int s = 0;
                for (int i = 0; i < inv.Count; i++)
                {
                    if (!(sortType == "All" || sortType == ""))
                    {
                       //  ItemType type = (ItemType)i;
                     //   sortType = type.ToString();
                        ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
                        // find your type
                        if (inv[i].Type == type)
                        {
                            // increment for each item found
                            a++;
                            if (a <= 30)
                            {
                                shortButton(0.5f * scr.x, .75f * scr.y + s * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y, inv[i].Name, i);                              
                                    s++;                              
                            }
                            else
                            {
                                scrollpos = GUI.BeginScrollView(new Rect(0, .75f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollpos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 30) * (0.25f * scr.y))), false, true);
                                shortButton(0.5f * scr.x, 0 * scr.y + s * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y, inv[i].Name, i);                             
                                    s++;
                                GUI.EndScrollView();
                            }
                        }
                    }
                    else
                    {
                        // if we have less than or equal to 35
                        if (inv.Count <= 30)
                        {
                            shortButton(0.5f * scr.x, .75f * scr.y + i * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y, inv[i].Name, i);                        
                        }
                        else
                        {
                            // movable scroll pos start of viewable area our view window  our current scroll pos scroll zone extra space  horizontal bar visible? vertical bar visible?                     
                            scrollpos = GUI.BeginScrollView(new Rect(0, .75f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollpos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 30) * (0.25f * scr.y))), false, true);
                            shortButton(0.5f*scr.x,0*scr.y+i*(0.25f*scr.y),3f*scr.x,0.25f*scr.y,inv[i].Name,i);                         
                            GUI.EndScrollView();
                        }
                    }
                }
                // short button to call when you wanna make a button
                #region Button
                void shortButton(float xP, float yP, float w, float h, string txt, int i)
                {
                    if (GUI.Button(new Rect(xP, yP, w, h), txt))
                    {                   
                        selectedItem = inv[i];
                    }
                }
                #endregion
            }

            #region Equip&Unequip
            // my equip now boyee
            void EquipUnequip(int j)
            {
                if (equipmentSlots[j].curItem == null || selectedItem.Name != equipmentSlots[j].curItem.name)
                {
                    if (GUI.Button(new Rect(4f * scr.x, 5.2f * scr.y, 2f * scr.x, .5f * scr.y), "Equip"))
                    {
                        if (equipmentSlots[j].curItem != null)
                        {
                            Destroy(equipmentSlots[j].curItem);
                        }
                        equippedItem = Instantiate(selectedItem.ItemMesh, equipmentSlots[j].location);
                        equipmentSlots[j].curItem = equippedItem;
                        equippedItem.name = selectedItem.Name;
                    }
                }
                else
                {

                    if (GUI.Button(new Rect(4f * scr.x, 5.2f * scr.y, 2f * scr.x, .5f * scr.y), "Unequip"))
                    {
                        if (equipmentSlots[j].curItem != null)
                        {
                            Destroy(equipmentSlots[j].curItem);
                        }
                        equippedItem.name = null;
                        equipmentSlots[j].curItem = null;
                        Destroy(equippedItem);
                    }

                }
            }
            #endregion
            #region ItemUse
            void ItemUse(ItemType Type)
            {
                int j;
                switch (Type)
                {

                    case ItemType.Apparrel:
                        EquipUnequip(j = 0);
                        break;
                    case ItemType.Consumable:
                        break;
                    case ItemType.Weapon:
                        EquipUnequip(j = 1);
                        break;
                    case ItemType.Potion:
                        break;
                    case ItemType.Food:
                        break;
                    case ItemType.Material:
                        break;
                    case ItemType.Scroll:
                        break;
                    case ItemType.Quest:
                        break;
                    case ItemType.Money:
                        break;
                    case ItemType.Misc:
                        break;
                    default:
                        break;
                }
                if (GUI.Button(new Rect(4f * scr.x, 6f * scr.y, 1f * scr.x, .5f * scr.y), "Discard"))
                {
                    for (int i = 0; i < equipmentSlots.Length; i++)
                    {
                        //check equiped item
                        if (equipmentSlots[i].curItem != null && selectedItem.ItemMesh.name == equipmentSlots[i].curItem.name)
                        {
                            // if deleted
                            Destroy(equipmentSlots[i].curItem);

                        }
                    }
                    // spawn in front
                    GameObject droppedItem = Instantiate(selectedItem.ItemMesh, dropLocation.position, Quaternion.identity);
                    droppedItem.name = selectedItem.Name;
                    // just in case
                    droppedItem.AddComponent<Rigidbody>().useGravity = true;
                    // remove take one away from list
                    if (selectedItem.Amount > 1) { selectedItem.Amount--; }
                    //remove entry from the list
                    else { inv.Remove(selectedItem); selectedItem = null; return; }
                }
            }
            #endregion
        }
    }
}

