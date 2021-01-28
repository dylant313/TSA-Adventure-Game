using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDrag : MonoBehaviour
{
    private float distance_to_screen;
    private Camera buildCamera;
    private bool isLocked = false;
    public int manualOffsetX;
    public int manualOffsetY;
    public int partID;
    public static int lockedCount = 0;

    private void Start()
    {
        buildCamera = GameObject.Find("Build Camera").GetComponent<Camera>();
    }

    void OnMouseDrag()
    {
        if(!isLocked)
        {
            distance_to_screen = buildCamera.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = buildCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + manualOffsetX, Input.mousePosition.y + manualOffsetY, distance_to_screen));
            if(Vector3.Distance(transform.position, FinalGame.positionArray[partID].transform.position) < 1)
            {
                transform.position = FinalGame.positionArray[partID].transform.position;
                isLocked = true;
                lockedCount++;
            }
        }
    }
}
