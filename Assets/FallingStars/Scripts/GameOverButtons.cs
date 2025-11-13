using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private const int MainMenuSceneIndex = 0;

    private void Awake()
    {
        ValidateReferences();
        AssignButtonListeners();
    }

    private void ValidateReferences()
    {
        if (mainMenuButton == null)
            Debug.LogWarning("[GameOverButtons] Main Menu Button is not assigned.");

        if (quitButton == null)
            Debug.LogWarning("[GameOverButtons] Quit Button is not assigned.");
    }

    private void AssignButtonListeners()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    private void LoadMainMenu()
    {
        DestroyAllDontDestroyOnLoad();
        Debug.Log("[GameOverButtons] Loading Main Menu...");
        SceneManager.LoadScene(MainMenuSceneIndex);
    }

    private void QuitGame()
    {
        Debug.Log("[GameOverButtons] Quitting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Destroys all objects in the DontDestroyOnLoad scene
    /// </summary>
    private void DestroyAllDontDestroyOnLoad()
    {
        Scene dontDestroyScene = SceneManager.GetSceneByName("DontDestroyOnLoad");
        if (!dontDestroyScene.IsValid())
        {
            Debug.Log("[GameOverButtons] No DontDestroyOnLoad scene found.");
            return;
        }

        GameObject[] rootObjects = dontDestroyScene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            Destroy(obj);
        }

        Debug.Log("[GameOverButtons] All DontDestroyOnLoad objects destroyed.");
    }
}
