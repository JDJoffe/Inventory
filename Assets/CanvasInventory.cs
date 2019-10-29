using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Lineara
{
    public class CanvasInventory : MonoBehaviour
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
        #region UI
        GameObject inventory;
        Text itemName, itemDesc, itemAmount, itemValue, itemDur;
        Image itemImage;
        public ScrollRect view;
        public GameObject invButton;
        public RectTransform content;
        #endregion
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
            sortType = "All";
            content = GameObject.Find("Content").GetComponent<RectTransform>();
            inventory = GameObject.Find("Canvas Inventory");
            itemName = GameObject.Find("Name").GetComponent<Text>();
            itemDesc = GameObject.Find("Description").GetComponent<Text>();
            itemAmount = GameObject.Find("Amount").GetComponent<Text>();
            itemValue = GameObject.Find("Value").GetComponent<Text>();
            itemDur = GameObject.Find("Durability").GetComponent<Text>();
            itemImage = GameObject.Find("Image").GetComponent<Image>();
            int o = 0;
            while (invnotloaded)
            {
                inv.Add(ItemData.CreateItem(o));
                GameObject newButton = Instantiate(invButton, content);
                newButton.GetComponentInChildren<Text>().text = inv[o].Name;
                Item item = inv[o];
                newButton.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
                content.sizeDelta = new Vector2(208.3f, 30 * inv.Count);
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
                int o = Random.Range(0, 29);
                inv.Add(ItemData.CreateItem(o));
                GameObject newButton = Instantiate(invButton, content);
                newButton.GetComponentInChildren<Text>().text = inv[o].Name;
                Item item = inv[o];
                newButton.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
                content.sizeDelta = new Vector2(208.3f, 30 * inv.Count);

            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                showInv = !showInv;
                if (showInv)
                {
                    inventory.SetActive(true);
                   
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                else
                {
                    inventory.SetActive(false);
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
            }
            invshow();
        }
        void invshow()
        {
            // you would normally put this in an options menu
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            // display the image of selected item
            if (selectedItem != null)
            {
                #region display selected item info
                //name
                itemName.text = selectedItem.Name;
                //tex
                // itemImage = selectedItem.Icon;
                //desc
                itemDesc.text = selectedItem.Desctiption;
                //amount
                itemAmount.text = selectedItem.Amount.ToString();
                //value
                itemValue.text = selectedItem.Value.ToString();
                //durability
                itemDur.text = selectedItem.Durability.ToString();
                #endregion
                ItemUse(selectedItem.Type);
            }
            else { return; }

        }
        #region buttons
        public void SortType(ItemType type)
        {
            sortType = EventSystem.current.currentSelectedGameObject.name;

            //if (type.ToString() == sortType)
            //{
            //    List<GameObject> invButton2 = new List<GameObject>();
            //    invButton2.Add(invButton);
            //    if (inv[type].Type.ToString() != sortType)
            //    {
            //        invButton2[i].SetActive(false);
            //    }
            //    else { invButton2[i].SetActive(true); /*invButton2.Remove(invButton);*/ }
            //}         
        }     
        public void SelectItem(Item item)
        {
            selectedItem = item;
        }  
        #region Equip&Unequip
        // my equip now boyee
        public void EquipUnequip(int j)
        {
            if (equipmentSlots[j].curItem == null || selectedItem.Name != equipmentSlots[j].curItem.name)
            {
                if (equipmentSlots[j].curItem != null)
                {
                    Destroy(equipmentSlots[j].curItem);
                }
                equippedItem = Instantiate(selectedItem.ItemMesh, equipmentSlots[j].location);
                equipmentSlots[j].curItem = equippedItem;
                equippedItem.name = selectedItem.Name;
            }
            else
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
        #endregion
        #region Discard
        public void Discard()
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
        #endregion
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

        }
        #endregion

    }
}

