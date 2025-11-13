using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Stats")]
    public float Score { get; private set; }
    public float TimePlayed { get; private set; }
    public int ObjectsInteracted { get; private set; }
    public int Attempts { get; private set; }

    [Header("References")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private ObjectSpawner spawner; 

    [Header("UI References")]
    [SerializeField] private GameObject[] uiToDeactivate;
    [SerializeField] private GameObject[] uiToActivate;
    [SerializeField] private RectTransform statsPanel;
    [SerializeField] private RectTransform statsPanelTargetPosition;

    private bool isGameOver;

    private void Awake()
    {
        InitializeSingleton();
    }

    private void Update()
    {
        if (isGameOver) return;

        Score += Time.deltaTime;
        TimePlayed += Time.deltaTime;
    }

    private void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterObjectInteraction()
    {
        if (isGameOver) return;
        ObjectsInteracted++;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        StopGameTime();
        StopSpawner(); 
        Attempts++;
        HandleUIState();
        MoveStatsPanel();
    }

    private void StopGameTime()
    {
        if (gameTimer != null)
            gameTimer.enabled = false;

        if (scoreManager != null)
            scoreManager.StopScore();
    }

    private void StopSpawner()
    {
        if (spawner != null)
        {
            spawner.CancelInvoke(); 
            spawner.enabled = false; 
        }
        else
        {
            var foundSpawner = FindObjectOfType<ObjectSpawner>();
            if (foundSpawner != null)
            {
                foundSpawner.CancelInvoke();
                foundSpawner.enabled = false;
            }
        }
    }

    private void HandleUIState()
    {
        foreach (var ui in uiToDeactivate)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        foreach (var ui in uiToActivate)
        {
            if (ui != null)
                ui.SetActive(true);
        }
    }

    private void MoveStatsPanel()
    {
        if (statsPanel == null || statsPanelTargetPosition == null)
            return;

        statsPanel.position = statsPanelTargetPosition.position;
    }

    public void ResetStats()
    {
        Score = 0f;
        TimePlayed = 0f;
        ObjectsInteracted = 0;
        isGameOver = false;

        if (gameTimer != null)
            gameTimer.enabled = true;

        if (scoreManager != null)
            scoreManager.ResetScore();

        if (spawner != null)
            spawner.enabled = true; 
    }
}
