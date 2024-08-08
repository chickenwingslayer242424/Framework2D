using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour //put the ItemTrigger on the PlayerBody not the Item
{
    public TMP_Text pointsText;
    private int pointsCounter;
    public GameObject itemPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && collision.gameObject.activeSelf)
        {
            Destroy(collision.gameObject);
            pointsCounter += 1;
            pointsText.text = "Points: " + pointsCounter;
            SpawnNewItem();

        }
    }
    private void SpawnNewItem()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instantiate the new item at the random position
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }
    
}
