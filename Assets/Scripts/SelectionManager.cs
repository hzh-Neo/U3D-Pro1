using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactObj = selectionTransform.GetComponent<InteractableObject>();
            if (interactObj)
            {
                interaction_Info_UI.text = interactObj.GetItemName();
                interaction_Info_UI.gameObject.SetActive(true);
            }
            else
            {
                interaction_Info_UI.gameObject.SetActive(false);
            }

        }
    }
}