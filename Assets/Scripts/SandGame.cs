using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandGame : MonoBehaviour
{
    public GameObject sandPrefab;
    public Image sandBoard;
    public Image fin11;
    public Image fin12;
    public Image fin21;
    public Image fin22;
    private RectTransform rectTransform;
    private int mover;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -435, 0);
        StartCoroutine(SandGameWait(true));
        SandPiece.prefabCount = 0;
    }

    public IEnumerator SandGameWait(bool start)
    {
        yield return new WaitUntil(() => PlayerRaycast.sandGameOn == true);
        mover = 0;
        if (start)
        {
            for (int i = 0; i < 40; i++)
            {
                Instantiate(sandPrefab, Vector3.zero, Quaternion.identity, sandBoard.transform);
            }
            for (int i = 29; i > 0; i--)
            {
                mover += i;
                rectTransform.anchoredPosition = new Vector3(0, -435 + mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine(CardMatcher());
        }
        else
        {
            yield return new WaitUntil(() => fin11.GetComponent<CanvasGroup>().alpha == 0 && fin21.GetComponent<CanvasGroup>().alpha == 0);
            GameObject.Find("Part1 - Sand").SetActive(false);
            yield return new WaitForSeconds(1.5f);
            for (int i = 1; i <= 29; i++)
            {
                mover -= i;
                rectTransform.anchoredPosition = new Vector3(0, mover, 0);
                yield return new WaitForSeconds(0.01f);
            }
            PlayerRaycast.sandGameOn = false;
            PlayerRaycast.partsDone++;
        }

    }

    public IEnumerator CardMatcher()
    {
        yield return new WaitUntil(() => SandPiece.clicked1 > -1 && SandPiece.clicked2 > -1);
        yield return new WaitForSeconds(0.5f);
        if (SandPiece.clicked1 == 0 && SandPiece.clicked2 == 22)
        {
            for(int i = 0; i < 10; i++)
            {
                fin11.rectTransform.sizeDelta += new Vector2(0.1f, 0.1f);
                fin12.rectTransform.sizeDelta += new Vector2(0.1f, 0.1f);
                fin11.GetComponent<CanvasGroup>().alpha -= 0.1f;
                fin12.GetComponent<CanvasGroup>().alpha -= 0.1f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if(SandPiece.clicked1 == 12 && SandPiece.clicked2 == 26)
        {
            for (int i = 0; i < 10; i++)
            {
                fin21.rectTransform.sizeDelta += new Vector2(0.1f, 0.1f);
                fin22.rectTransform.sizeDelta += new Vector2(0.1f, 0.1f);
                fin21.GetComponent<CanvasGroup>().alpha -= 0.1f;
                fin22.GetComponent<CanvasGroup>().alpha -= 0.1f;
                yield return new WaitForSeconds(0.01f);
            }
            StartCoroutine(SandGameWait(false));
        }
        yield return new WaitForSeconds(0.5f);
        SandPiece.clicked1 = -1;
        SandPiece.clicked2 = -1;
        StartCoroutine(CardMatcher());
    }
}
