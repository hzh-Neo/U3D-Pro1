using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public Text interaction_Info_UI;
    public Camera camera1;

    private void Start()
    {

    }

    void Update()
    {
        RayThings();
    }

    private void RayThings()
    {
        Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            Transform selectionTransform = hit.transform;
            InteractableObject interactObj = selectionTransform.GetComponent<InteractableObject>();

            if (interactObj)
            {
                string name = interactObj.GetItemName();
                if (!String.IsNullOrEmpty(name))
                {
                    interaction_Info_UI.text = name;
                    interaction_Info_UI.gameObject.SetActive(true);
                }
                else
                {
                    interaction_Info_UI.gameObject.SetActive(false);
                }
                if (interactObj.canPicker)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Bag.Instance.AddBagItem(interactObj.ImgName, 1);
                        Destroy(interactObj.gameObject);
                    }
                }
            }
            else
            {
                interaction_Info_UI.gameObject.SetActive(false);
            }

        }
        else
        {
            interaction_Info_UI.gameObject.SetActive(false);
        }
    }
}