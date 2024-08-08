using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            parentAfterDrag = transform.parent;
            startPosition = transform.position;
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) ////Mouseposition wird in Worldspace umgerechnet
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector3 globalMousePos);
            transform.position = globalMousePos; 
        }
        else
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag.GetComponent<InventorySlot>() != null)
        {
            //snap the item to the slot's position
            transform.SetParent(parentAfterDrag, false);
            transform.localPosition = Vector3.zero; //ensure the dropped item snaps inside the slot
            //Disable further dragging -- wenn in inventar soll es nicht mehr bewegt werden
            this.enabled = false;
        }
        else
        {
            //Return to original position if not dropped in a slot
            transform.SetParent(parentAfterDrag);
            transform.position = startPosition;
        }

        image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }
}
