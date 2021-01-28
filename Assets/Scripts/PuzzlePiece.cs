using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    public static int prefabCount;
    public static int inPlace = 0;
    private int prefabID;
    private bool beingDragged;
    private RectTransform rectTransform;
    public Vector2 lastPosition;
    public List<GameObject> collisions = new List<GameObject>();

    void Start()
    {
        prefabID = prefabCount;
        prefabCount++;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = GameObject.Find("Slot" + prefabID).GetComponent<RectTransform>().anchoredPosition;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("piece" + prefabID);
        StartCoroutine(TrackPositions());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && beingDragged)
        {
            transform.Rotate(new Vector3(0,0,-90));
            if (transform.rotation.eulerAngles.z % 360 == 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            }
        }
        if(transform.hasChanged && !beingDragged)
        {
            transform.hasChanged = false;
            lastPosition = rectTransform.anchoredPosition;
        }
    }

    IEnumerator TrackPositions()
    {
        yield return new WaitUntil(() => roundPosition() == ElectricGame.correctPositions[prefabID - 1]);
        inPlace++;
        yield return new WaitUntil(() => roundPosition() != ElectricGame.correctPositions[prefabID - 1]);
        inPlace--;
        StartCoroutine(TrackPositions());
    }

    private Vector2 roundPosition()
    {
        int x = Mathf.RoundToInt(rectTransform.anchoredPosition.x);
        int y = Mathf.RoundToInt(rectTransform.anchoredPosition.y);
        return new Vector2(x, y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        beingDragged = true;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(beingDragged)
        {
            if (collisions.Count == 1)
            {
                rectTransform.anchoredPosition = collisions[0].GetComponent<RectTransform>().anchoredPosition;
                collisions[0].GetComponent<RectTransform>().anchoredPosition = lastPosition;
                lastPosition = rectTransform.anchoredPosition;
            }
            else
            {
                rectTransform.anchoredPosition = lastPosition;
            }
            beingDragged = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)   
    {
        collisions.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisions.Remove(collision.gameObject);
    }
}
