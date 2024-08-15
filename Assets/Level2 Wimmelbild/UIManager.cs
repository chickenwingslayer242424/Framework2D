using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton bbg

    public Transform inventoryPanel;
    public TMP_Text scoreText;
    public GameObject FinalScorePanel;
    public TMP_Text EndScoreText;
    public GameObject confettiEffect;

    private int score = 0; 
    private int collectedItemCount = 0; 
    private int totalItems = 5; 
    private List<GameObject> inventoryItems = new List<GameObject>(); //liste zur Verwaltung der Inventargegenstände

    private void Awake()
    {
        if (Instance == null) //wenn es noch keine Instanz gibt:
        {
            Instance = this; // diese Instanz wird zur Singleton-Instanz
        }
        else
        {
            Destroy(gameObject); //wenn es schon eine Instanz gibt, zerstöre diese!!!!
            return;
        }
    }

    private void Start()
    {
        ResetGame(); //resets game state at the start -- merk dir das Feyza
    }

    public void UpdateScore(int amount)
    {
        if (scoreText == null)
        {
            Debug.LogWarning("ScoreText is not assigned.");
            return;
        }

        score += amount; // Punktestand um den übergebenen Betrag zu erhöhen
        scoreText.text = "Score: " + score; // aktualisiert UI Punktestand
    }

    public void UpdateInventoryUI(GameObject item)
    {
        if (inventoryItems == null)
        {
            Debug.LogWarning("InventoryItems is not initialized.");
            return;
        }

        inventoryItems.Add(item); // Das Item zur Inventar-Liste hinzufügen
        collectedItemCount++;

        if (collectedItemCount >= totalItems)
        {
            ShowHighScorePanel();
        }
    }

    public void MarkItemAsFound(TMP_Text itemText)
    {
        if (itemText == null)
        {
            Debug.LogWarning("ItemText is not assigned.");
            return;
        }

        // Itemtext durchstreichen
        itemText.fontStyle = FontStyles.Strikethrough;
        itemText.outlineWidth = 0.5f; 
    }

    private void ShowHighScorePanel()
    {
        if (FinalScorePanel == null || EndScoreText == null)
        {
            Debug.LogWarning("FinalScorePanel or EndScoreText is not assigned.");
            return;
        }

        FinalScorePanel.SetActive(true);
        EndScoreText.text = "Your Score: " + score;

        // Set initial position of the panel below the screen
        RectTransform panelRectTransform = FinalScorePanel.GetComponent<RectTransform>();
        panelRectTransform.anchoredPosition = new Vector2(0, -Screen.height);

        //Animates the panel to move up to the center of the screen
        panelRectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce).OnComplete(() => //Panelanimation
        {
            StartCoroutine(DelayedFreeze()); //starts coroutine to delay the freezing of the game
        });

        // Activate confetti effect
        if (confettiEffect != null)
        {
            confettiEffect.SetActive(true);
        }
    }

    private IEnumerator DelayedFreeze()
    {
        yield return new WaitForSeconds(1f); //delay after freeze from GameFinishedPanel
        Time.timeScale = 0f; 
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; //resumes the game
        SceneManager.LoadSceneAsync(0); 
        ResetGame(); //reset game state before loading main menu
    }

    public void CloseGame()
    {
        //Application.Quit(); //nutzen für build
        UnityEditor.EditorApplication.isPlaying = false; //schließt den editor
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); //reload the current scene
        ResetGame(); //resets game state before reloading the scene
    }

    private void ResetGame()
    {
        score = 0;
        collectedItemCount = 0;
        inventoryItems.Clear();
        if (FinalScorePanel != null)
        {
            FinalScorePanel.SetActive(false);
        }
        if (confettiEffect != null)
        {
            confettiEffect.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}
