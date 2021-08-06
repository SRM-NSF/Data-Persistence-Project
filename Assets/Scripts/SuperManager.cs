using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SuperManager : MonoBehaviour
{
    public static SuperManager Instance;
    public TMP_InputField inputName;
    public string playerName;
    private string bestPlayerName;
    private int bestPoints;
    public TextMeshProUGUI bestScoreText;

    [System.Serializable]
    class SaveData
    {
        public int bestPoints;
        public string bestPlayerName;
    }


    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
        }
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile1.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPoints = data.bestPoints;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadBestPlayer();
        LoadBestScore();
        bestScoreText.text = "Best Score: " + bestPlayerName + ": " + bestPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        playerName = inputName.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
