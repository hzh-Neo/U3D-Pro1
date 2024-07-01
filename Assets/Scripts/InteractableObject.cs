using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public string ImgName;
    public string ItemShowName;
    public bool canPicker;

    public string GetItemName()
    {
        return String.IsNullOrEmpty(ItemShowName) ? ItemName : ItemShowName;
    }
}


