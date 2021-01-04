using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Camera cam;
    public float distance;
    public Image cursor;

    void Start()
    {
        cam = Camera.main;
    }

    void ColorChange(float color)
    {
        var tempColor = cursor.color;
        tempColor.a = color;
        cursor.color = tempColor;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            if (hit.transform.tag != "Untagged")
            {
                ColorChange(1f);
                if (Input.GetMouseButtonDown(0))
                {
                    if(hit.transform.tag == "Part1")
                    {
                        Debug.Log("Part1 found.");
                    }
                    if (hit.transform.tag == "Part2")
                    {
                        Debug.Log("Part2 found.");
                    }
                    if (hit.transform.tag == "Part3")
                    {
                        Debug.Log("Part3 found.");
                    }
                    if (hit.transform.tag == "Part4")
                    {
                        Debug.Log("Part4 found.");
                    }
                }
            }
            else
            {
                ColorChange(0.5f);
            }
        }
        else
        {
            ColorChange(0.5f);
        }
        
    }
}
