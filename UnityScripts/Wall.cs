using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int X { get; set; }
    public int Y { get; set; }

    public Wall(int _Height, int _Width)
    {
        GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _Width);
        GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _Height);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
