using System;
using System.Collections.Generic;
using System.Linq;
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
        // Usamos el método actual que solo toma limit
        var allPlayers = await firebaseManager.GetHighscoresAsync(50); // Trae más para filtrar localmente

        if (allPlayers == null || allPlayers.Count == 0)
        {
            Debug.Log("[LeaderboardUI] No players found in Firestore.");
            return;
        }

        // Filtramos solo los que estén en la colección correcta (PlayerScores)
        var topPlayers = allPlayers
            .Where(p => p.ContainsKey("PlayerName") && p.ContainsKey("Score") && p.ContainsKey("SurvivalTime"))
            .Select(p => new PlayerData
            {
                Name = GetString(p, "PlayerName"),
                Score = GetDouble(p, "Score"),
                Time = GetDouble(p, "SurvivalTime"),
                Bonus = GetDouble(p, "BonusCollected")
            })
            .OrderByDescending(p => p.Score)
            .ThenByDescending(p => p.Time)
            .Take(3)
            .ToList();

        Debug.Log("[LeaderboardUI] Top players fetched from Firestore:");
        for (int i = 0; i < topPlayers.Count; i++)
        {
            var player = topPlayers[i];
            Debug.Log($"Player {i + 1}: Name={player.Name}, Time={player.Time}, Score={player.Score}, Bonus={player.Bonus}");
        }

        for (int i = 0; i < nameTexts.Length; i++)
        {
            if (i < topPlayers.Count)
            {
                var player = topPlayers[i];
                nameTexts[i].text = player.Name;
                timeTexts[i].text = $"{player.Time:0.0}s";
                scoreTexts[i].text = ((int)player.Score).ToString();
                bonusTexts[i].text = ((int)player.Bonus).ToString();
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

    private string GetString(Dictionary<string, object> dict, string key) =>
        dict.ContainsKey(key) && dict[key] != null ? dict[key].ToString() : "-";

    private double GetDouble(Dictionary<string, object> dict, string key)
    {
        if (!dict.ContainsKey(key) || dict[key] == null) return 0;

        object value = dict[key];

        if (value is double d) return d;
        if (value is float f) return f;
        if (value is int i) return i;
        if (value is long l) return l;

        if (double.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double result))
            return result;

        return 0;
    }

    private class PlayerData
    {
        public string Name;
        public double Score;
        public double Time;
        public double Bonus;
    }
}
