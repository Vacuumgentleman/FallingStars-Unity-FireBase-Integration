using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float Score { get; private set; }
    public float TimePlayed { get; private set; }
    public int ObjectsInteracted { get; private set; }
    public int Attempts { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Score += Time.deltaTime;
        TimePlayed += Time.deltaTime;
    }

    public void RegisterObjectInteraction()
    {
        ObjectsInteracted++;
    }

    public void GameOver()
    {
        Attempts++;
        LoadEndScreen();
    }

    private void LoadEndScreen()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void ResetStats()
    {
        Score = 0;
        TimePlayed = 0;
        ObjectsInteracted = 0;
    }
}
