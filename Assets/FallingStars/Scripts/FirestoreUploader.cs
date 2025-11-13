using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FirestoreUploader : MonoBehaviour
{
    [Header("Firebase")]
    [SerializeField] private FirebaseFirestore firestore;

    [Header("Gameplay References")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameTimer gameTimer;

    [Header("UI References")]
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text statusText;

    private const string CollectionName = "PlayerScores";

    private void Awake()
    {
        ValidateReferences();
        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    private void ValidateReferences()
    {
        if (firestore == null)
        {
            firestore = FirebaseFirestore.DefaultInstance;
            if (firestore == null)
            {
                Debug.LogError("[FirestoreUploader] Firestore reference missing.");
            }
        }

        if (scoreManager == null)
            Debug.LogError("[FirestoreUploader] ScoreManager reference missing.");

        if (gameTimer == null)
            Debug.LogError("[FirestoreUploader] GameTimer reference missing.");

        if (playerNameInput == null || submitButton == null || statusText == null)
            Debug.LogError("[FirestoreUploader] UI references missing.");
    }

    private void OnSubmitClicked()
    {
        string playerName = playerNameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            statusText.text = "Enter your name.";
            return;
        }

        submitButton.interactable = false;
        statusText.text = "Uploading...";

        _ = UploadPlayerDataAsync(playerName);
    }

    private async Task UploadPlayerDataAsync(string playerName)
    {
        try
        {
            int finalScore = scoreManager != null ? scoreManager.GetFinalScore() : 0;
            float survivalTime = gameTimer != null ? gameTimer.GetElapsedTime() : 0f;

            var playerData = new
            {
                PlayerName = playerName,
                Score = finalScore,
                SurvivalTime = survivalTime,
                DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            await firestore.Collection(CollectionName).AddAsync(playerData);

            statusText.text = "Score uploaded!";
            Debug.Log($"[FirestoreUploader] Data uploaded for {playerName}: {finalScore} pts, {survivalTime:F1}s");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirestoreUploader] Upload failed: {ex.Message}");
            statusText.text = "Upload failed.";
        }
        finally
        {
            submitButton.interactable = true;
        }
    }
}
