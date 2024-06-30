using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public GameObject bagOne;
    // Update is called once per frame
    void Update()
    {
        onListenToggleBag();
    }

    private void onListenToggleBag()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            toggleBagOpen();
        }
    }

    private void toggleBagOpen()
    {
 
        bool isActive = bagOne.activeSelf;
        bagOne.SetActive(!isActive);

        if (!isActive)
        {
            if (InitBagHoll.Instance.isInit)
            {
                EventSystem.Publish(default(UpdateBag));
            }
          
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // 根据需要锁定或解锁光标
        }
    }

}
