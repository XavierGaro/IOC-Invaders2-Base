using System.Collections;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnEventDelegate();
    public event OnEventDelegate OnLevelChange;

    
    [Header("Scenes")]
    [SerializeField] private string mainMenu = "MainMenu";
    [SerializeField] private string gameOver = "GameOver";
    [SerializeField] private float sceneLoadDelay = 1.5f;

    [Header("Levels")] 
    [SerializeField] private GameLevelsSO gameLevels;

    
    
    public static GameLevelsSO GameLevels
    {
        get { return _instance.gameLevels; }
    }


    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public static void LoadGame()
    {
        GameState.Instance.Reset();
        var level = Instance.gameLevels.Levels[0];

        LoadLevel(level.sceneName);
    }

    public static void LoadMainMenu()
    {
        LoadLevel(Instance.mainMenu);
    }

    public static void LoadGameOver()
    {
        AudioManager.Instance.StopTrack();
        LoadLevel(Instance.gameOver);
    }


    public static void LoadLevel(string sceneName)
    {
        Instance.DelayedLoadLevel(sceneName);
    }

    private void DelayedLoadLevel(string sceneName)
    {
        if (OnLevelChange != null) OnLevelChange();
        StartCoroutine(WaitAndLoad(sceneName, sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
    public static void QuitGame()
    {
        Application.Quit();
    }

}