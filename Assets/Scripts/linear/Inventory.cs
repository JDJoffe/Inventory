﻿using System.Collections;
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
        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        };
        public equipment[] equipmentSlots;
        public GUISkin itemTextSkin;
        public GUISkin baseSkin;
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
                // display the image of selected item
                if (selectedItem != null)
                {
                    GUI.skin = itemTextSkin;
                    GUI.Box(new Rect(4.5f * scr.x, 0.275f * scr.y, 2f * scr.x, 2f * scr.y), "");
                    GUI.DrawTexture(new Rect(4.75f * scr.x, 0.475f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Icon);
                    GUI.Box(new Rect(4f * scr.x, 0f * scr.y, 3f * scr.x, .275f * scr.y), selectedItem.Name);
                    GUI.Box(new Rect(4f * scr.x, 2.275f * scr.y, 3f * scr.x, 3f * scr.y), selectedItem.Desctiption);
                }
                else { return; }
            }
            GUI.skin = baseSkin;
        }
        void Display()
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
}
