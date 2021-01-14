using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    public static int prefabCount;
    private int prefabID;
    private RectTransform rectTransform;
    private bool beingDragged;
    public Vector2 lastPosition;
    public List<GameObject> collisions = new List<GameObject>();

    void Start()
    {
        prefabID = prefabCount;
        prefabCount++;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = GameObject.Find("Slot" + prefabID).GetComponent<RectTransform>().anchoredPosition;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("piece" + prefabID);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && beingDragged)
        {
            transform.Rotate(new Vector3(0,0,-90));
        }
        if(transform.hasChanged && !beingDragged)
        {
            transform.hasChanged = false;
            lastPosition = rectTransform.anchoredPosition;
        }
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
