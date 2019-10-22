using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Linear
{
    public class Inventory : MonoBehaviour
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

                GUI.Box(new Rect(0, 0, scr.x * 8, Screen.height), "");
                Display();
                    if (GUI.Button(new Rect(4.25f * scr.x, 0, scr.x, 0.25f * scr.y), "All"))
                    {
                        sortType = "All";
                    }
                    if (GUI.Button(new Rect(5.25f * scr.x, 0, scr.x, 0.25f * scr.y), "Apparrel"))
                    {
                        sortType = "Apparrel";
                    }
                    if (GUI.Button(new Rect(6.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Consumable"))
                    {
                        sortType = "Consumable";
                    }
                    if (GUI.Button(new Rect(7.25f * scr.x, 0, scr.x, 0.25f * scr.y), "Weapon"))
                    {
                        sortType = "Weapon";
                    }
                    if (GUI.Button(new Rect(8.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Potion"))
                    {
                        sortType = "Potion";
                    }
                    if (GUI.Button(new Rect(9.25f * scr.x, 0, scr.x, 0.25f * scr.y), "Food"))
                    {
                        sortType = "Food";
                    }
                    if (GUI.Button(new Rect(10.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Material"))
                    {
                        sortType = "Material";
                    }
                    if (GUI.Button(new Rect(11.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Scroll"))
                    {
                        sortType = "Scroll";
                    }
                    if (GUI.Button(new Rect(12.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Quest"))
                    {
                        sortType = "Quest";
                    }
                    if (GUI.Button(new Rect(13.25f  * scr.x, 0, scr.x, 0.25f * scr.y), "Money"))
                    {
                        sortType = "Money";
                    }
                    if (GUI.Button(new Rect(14.25f * scr.x, 0, scr.x, 0.25f * scr.y), "Misc"))
                    {
                        sortType = "Misc";
                    }
                    // display the image of selected item
                    if (selectedItem != null)
                {
                    #region gui display selected item info
                    GUI.skin = itemTextSkin;
                    GUI.Box(new Rect(4.5f * scr.x, 0.275f * scr.y, 2f * scr.x, 2f * scr.y), "");
                    GUI.DrawTexture(new Rect(4.75f * scr.x, 0.475f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Icon);
                    GUI.Box(new Rect(4f * scr.x, 0f * scr.y, 3f * scr.x, .275f * scr.y), selectedItem.Name);
                    GUI.Box(new Rect(4f * scr.x, 2.275f * scr.y, 3f * scr.x, 1f * scr.y), selectedItem.Desctiption);
                    GUI.Box(new Rect(4f * scr.x, 3.275f * scr.y, 1.2f * scr.x, .275f * scr.y), "Amount " + selectedItem.Amount);
                    GUI.Box(new Rect(4f * scr.x, 3.555f * scr.y, 1.2f * scr.x, .275f * scr.y), "Value " + selectedItem.Value);
                    GUI.Box(new Rect(4f * scr.x, 3.83f * scr.y, 1.2f * scr.x, .275f * scr.y), "Durability " + selectedItem.Durability);
                    #endregion
                    ItemUse(selectedItem.Type);
                }
                else { return; }

            
            GUI.skin = baseSkin;

            GUI.Box(new Rect(0, 0, scr.x * 8, Screen.height), "");
           
            }           
        }
        void Display()
        {
            if (!(sortType == "All" || sortType == ""))
            {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType);
                // amount of that type
                int a = 0;
                // slot position
                int s = 0;
                for (int i = 0; i < inv.Count; i++)
                {
                    // find your type
                    if (inv[i].Type == type)
                    {
                        // increment for each item found
                        a++;
                    }
                }
                if (a <= 30)
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y), inv[i].Name))
                            {
                                selectedItem = inv[i];
                            }
                            s++;
                        }
                    }
                }
                else
                {
                    scrollpos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollpos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 30) * (0.25f * scr.y))), false, true);
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + s * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y), inv[i].Name))
                            {
                                selectedItem = inv[i];
                            }
                            s++;
                        }
                    }
                    GUI.EndScrollView();
                }
            }
            else
            {
                // if we have less than or equal to 35
                if (inv.Count <= 30)
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                    }
                }
                else
                {
                    // movable scroll pos
                    //start of viewable area
                    // our view window
                    // our current scroll pos
                    // scroll zone extra space
                    // horizontal bar visible?
                    // vertical bar visible?
                    scrollpos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollpos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 30) * (0.25f * scr.y))), false, true);



                    for (int i = 0; i < inv.Count; i++)
                    {



                        if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + i * (0.25f * scr.y), 3f * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }


                    }
                    GUI.EndScrollView();
                }
            }

        }
        #region Equip&Unequip
        // my equip now boyee
        void EquipUnequip(int j)
        {
            if (equipmentSlots[j].curItem == null || selectedItem.Name != equipmentSlots[j].curItem.name)
            {
                if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 4f * scr.x, .5f * scr.y), "Equip Jamie ver"))
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
                if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 4f * scr.x, .5f * scr.y), "Unequip Jamie ver"))
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

