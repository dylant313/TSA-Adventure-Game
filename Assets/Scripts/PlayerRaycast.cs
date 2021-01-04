using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    private Camera cam;
    public float distance;

    void Start()
    {
        cam = Camera.main;
    }

    void CheckInteraction()
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;
        RaycastHit hit;

        if(Physics.Raycast(origin, direction, out hit, distance))
        {
            if(hit.transform.tag == "Part1")
            {
                Debug.Log("Part1 found.");
            }
            else
            {
                Debug.Log("Not a part.");
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }
    }
}
