using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MainMenu1()
    {
        SceneManager.LoadSceneAsync(0);

    }
    public void PlayGame1()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void PlayGame2()
    {
        SceneManager.LoadSceneAsync(2);
    }
     public void PlayGame3()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void CloseGame()
    {
        //Application.Quit(); //nutzen für build
        UnityEditor.EditorApplication.isPlaying = false; //schließt den editor
    }
}
