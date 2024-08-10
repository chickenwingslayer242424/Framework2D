using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private int score = 0; 
    private int collectedItemCount = 0; 
    private int totalItems = 5; 
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
        inventoryItems.Add(item); //Das Item zur Inventar-Liste hinzufügen
        collectedItemCount++;

        if (collectedItemCount >= totalItems)
        {
            ShowHighScorePanel();
        }
    }

    public void MarkItemAsFound(TMP_Text itemText)
    {
        // Itemtext durchstreichen
        itemText.fontStyle = FontStyles.Strikethrough;
        itemText.outlineWidth = 0.5f; 
    }

    private void ShowHighScorePanel()
    {
        FinalScorePanel.SetActive(true);
        EndScoreText.text = "Your Score: " + score;

        // Set initial position of the panel below the screen
        RectTransform panelRectTransform = FinalScorePanel.GetComponent<RectTransform>();
        panelRectTransform.anchoredPosition = new Vector2(0, -Screen.height);

        //Animate the panel to move up to the center of the screen
        panelRectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            Time.timeScale = 0f; //freezes game nach der Animation
        });
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadSceneAsync(0); // Load the main menu scene
    }

    public void CloseGame()
    {
        //Application.Quit(); //nutzen für build
        UnityEditor.EditorApplication.isPlaying = false; //schließt den editor
    }
}
