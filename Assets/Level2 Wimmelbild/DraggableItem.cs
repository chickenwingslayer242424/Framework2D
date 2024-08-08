using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image; //Item Image 
    [HideInInspector] public Transform parentAfterDrag; //der Parent des Items nach dem Ziehen
    public int scoreValue = 10; 
    private bool isPickedUp = false; //checkt, ob das Item aufgenommen wurde
    private float timeTaken = 0f; // Die Zeit, die für das Ziehen des Items benötigt wird
    private CanvasGroup canvasGroup; // Die CanvasGroup-Komponente des Items
    private Vector3 startPosition; // Die Startposition des Items
    private Canvas canvas; // Der Canvas, auf dem das Item ist

    private void Awake() //zugriff erteilen
    {
        canvasGroup = GetComponent<CanvasGroup>(); 
        image = GetComponent<Image>(); 
        canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (!isPickedUp && parentAfterDrag == null) //wenn das Item gezogen wird und noch keinen neuen Parent hat
        {
            timeTaken += Time.deltaTime; //Zeit für das Ziehen des Items erhöhen
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            parentAfterDrag = transform.parent; //setzt den aktuellen Parent des Items
            startPosition = transform.position; // Speichere die Startposition des Items
            transform.SetParent(canvas.transform); // Setzt den Canvas als neuen Parent
            transform.SetAsLastSibling(); //setzt das Item als letztes Kind im Canvas, damit es oben angezeigt wird
            image.raycastTarget = false; //deaktiviert das Raycasting auf das Bild  um zu verhindern, dass es weitere Raycasting-Ereignisse empfängt!!
            canvasGroup.blocksRaycasts = false; //deaktiviere das Raycasting auf die CanvasGroup, damit man über andere UI-Elemente hinweg ziehen kann, ohne dass diese blockiert werden
        }
    }

    public void OnDrag(PointerEventData eventData) // Mouseposition wird in Worldspace umgerechnet
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector3 globalMousePos);
            transform.position = globalMousePos; //setzt die Position des Items auf die Mausposition
        }
        else
        {
            transform.position = Input.mousePosition; //setzt die Position des Items auf die Mausposition
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag.GetComponent<InventorySlot>() != null) //Wenn das Item in einen Slot gezogen wurde, passiert folgendes:
        {
            transform.SetParent(parentAfterDrag, false); //setzt den Slot als neuen Parent des Items
            transform.localPosition = Vector3.zero; //setzt die Position des Items im Slot zurück
            int finalScore = Mathf.Max(1, scoreValue - Mathf.FloorToInt(timeTaken)); //berechnet die Punkte, die abgezogen werden sollen
            UIManager.Instance.UpdateScore(finalScore); //aktualisiert Punktestand
            isPickedUp = true; 
            this.enabled = false; //deaktiviert das Skript, damit das Item nicht erneut gezogen werden kann
        }
        else
        {
            transform.SetParent(parentAfterDrag); // Setze den ursprünglichen Parent des Items
            transform.position = startPosition; // Setze die Position des Items zurück
        }

        image.raycastTarget = true; //aktiviert Raycasting aufs Bild
        canvasGroup.blocksRaycasts = true; //aktiviert Raycasting auf die CanvasGroup
    }
}
