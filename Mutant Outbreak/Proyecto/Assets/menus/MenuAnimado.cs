using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAnimado : MonoBehaviour
{
    public RawImage _img;
    public float _x;
    public float _y;
    
    void Update()
    {
        Rect currentRect = _img.uvRect;
        Vector2 newPosition = currentRect.position + new Vector2(_x, _y) * Time.deltaTime;
        _img.uvRect = new Rect(newPosition, currentRect.size);
    }
}
