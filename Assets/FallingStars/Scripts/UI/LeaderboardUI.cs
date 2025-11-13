using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [Header("Firebase Reference")]
    [SerializeField] private FirebaseManager firebaseManager;

    [Header("Top 3 Player Texts")]
    [SerializeField] private TMP_Text[] nameTexts;
    [SerializeField] private TMP_Text[] timeTexts;
    [SerializeField] private TMP_Text[] scoreTexts;
    [SerializeField] private TMP_Text[] bonusTexts;

    private async void Start()
    {
        if (!ValidateReferences()) return;
        await LoadTop3Async();
    }

    private bool ValidateReferences()
    {
        if (firebaseManager == null)
        {
            Debug.LogError("[LeaderboardUI] FirebaseManager reference missing.");
            return false;
        }

        if (nameTexts == null || timeTexts == null || scoreTexts == null || bonusTexts == null)
        {
            Debug.LogError("[LeaderboardUI] TMP text references missing.");
            return false;
        }

        return true;
    }

    private async Task LoadTop3Async()
    {
        var topPlayers = await firebaseManager.GetHighscoresAsync(3);

        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i < topPlayers.Count)
            {
                var player = topPlayers[i];

                string name = player.ContainsKey("PlayerName") ? player["PlayerName"]?.ToString() ?? "-" : "-";
                float time = player.ContainsKey("SurvivalTime") ? Convert.ToSingle(player["SurvivalTime"]) : 0f;
                int score = player.ContainsKey("Score") ? Convert.ToInt32(player["Score"]) : 0;
                int bonus = player.ContainsKey("BonusCollected") ? Convert.ToInt32(player["BonusCollected"]) : 0;

                nameTexts[i].text = name;
                timeTexts[i].text = $"{time:0.0}s";
                scoreTexts[i].text = score.ToString();
                bonusTexts[i].text = bonus.ToString();
            }
            else
            {
                nameTexts[i].text = "-";
                timeTexts[i].text = "-";
                scoreTexts[i].text = "-";
                bonusTexts[i].text = "-";
            }
        }

        Debug.Log("[LeaderboardUI] Top 3 updated successfully from Firestore.");
    }
}
