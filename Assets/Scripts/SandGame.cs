using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandGame : MonoBehaviour
{
    private RectTransform rectTransform;
    private int mover;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -435, 0);
        StartCoroutine(SandGameWait(true));
    }

    public IEnumerator SandGameWait(bool start)
    {
        yield return new WaitUntil(() => PlayerRaycast.sandGameOn == true);
        mover = 0;
        if (start)
        {
            for (int i = 29; i > 0; i--)
            {
                mover += i;
                rectTransform.anchoredPosition = new Vector3(0, -435 + mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 29; i++)
            {
                mover -= i;
                rectTransform.anchoredPosition = new Vector3(0, mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            PlayerRaycast.sandGameOn = false;
        }

    }
}
