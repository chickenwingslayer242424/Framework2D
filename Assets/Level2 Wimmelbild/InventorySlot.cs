using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
            // Set the dropped item as a child of the slot to make it part of the slot's hierarchy
            dropped.transform.SetParent(transform, false);
            dropped.transform.localPosition = Vector3.zero; // Ensure the dropped item snaps inside the slot
        }
    }
}
