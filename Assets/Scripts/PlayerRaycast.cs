using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Camera cam;
    public float distance;
    public Image cursor;
    public static bool mathGameOn = false;
    public static bool chemGameOn = false;
    public static bool elecGameOn = false;
    public static bool sandGameOn = false;
    public static int partsDone = 0;

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
                if (Input.GetMouseButtonDown(0) && FirstPersonController.canMove)
                {
                    if(hit.transform.tag == "Part1")
                    {
                        sandGameOn = true;
                        GameManager(true);
                    }
                    if (hit.transform.tag == "Part2")
                    {
                        elecGameOn = true;
                        GameManager(true);
                    }
                    if (hit.transform.tag == "Part3")
                    {
                        chemGameOn = true;
                        GameManager(true);
                    }
                    if (hit.transform.tag == "Part4")
                    {
                        mathGameOn = true;
                        GameManager(true);
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

        if(Input.GetKeyDown(KeyCode.P))
        {
            partsDone = 4;
        }
    }

    public void GameManager(bool start)
    {
        FirstPersonController.canMove = !start;
        Cursor.visible = start;
        if(start)
        {
            Cursor.lockState = CursorLockMode.None;
            if(mathGameOn)
            {
                StartCoroutine(MathDisable());
            }
            if (chemGameOn)
            {
                StartCoroutine(ChemDisable());
            }
            if (elecGameOn)
            {
                StartCoroutine(ElecDisable());
            }
            if (sandGameOn)
            {
                StartCoroutine(SandDisable());
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public IEnumerator MathDisable()
    {
        yield return new WaitUntil(() => mathGameOn == false);
        GameManager(false);
    }

    public IEnumerator ChemDisable()
    {
        yield return new WaitUntil(() => chemGameOn == false);
        GameManager(false);
    }

    public IEnumerator ElecDisable()
    {
        yield return new WaitUntil(() => elecGameOn == false);
        GameManager(false);
    }

    public IEnumerator SandDisable()
    {
        yield return new WaitUntil(() => sandGameOn == false);
        GameManager(false);
    }
}
