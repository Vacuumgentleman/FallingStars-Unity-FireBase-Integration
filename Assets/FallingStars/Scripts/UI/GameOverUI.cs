using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Firebase")]
    [SerializeField] private FirebaseManager firebaseManager;

    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Button submitButton;

    [Header("Player Stats Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text interactionsText;

    [Header("UI State Changes")]
    [SerializeField] private GameObject[] activateOnSubmit;
    [SerializeField] private GameObject deactivateOnSubmit;

    private float finalScore;
    private float finalTime;
    private int finalInteractions;

    private void Start()
    {
        if (!ValidateReferences()) return;

        warningText.gameObject.SetActive(false);
        LoadPlayerStats();
        submitButton.onClick.AddListener(OnSubmit);
    }

    private bool ValidateReferences()
    {
        if (firebaseManager == null)
        {
            Debug.LogError("[GameOverUI] FirebaseManager reference missing.");
            return false;
        }

        if (nameInputField == null || warningText == null || submitButton == null)
        {
            Debug.LogError("[GameOverUI] Input/UI references missing.");
            return false;
        }

        if (scoreText == null || timeText == null || interactionsText == null)
        {
            Debug.LogError("[GameOverUI] Player stat texts missing.");
            return false;
        }

        return true;
    }

    private void LoadPlayerStats()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("[GameOverUI] GameManager instance not found.");
            return;
        }

        finalScore = GameManager.Instance.Score;
        finalTime = GameManager.Instance.TimePlayed;
        finalInteractions = GameManager.Instance.ObjectsInteracted;

        scoreText.text = $"Score: {finalScore:0}";
        timeText.text = $"Time: {finalTime:0.0}s";
        interactionsText.text = $"Interactions: {finalInteractions}";
    }

    private async void OnSubmit()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            warningText.gameObject.SetActive(true);
            return;
        }

        warningText.gameObject.SetActive(false);
        submitButton.interactable = false;

        await UploadPlayerDataAsync(playerName);

        HandleUIStateChange();

        submitButton.interactable = true;
    }

    private async Task UploadPlayerDataAsync(string playerName)
    {
        if (FirebaseManager.Instance == null)
        {
            Debug.LogError("[GameOverUI] FirebaseManager not found in scene.");
            return;
        }

        await FirebaseManager.Instance.SavePlayerDataAsync(
            playerName,
            Mathf.RoundToInt(finalScore),
            finalTime,
            finalInteractions
        );

        Debug.Log($"[GameOverUI] Player data uploaded for {playerName}.");
    }

    private void HandleUIStateChange()
    {
        if (activateOnSubmit != null)
        {
            foreach (var obj in activateOnSubmit)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
        }

        if (deactivateOnSubmit != null)
            deactivateOnSubmit.SetActive(false);
    }
}
