using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }
    int Size;

    public Color Idle;
    public Color Hovered;

    Image img;

    public Cell(int _X, int _Y)
    {
        X = _X;
        Y = _Y;
        img = GetComponent<Image>();
        img.color = Idle;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
