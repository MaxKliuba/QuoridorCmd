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
    Wall[,] Walls;

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
        Walls = new Wall[Size - 1, Size - 1];
        CellSize = (int)CellPref.GetComponent<RectTransform>().rect.width;
        WallHeight = (SizePixels - (2 * Margin + Size * CellSize)) / (Size - 1);
        WallWidth = 2 * CellSize + WallHeight;
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
        //walls placing
        //vertical ones
        for (int i = 0; i < Size - 1; i++)
        {
            for (int j = 0; j < Size - 1; j++)
            {
                //taking cell`s zero point
                int X = -SizePixels / 2 + Margin + CellSize / 2;
                int Y = -SizePixels / 2 + Margin + CellSize / 2;
                //shifting to first wall`s origin
                X += CellSize / 2 + WallHeight / 2;
                Y += CellSize / 2 + WallHeight / 2;
                //positioning
                X += j * (CellSize + WallHeight);
                Y += i * (CellSize + WallHeight);

                Wall wall = Instantiate(WallPref, transform, false);
                wall.transform.localPosition = new Vector2(X, Y);
                wall.transform.Rotate(Vector3.forward, 90);
                Walls[i, j] = wall;
            }
        }
        //horizontal ones
        for (int i = 0; i < Size - 1; i++)
        {
            for (int j = 0; j < Size - 1; j++)
            {
                //taking cell`s zero point
                int X = -SizePixels / 2 + Margin + CellSize / 2;
                int Y = -SizePixels / 2 + Margin + CellSize / 2;
                //shifting to first wall`s origin
                X += CellSize / 2 + WallHeight / 2;
                Y += CellSize / 2 + WallHeight / 2;
                //positioning
                X += j * (CellSize + WallHeight);
                Y += i * (CellSize + WallHeight);

                Wall wall = Instantiate(WallPref, transform, false);
                wall.transform.localPosition = new Vector2(X, Y);
                //no rotation needed
                Walls[i, j] = wall;
            }
        }
    }
}
