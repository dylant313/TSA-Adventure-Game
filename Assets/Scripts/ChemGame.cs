using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemGame : MonoBehaviour
{
    public Slider tube1;
    public Slider tube2;
    public Slider tube3;
    public Slider tube4;
    public Text noteText;
    private RectTransform rectTransform;
    private int mover;
    private bool blocker;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector3(0, -435, 0);
        StartCoroutine(ChemGameWait(true));
    }

    public IEnumerator ChemGameWait(bool start)
    {
        yield return new WaitUntil(() => PlayerRaycast.chemGameOn == true);
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
            yield return new WaitForSeconds(1f);
            if (tube1.value == 6 && tube2.value == 24 && tube3.value == 2 && tube4.value == 4)
            {
                noteText.text = "Note:\nComplete!";
                GameObject.Find("Part3 - Fuel").SetActive(false);
                yield return new WaitForSeconds(1f);
                for (int i = 1; i <= 29; i++)
                {
                    mover -= i;
                    rectTransform.anchoredPosition = new Vector3(0, mover, 0);
                    yield return new WaitForSeconds(0.01f);
                }
                PlayerRaycast.chemGameOn = false;
                PlayerRaycast.partsDone++;
            }
            else
            {
                blocker = false;
            }
        }
    }

    void Update()
    {
        if (PlayerRaycast.chemGameOn && blocker == false)
        {
            if(tube1.value == 6 && tube2.value == 24 && tube3.value == 2 && tube4.value == 4)
            {
                StartCoroutine(ChemGameWait(false));
                blocker = true;
            }
        }
    }
}
