using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) //Wenn der Slot leer ist
        {
            GameObject dropped = eventData.pointerDrag; //das gezogene Item
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); //Zugriff auf das DraggableItem-Skript
            draggableItem.parentAfterDrag = transform; //Setzt den Slot als neuen Parent des Items
            //Setzt das Item als Kind des Slots
            dropped.transform.SetParent(transform, false);
            dropped.transform.localPosition = Vector3.zero; //setzt die Position des Items im Slot zurück
        }
    }
}
