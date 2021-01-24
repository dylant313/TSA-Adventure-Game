using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SandPiece : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static int prefabCount;
    public static int clicked1 = -1;
    public static int clicked2 = -1;
    private int prefabID;
    private RectTransform rectTransform;
    private Image thisImage;

    void Start()
    {
        prefabID = prefabCount;
        prefabCount++;
        rectTransform = GetComponent<RectTransform>();
        thisImage = GetComponent<Image>();
        rectTransform.anchoredPosition = new Vector2(-210 + (60 * (prefabID % 8)),-120 + (60 * (prefabID / 8)));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        thisImage.color = new Color32(176, 176 ,176 ,255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        thisImage.color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(clicked1 == -1)
        {
            clicked1 = prefabID;
            StartCoroutine(Rotate());
        }
        else if (clicked2 == -1)
        {
            clicked2 = prefabID;
            StartCoroutine(Rotate());
        }
    }

    public IEnumerator Rotate()
    {
        for(int i = 0; i < 30; i++)
        {
            transform.Rotate(new Vector3(0, 3, 0));
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitUntil(() => clicked1 == -1 && clicked2 == -1);
        for (int i = 0; i < 30; i++)
        {
            transform.Rotate(new Vector3(0, -3, 0));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
