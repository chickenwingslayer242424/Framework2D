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

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            parentAfterDrag = transform.parent;
            startPosition = transform.position;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag.GetComponent<InventorySlot>() != null)
        {
            // Snap the item to the slot's position
            transform.position = parentAfterDrag.position;
            // Disable further dragging
            this.enabled = false;
        }
        else
        {
            // Return to original position if not dropped in a slot
            transform.SetParent(parentAfterDrag);
            transform.position = startPosition;
        }

        image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }
}
