using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton bbg

    public Transform inventoryPanel; 
    public TMP_Text scoreText; 

    private int score = 0; //Punktestand
    private List<GameObject> inventoryItems = new List<GameObject>(); //liste zur Verwaltung der Inventargegenstände

    private void Awake()
    {
        if (Instance == null) //wenn es noch keine Instanz gibt:
        {
            Instance = this; //diese Instanz wird zur Singleton-Instanz
            DontDestroyOnLoad(gameObject); //nicht zerstören, wenn eine neue Szene geladen wird
        }
        else
        {
            Destroy(gameObject); //wenn es schon eine Instanz gibt, zerstöre diese!!!!
        }
    }

    public void UpdateScore(int amount)
    {
        score += amount; //punktestand um den übergebenen Betrag zu erhöhen
        scoreText.text = "Score: " + score; //aktualisert UI Punktestand
    }

    public void UpdateInventoryUI(GameObject item)
    {
        item.transform.SetParent(inventoryPanel, false); //Das Item zum Inventar-Panel hinzufügen
        inventoryItems.Add(item); //Das Item zur Inventar-Liste hinzufügen
    }
}
