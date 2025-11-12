using Firebase.Extensions;
using Firebase.Firestore;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Firestore : MonoBehaviour
{
    FirebaseFirestore db;
    async void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        await AddDataToCollection();
        await ReadAllData();
    }

    public async Task AddDataToCollection()
    {
        DocumentReference docRef = db.Collection("users").Document("memo");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "Name", "Memo" },
            { "Lastname", "Reyes" },
            { "Born", 2000 }
        };

        await docRef.SetAsync(user);

        Debug.Log("Added Document to firebase");
    }

    public async Task ReadAllData()
    {
        CollectionReference usersRef = db.Collection("users");
        QuerySnapshot snapshot = await usersRef.GetSnapshotAsync();

        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            Debug.Log($"User: {document.Id}");

            Dictionary<string, object> userData = document.ToDictionary();

            if (userData.TryGetValue("Name", out var name))
                Debug.Log($"Name: {name}");

            if (userData.TryGetValue("Lastname", out var lastname))
                Debug.Log($"Lastname: {lastname}");

            if (userData.TryGetValue("Born", out var born))
                Debug.Log($"Year: {born}");
        }

        Debug.Log("Readed all info");
    }

    public async Task ReadOneDocument()
    {
        DocumentReference usersRef = db.Collection("users").Document("memo");
        DocumentSnapshot snapshot = await usersRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            Debug.Log($"User: {snapshot.Id}");

            Dictionary<string, object> userData = snapshot.ToDictionary();

            if (userData.TryGetValue("Name", out var name))
                Debug.Log($"Name: {name}");

            if (userData.TryGetValue("Lastname", out var lastname))
                Debug.Log($"Lastname: {lastname}");

            if (userData.TryGetValue("Born", out var born))
                Debug.Log($"Year: {born}");
        }

        Debug.Log("Readed one document info");
    }
}
 