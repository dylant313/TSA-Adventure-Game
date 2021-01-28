using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class FinalGame : MonoBehaviour
{
    public CanvasGroup fadeBlack;
    public Text endText;
    public Camera buildCamera;
    public Camera mainCamera;
    public GameObject cursorUI;
    public GameObject minimap;
    public static GameObject[] positionArray;
    private GameObject[] rocketArray;
    private bool moving = false;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        buildCamera.enabled = false;
        StartCoroutine(WaitForEnd());
        rocketArray = GameObject.FindGameObjectsWithTag("FinalGame");
        foreach (GameObject rocketPart in rocketArray)
        {
            rocketPart.SetActive(false);
        }
        positionArray = GameObject.FindGameObjectsWithTag("FinalPositions").OrderBy(go => go.name).ToArray();
        foreach (GameObject rocketPart in positionArray)
        {
            rocketPart.SetActive(false);
        }
    }

    private void Update()
    {
        if(moving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(500, transform.position.y, transform.position.z), ref velocity, 1);
        }
    }

    IEnumerator WaitForEnd()
    {
        for (float i = 1; i >= 0; i -= 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitUntil(() => PlayerRaycast.partsDone == 4);

        FirstPersonController.canMove = false;
        for (float i = 0; i <= 1; i+= 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
        cursorUI.SetActive(false);
        minimap.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach (GameObject rocketPart in rocketArray)
        {
            rocketPart.SetActive(true);
        }
        mainCamera.enabled = false;
        buildCamera.enabled = true;
        for (float i = 1; i >= 0; i -= 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }

        for (float i = 0; i <= 1; i += 0.049f)
        {
            endText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(3);
        for(float i = 1; i >= 0; i -= 0.049f)
        {
            endText.GetComponent<CanvasGroup>().alpha = i;
            yield return new WaitForSeconds(0.02f);
        }
        endText.GetComponent<CanvasGroup>().alpha = 0;

        yield return new WaitUntil(() => RocketDrag.lockedCount == 7);
        moving = true;
        yield return new WaitForSeconds(5);
        for (float i = 0; i <= 1; i += 0.049f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.03f);
        }
        fadeBlack.alpha = 1;
        SceneManager.LoadScene("Intro");
    }
}
