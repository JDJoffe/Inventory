using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialInventory : MonoBehaviour
{
    public List<Item> inv = new List<Item>();
    #region var
    [Header("Main UI")]
    public bool showSelectMenu;
    public bool toggleToggleable;
    public float scrW, scrH;
    [Header("Resources")]
    public Texture2D radialTex;
    public Texture2D slotTex;
    [Range(0, 100)]
    public int circleScaleOffset;
    [Header("Icons")]
    public Vector2 iconSize;
    public bool showIcons, showBoxes, showBounds;
    [Range(0.1f, 1)]
    public float iconSizeNum = 1;
    [Range(-360, 360)]
    public int radialRot;
    [SerializeField]
    public float iconOffset;
    [Header("Mouse Settings")]
    public Vector2 mouse;
    public Vector2 input;
    public Vector2 circleCentre;
    [Header("Input Settings")]
    public int KeyIndex;
    public int mouseIndex, inputIndex;
    public float inputDist, inputAng;
    [Header("Sector Settings")]
    public Vector2[] slotPos;
    public Vector2[] boundPos;
    [Range(1, 8)]
    public int numOfSectors = 1;
    [Range(50, 300)]
    public float circleRadius;
    public float mouseDist, sectorDegree, mouseAng;
    public int sectorIndex = 0;
    public bool withinCircle;
    [Header("Misc")]
    private Rect debugWindow;
    #region Set Up Func
    private Vector2 Scr(float x, float y)
    {
        Vector2 coord = new Vector2(scrW * x, scrH * y);
        return coord;
    }
    private Vector2[] BoundPosition(int slots)
    {
        Vector2[] boundPos = new Vector2[slots];
        float ang = 0 + radialRot;
        for (int i = 0; i < boundPos.Length; i++)
        {
            // make a circle yay
            boundPos[i].x = circleCentre.x + circleRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
            boundPos[i].y = circleCentre.y + circleRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
            ang += sectorDegree;
        }
        return boundPos;
    }
    private Vector2[] SlotPositions(int slots)
    {
        Vector2[] slotPos = new Vector2[slots];
        float ang = ((iconOffset / 2) * 2) + radialRot;
        for (int i = 0; i < slotPos.Length; i++)
        {
            // make a circle yay
            slotPos[i].x = circleCentre.x + circleRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
            slotPos[i].y = circleCentre.y + circleRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
            ang += sectorDegree;
        }
        return slotPos;
    }
    private void SetItemSlots(int slots, Vector2[] pos)
    {
        for (int i = 0; i < slots; i++)
        {
            // have a var that is == to slots -i and decrease by 1 each time the for loop runs
            // slots-i-1
            // 1st run slots = 8, i = 0, 1 = 1
            // inv[7].icon
            // 2nd run slots = 8, i = 1, 1 = 1
            // inv[6].icon
            //etc...
            GUI.DrawTexture(new Rect(pos[i].x - (scrW * iconSizeNum * 0.5f), pos[i].y - (scrH * iconSizeNum * 0.5f), scrW * iconSizeNum, scrH * iconSizeNum), inv[slots-i-1].Icon);
        }
    }
    private int CheckCurrentSector(float ang)
    {
        float boundingAng = 0;
        for (int i = 0; i < numOfSectors; i++)
        {
            boundingAng += sectorDegree;
            if (ang < boundingAng)
            {
                return i;
            }
        }
        return 0;
    }
    private void CalculateMouseAngles()
    {
        //
        // this is all  cursed please find a way to fix it
        //
        mouse = Input.mousePosition;
        input.x = Input.GetAxis("Horizontal");
        input.y = -Input.GetAxis("Vertical");
        mouseDist = Mathf.Sqrt(Mathf.Pow((mouse.x - circleCentre.x), 2) + Mathf.Pow((mouse.y - circleCentre.y), 2));
        inputDist = Vector2.Distance(Vector2.zero, input);
        // gross yucky not nice conditional operator
        withinCircle = mouseDist <= circleRadius ? true : false;
        // if input.magnitude != 0
        if (input.x != 0 || input.y != 0)
        {
            inputAng = (Mathf.Atan2(-input.y, input.x) * 180 / Mathf.PI) + radialRot;
        }
        else
        {
            mouseAng = (Mathf.Atan2(mouse.y - circleCentre.y, mouse.x - circleCentre.x) * 180 / Mathf.PI) + radialRot;
        }
        if (mouseAng < 0)
        {
            mouseAng += 360;
        }
        if (inputAng < 0)
        {
            inputAng += 360;
        }
        inputIndex = CheckCurrentSector(inputAng);
        mouseIndex = CheckCurrentSector(mouseAng);
        if (input.x != 0 || input.y != 0)
        {
            sectorIndex = inputIndex;
        }
        if (input.x == 0 && input.y == 0)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                sectorIndex = mouseIndex;
            }
        }
    }
    #endregion
    #region Unity Func
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            inv.Add(ItemData.CreateItem(i));
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            scrH = Screen.height / 9;
            scrW = Screen.width / 16;
            circleCentre.x = Screen.width / 2;
            circleCentre.y = Screen.height / 2;
            showSelectMenu = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            showSelectMenu = false;
        }
    }
    private void OnGUI()
    {
        if (showSelectMenu)
        {
            CalculateMouseAngles();
            sectorDegree = 360 / numOfSectors;
            iconOffset = sectorDegree / 2;
            slotPos = SlotPositions(numOfSectors);
            boundPos = BoundPosition(numOfSectors);
            // deadzone
            GUI.Box(new Rect(Scr(7.5f, 4), Scr(1, 1)), "");
            // circle 
            GUI.DrawTexture(new Rect(
                circleCentre.x - circleRadius - (circleScaleOffset / 4),
                circleCentre.y - circleRadius - (circleScaleOffset / 4),
                (circleRadius * 2) + (circleScaleOffset / 2), 
                (circleRadius * 2) + (circleScaleOffset / 2)), radialTex);
            if (showBoxes)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.DrawTexture(new Rect(
                        slotPos[i].x - (scrW * iconSizeNum * 0.5f),
                        slotPos[i].y - (scrH * iconSizeNum * 0.5f),
                        scrW * iconSizeNum,
                        scrH * iconSizeNum), slotTex);
                }
            }
            if (showBounds)
            {
                for (int i = 0; i < numOfSectors; i++)
                {
                    GUI.Box(new Rect(
                        boundPos[i].x - (scrW * 0.1f * 0.5f),
                        boundPos[i].y - (scrH * 0.1f * 0.5f),
                        scrW * 0.1f, scrH * 0.1f), "");
                }
            }
            if (showIcons)
            {
                SetItemSlots(numOfSectors, slotPos);
            }
        }
    }
    #endregion
    #endregion

}
