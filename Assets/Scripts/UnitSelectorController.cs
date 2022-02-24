using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selector_img;

    [SerializeField] private Color selected, notSelected;

    public Action<GridObject> OnUnitClicked;
    
    private void Start()
    {
        selector_img.color = notSelected;
    }

    public void Selected()
    {
        selector_img.color = selector_img.color == selected ? notSelected : selected;
        OnUnitClicked(GetComponent<GridObject>());
    }

    private void OnMouseDown()
    {
        Selected();
    }
}
