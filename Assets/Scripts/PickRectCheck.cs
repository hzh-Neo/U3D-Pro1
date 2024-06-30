using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickRectCheck : MonoBehaviour
{
    public GameObject interObj;
    private InteractableObject iObj;
    private string defaultName;

    private void Start()
    {
        iObj = interObj.GetComponent<InteractableObject>();
        defaultName = iObj.ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iObj.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iObj.enabled = false;
        }
    }
}
