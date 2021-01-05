using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    private Camera cam;
    public float distance;
    public Image cursor;
    public CanvasGroup mathUI;
    public static bool mathGameOn = false;

    void Start()
    {
        cam = Camera.main;
        mathUI.alpha = 0f;
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
                        mathGameOn = true;
                        MathGameManager(true);
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

    public void MathGameManager(bool start)
    {
        FirstPersonController.canMove = !start;
        Cursor.visible = start;
        if (start)
        {
            Cursor.lockState = CursorLockMode.Confined;
            mathUI.alpha = 1f;
            StartCoroutine(MathGame());
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            mathUI.alpha = 0f;
        }
    }

    public IEnumerator MathGame()
    {
        yield return new WaitUntil(() => mathGameOn == false);
        MathGameManager(false);
    }
}
