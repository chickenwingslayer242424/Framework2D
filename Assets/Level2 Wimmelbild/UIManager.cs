using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // Referenzen zu UI-Elementen
    public Transform inventoryPanel; // Das Panel, das die Inventargegenstände hält
    public TMP_Text scoreText; // TMP_Text-Element zur Anzeige der Punkte

    private int score = 0; // Die Punktzahl des Spielers
    private List<GameObject> inventoryItems = new List<GameObject>(); // Liste zur Verwaltung der Inventargegenstände

    void Awake()
    {
        // Sicherstellen, dass nur eine Instanz von UIManager existiert
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methode zur Aktualisierung der Punkteanzeige
    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    // Methode zum Hinzufügen eines Gegenstands zum Inventar-UI
    public void UpdateInventoryUI(GameObject item)
    {
        GameObject newItem = new GameObject(item.name);
        newItem.AddComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        newItem.transform.SetParent(inventoryPanel, false);
        inventoryItems.Add(newItem);
    }

    // Methode zum Leeren des Inventars
    public void ClearInventory()
    {
        foreach (GameObject item in inventoryItems)
        {
            Destroy(item);
        }
        inventoryItems.Clear();
    }
}
