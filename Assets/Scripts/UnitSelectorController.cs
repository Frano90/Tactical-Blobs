using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selector_img;

    [SerializeField] private Color selected, notSelected;

    public Action<GridObject> OnUnitSelected;
    
    private bool isClicked;
    private void Start()
    {
        isClicked = false;
        selector_img.color = notSelected;
    }

    public void Selected()
    {
        if (isClicked) return;

        isClicked = true;
        selector_img.color = selector_img.color == selected ? notSelected : selected;
        OnUnitSelected(GetComponent<GridObject>());
    }

    private void OnMouseDown(){Selected();}

    public void Reset()
    {
        isClicked = false;
        selector_img.color = notSelected;
    }
}
