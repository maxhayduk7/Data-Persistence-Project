using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TMP_InputField enterName;
    
    private void Start()
    {
        bestScore.text = GameData.Instance.GetBestScoreText();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void SetName()
    {
        var name = enterName.text;
        GameData.Instance.SetName(name);
    }
}
