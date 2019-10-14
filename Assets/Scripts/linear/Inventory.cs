using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        };
        public equipment[] equipmentSlots;
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            inv.Add(ItemData.CreateItem(0));
            inv.Add(ItemData.CreateItem(2));
            inv.Add(ItemData.CreateItem(27));
        }

        private void Update()
        {
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
            if (showInv)
            {
                // you would normally put this in an options menu
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;

                GUI.Box(new Rect(0, 0, scr.x * 8, Screen.height), "");
            }
        }
    }
}
