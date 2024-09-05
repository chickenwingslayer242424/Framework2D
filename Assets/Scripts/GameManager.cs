using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour //put the ItemTrigger on the PlayerBody not the Item
{
    public TMP_Text pointsText;
    private int pointsCounter;
    public GameObject itemPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public TMP_Text endScoreText;
    public AudioClip collectSound;
    private AudioSource audioSource;
    public string playerName;
    public TMP_InputField playerNameInput;
    [SerializeField] private ParticleSystem pickUpParticle = default;
    //   public static GameManager instance;
    //    void Awake()
    // {
    //      if (instance == null)
    //     {
    //         instance = this;
    //     }
    //     else if (instance != this)
    //     {
    //         Destroy(gameObject);
    //     }

    //     DontDestroyOnLoad(gameObject);
    // }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        } // Get the AudioSource component
        if (collectSound != null)
        {
            audioSource.clip = collectSound; // Assign the audio clip if set in the Inspector
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && collision.gameObject.activeSelf)
        {
            ScoreCounter();
            Destroy(collision.gameObject);
            ParticleOnTrigger(collision);
            SpawnNewItem();
            PlayCollectSound();
            SaveHighScore();
        }

    }
    private void ParticleOnTrigger(Collider2D collision)
    {
        ParticleSystem instantiatedParticle = Instantiate(pickUpParticle, collision.transform.position, Quaternion.identity);
        instantiatedParticle.Play();
        Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
    }
    private void ScoreCounter()
    {
        pointsCounter += 1;
        pointsText.text = "Points: " + pointsCounter;
        endScoreText.text = "Your Score: " + pointsCounter;
    }
    //    public int GetScore()
    // {
    //     return pointsCounter;
    // }
    // public string GetName()
    // {
    //     return playerName;
    // }
    public void SavePlayerName()
    {
        playerName = playerNameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName);

    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("PlayerScore", pointsCounter);
    }
  
    private void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.Play(); // Play the assigned audio clip
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
