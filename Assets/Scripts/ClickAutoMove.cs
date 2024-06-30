using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickAutoMove : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AutoMove();
    }

    private void AutoMove()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                agent.SetDestination(raycastHit.point);

            }
        }
    }
}
