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
    void Start()
    {
        //img = GetComponent<Image>();
        //img.color = Idle;
    }
    void Update()
    {

    }

    [ContextMenu("Data")]
    public void ChangeColor()
    {
        Debug.Log("aboba");
    }
}
