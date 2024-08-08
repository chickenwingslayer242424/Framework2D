using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform inventoryPanel; 
    public TMP_Text scoreText; 

    private int score = 0; // player's score -- kommt später
    private List<GameObject> inventoryItems = new List<GameObject>(); // List to keep track of inventory items

    private void Awake()
    {
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

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void UpdateInventoryUI(GameObject item)
    {
        item.transform.SetParent(inventoryPanel, false);
        inventoryItems.Add(item);
        UpdateScore(10); // Example score -- kommt auch später
    }
}
