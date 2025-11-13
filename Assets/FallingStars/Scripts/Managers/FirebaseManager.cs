using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;

/// <summary>
/// Singleton Firebase manager that persists across scenes.
/// Ensures the GameObject is a root object before calling DontDestroyOnLoad.
/// </summary>
public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private FirebaseFirestore db;

    private void Awake()
    {
        InitializeSingleton();
        InitializeDatabase();
    }

    private void InitializeSingleton()
    {
        // If an instance already exists and it's not this, destroy this object.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Ensure this GameObject is a root object. If not, detach it.
        if (transform.parent != null)
        {
            transform.SetParent(null, worldPositionStays: true);
            Debug.Log("[FirebaseManager] Detached from parent to become a root GameObject.");
        }

        // Persist across scene loads
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeDatabase()
    {
        try
        {
            db = FirebaseFirestore.DefaultInstance;
            Debug.Log("[FirebaseManager] Firestore initialized.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirebaseManager] Failed to initialize Firestore: {ex.Message}");
        }
    }

    public async Task SavePlayerDataAsync(string playerName, int score, float survivalTime, int bonusCollected)
    {
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("[FirebaseManager] Player name is missing. Data not saved.");
            return;
        }

        if (db == null)
        {
            Debug.LogError("[FirebaseManager] Firestore is not initialized. Cannot save data.");
            return;
        }

        try
        {
            CollectionReference highscoresRef = db.Collection("Highscores");

            var playerData = new Dictionary<string, object>
            {
                { "PlayerName", playerName },
                { "Score", score },
                { "SurvivalTime", survivalTime },
                { "BonusCollected", bonusCollected },
                { "DateTime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") }
            };

            await highscoresRef.AddAsync(playerData);
            Debug.Log($"[FirebaseManager] Data saved successfully for {playerName}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirebaseManager] Failed to save data: {ex.Message}");
        }
    }

    public async Task<List<Dictionary<string, object>>> GetHighscoresAsync(int limit = 10)
    {
        var highscores = new List<Dictionary<string, object>>();

        if (db == null)
        {
            Debug.LogError("[FirebaseManager] Firestore is not initialized. Cannot retrieve highscores.");
            return highscores;
        }

        try
        {
            Query query = db.Collection("Highscores")
                            .OrderByDescending("Score")
                            .Limit(limit);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
                highscores.Add(doc.ToDictionary());
            }

            Debug.Log($"[FirebaseManager] Retrieved {highscores.Count} highscores.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[FirebaseManager] Failed to load highscores: {ex.Message}");
        }

        return highscores;
    }
}
