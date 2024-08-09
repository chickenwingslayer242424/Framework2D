using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 10f; 
    public float zoomSpeed = 2f; 
    public float minZoom = 5f; 
    public float maxZoom = 15f; 
    public float edgeScrollBoundary = 10f; //Abstand vom Bildschirmrand, ab dem gescrollt wird

    public Vector2 minBounds; //minimale Grenzen der Kamerabewegung
    public Vector2 maxBounds; //maximale Grenzen der Kamerabewegung

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;//hauptkamera zuweisen. cinemachine, who?
    }

    private void Update()
    {
        HandleEdgeScrolling(); // Methode zum Scrollen der Kamera aufrufen
        HandleZoom(); // Methode zum Zoomen der Kamera aufrufen
    }

    private void HandleEdgeScrolling() //bewegt die Kamera, wenn sich die Maus in der Nähe des Bildschirmrands befindet
    {
        Vector3 pos = cam.transform.position; //Camera Position

        if (Input.mousePosition.x >= Screen.width - edgeScrollBoundary)//bewegt cam nach rechts
        {
            pos.x += scrollSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x <= edgeScrollBoundary)//links
        {
            pos.x -= scrollSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y >= Screen.height - edgeScrollBoundary)//oben
        {
            pos.y += scrollSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y <= edgeScrollBoundary)//unten yippie
        {
            pos.y -= scrollSpeed * Time.deltaTime;
        }

        //Kameraposition grenze setzen
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        cam.transform.position = pos; //Aktualisiert die Position der Kamera
    }

    private void HandleZoom() //zoomt die Kamera basierend auf dem Mausrad
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");//liest das Mausrad
        float size = cam.orthographicSize;//aktuelle Größe der Kamera
        size -= scroll * zoomSpeed; //Verändert die Größe basierend auf dem Mausrad
        size = Mathf.Clamp(size, minZoom, maxZoom); //setzt grenzen für den Zoom
        
        //Begrenzt maximale Zoomstufe basierend auf der Größe des Hintergrunds
        float maxZoomWidth = (maxBounds.x - minBounds.x) / 2f;
        float maxZoomHeight = (maxBounds.y - minBounds.y) / 2f;
        size = Mathf.Min(size, maxZoomWidth / cam.aspect, maxZoomHeight);

        cam.orthographicSize = size; //aktualisiert die Größe der Kamera
        
        // Kamera mittig halten
        Vector3 pos = cam.transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x + size * cam.aspect, maxBounds.x - size * cam.aspect);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + size, maxBounds.y - size);
        cam.transform.position = pos;
    }
}
