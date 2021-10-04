using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Size;
    public int Margin;

    int CellSize;
    int SizePixels;
    int WallWidth, WallHeight;

    public Cell CellPref;
    public Wall WallPref;

    Cell[,] Cells;


    void Start()
    {
        BoardInit();
    }

    void Update()
    {

    }
    public void BoardInit()
    {
        SizePixels = (int)GetComponent<RectTransform>().rect.width;
        Cells = new Cell[Size, Size];
        CellSize = (int)CellPref.GetComponent<RectTransform>().rect.width;
        WallHeight = (int)WallPref.GetComponent<RectTransform>().rect.height;
        WallWidth = (int)WallPref.GetComponent<RectTransform>().rect.width;
        //cell placing
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                //zeroing
                int X = -SizePixels / 2 + Margin + CellSize / 2;
                int Y = -SizePixels / 2 + Margin + CellSize / 2;
                //positioning
                X += (CellSize + WallHeight) * j;
                Y += (CellSize + WallHeight) * i;

                Cell cell = Instantiate(CellPref, transform, false);
                cell.transform.localPosition = new Vector2(X, Y);
                Cells[i, j] = cell;
            }
        }
        //wall placing
        for(int i = 0; i < Size - 1; i++)
        {
            for (int j = 0; j < Size - 1; j++)
            {

            }
        }
    }
}
