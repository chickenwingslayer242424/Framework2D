using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag; //Parent des Items nach dem Ziehen
    public int scoreValue = 10; 
    private bool isPickedUp = false; //Checkt, ob das Item aufgenommen wurde
    private float timeTaken = 0f; //Die Zeit, die für das Ziehen des Items benötigt wird
    private CanvasGroup canvasGroup; //die CanvasGroup-Komponente des Items
    private Vector3 startPosition; //Item Starposition
    private Canvas canvas; //ItemCanvas

    public TMP_Text associatedText; // Der Text, der durchgestrichen werden soll
    public Sprite defaultSprite; // Standardbild, das im Slot verwendet werden soll

    public AudioClip dragSound; // Sound beim Starten des Ziehens
    public AudioClip dropSound; // Sound beim Ablegen im Slot
    private AudioSource audioSource;

    private void Awake() // Zugriff erteilen
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        canvas = GetComponentInParent<Canvas>();
        audioSource = GetComponent<AudioSource>(); // AudioSource-Komponente
    }

    private void Update()
    {
        if (!isPickedUp && parentAfterDrag == null) //wenn das Item gezogen wird und noch keinen neuen Parent hat
        {
            timeTaken += Time.deltaTime; //zeit für das Ziehen des Items erhöhen
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            parentAfterDrag = transform.parent; //setzt den aktuellen Parent des Items
            startPosition = transform.position; //speichert Startposition des Items
            transform.SetParent(canvas.transform); //setzt den Canvas als neuen Parent
            transform.SetAsLastSibling(); //setzt das Item als letztes Kind im Canvas, damit es oben angezeigt wird
            image.raycastTarget = false; //deaktiviert das Raycasting auf das Bild, um zu verhindern, dass es weitere Raycasting-Ereignisse empfängt
            canvasGroup.blocksRaycasts = false; //deaktiviert das Raycasting auf die CanvasGroup, damit man über andere UI-Elemente hinweg ziehen kann, ohne dass diese blockiert werden!!

            // Play drag sound
            if (audioSource != null && dragSound != null)
            {
                audioSource.PlayOneShot(dragSound);
            }
        }
    }

    public void OnDrag(PointerEventData eventData) //mouseposition wird in Worldspace umgerechnet
    {
        if (canvas.renderMode == RenderMode.WorldSpace)
        {
            //umwandlung der Bildschirmposition der Maus in Worldcoordinates
            Vector3 globalMousePos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out globalMousePos);
            transform.position = globalMousePos; //setzt die Position des Items auf die Mausposition
        }
        else
        {
            transform.position = Input.mousePosition; //setzt die Position des Items auf die Mausposition
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("InventorySlot")) //wenn das Item in einen Slot gezogen wurde, passiert folgendes:
        {
            transform.SetParent(eventData.pointerEnter.transform, false); //setzt den Slot als neuen Parent des Items
            transform.localPosition = Vector3.zero; //setzt die Position des Items im Slot zurück

            //setzt die Skalierung des Items auf 1, damit es korrekt im UI-Canvas angezeigt wird
            transform.localScale = Vector3.one;

            // Standardbild verwenden
            if (defaultSprite != null)
            {
                image.sprite = defaultSprite;
                image.color = Color.white; //setzt die Farbe auf weiß, um sicherzustellen, dass es nicht eingefärbt ist
            }

            //passt die Größe des Items an die Größe des Slots an -- sonst sieht es shit aus
            RectTransform slotRectTransform = eventData.pointerEnter.GetComponent<RectTransform>();
            RectTransform itemRectTransform = GetComponent<RectTransform>();
            itemRectTransform.sizeDelta = slotRectTransform.sizeDelta;

            int finalScore = Mathf.Max(1, scoreValue - Mathf.FloorToInt(timeTaken)); //berechnet die Punkte, die abgezogen werden sollen
            UIManager.Instance.UpdateScore(finalScore); //aktualisiert Punktestand

            // Item als gefunden markieren
            UIManager.Instance.MarkItemAsFound(associatedText); // Den zugehörigen Text durchstreichen

            // Play drop sound
            if (audioSource != null && dropSound != null)
            {
                audioSource.PlayOneShot(dropSound);
            }

            isPickedUp = true;
            this.enabled = false; //deaktiviert das Skript, damit das Item nicht erneut gezogen werden kann
        }
        else
        {
            transform.SetParent(parentAfterDrag); //setzt ursprünglichen Parent des Items
            transform.position = startPosition; //setzt Position des Items zurück
        }

        image.raycastTarget = true; //aktiviert Raycasting aufs Bild
        canvasGroup.blocksRaycasts = true; //aktiviert Raycasting auf die CanvasGroup
    }
}
