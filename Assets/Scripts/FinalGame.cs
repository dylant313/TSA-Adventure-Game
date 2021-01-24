using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGame : MonoBehaviour
{
    public CanvasGroup fadeBlack;
    public Camera buildCamera;
    public Camera mainCamera;

    void Start()
    {
        buildCamera.enabled = false;
        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitUntil(() => PlayerRaycast.partsDone == 4);
        for (float i = 0; i < 1; i+= 0.05f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
        mainCamera.enabled = false;
        buildCamera.enabled = true;
        for (float i = 1; i > 0; i -= 0.05f)
        {
            fadeBlack.alpha = i;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
