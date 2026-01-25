using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerPrefab
{
    public GameObject prefab;
    [Range(0.1f, 10f)] public float spawnChance = 1f;
}

public class CustomerSpawner : MonoBehaviour
{
    [Header("Customer Prefabs")]
    [SerializeField] private CustomerPrefab[] customerPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxNPCs = 18;
    [SerializeField] private float minSpawnDelay = 8f;
    [SerializeField] private float maxSpawnDelay = 18f;

    [Header("Cafe Status")]
    [SerializeField] private bool cafeOpen = true;

    //Private variables
    private float spawnTimer = 0f;
    private float currentSpawnDelay;
    private int currentNPCs = 0;
    private float totalSpawnWeight = 0f;

    void Start()
    {
        //Calculate total spawn weight for random selection
        CalculateSpawnWeights();

        //Start the countdown immediately
        ResetSpawnTimer();
    }

    void Update()
    {
        //Only spawn if cafe is open
        if (!cafeOpen) return;

        //Update the timer
        spawnTimer -= Time.deltaTime;

        //Check if it's time to spawn
        if (spawnTimer <= 0)
        {
            SpawnNPC();
            spawnTimer = currentSpawnDelay;
        }
    }

    void CalculateSpawnWeights()
    {
        totalSpawnWeight = 0f;
        foreach (var customer in customerPrefabs)
        {
            totalSpawnWeight += customer.spawnChance;
        }
    }

    void ResetSpawnTimer()
    {
        //Generate a new random delay
        currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

        //Reset the timer
        spawnTimer = 0f;

        Debug.Log($"Next NPC in {currentSpawnDelay:F1} seconds");
    }

    GameObject GetRandomCustomerPrefab()
    {
        if (customerPrefabs.Length == 0)
        {
            Debug.LogError("No customer prefab assigned!");
            return null;
        }

        //If only one prefab, return it
        if (customerPrefabs.Length == 1)
            return customerPrefabs[0].prefab;

        //Weighted random selection
        float randomValue = Random.Range(0f, totalSpawnWeight);
        float currentWeight = 0f;

        foreach (var customer in customerPrefabs)
        {
            currentWeight += customer.spawnChance;
            if (randomValue <= currentWeight)
            {
                return customer.prefab;
            }
        }

        //Fallback
        return customerPrefabs[0].prefab;
    }

    //void TrySpawnNPC()
    //{
        //Check conditions before spawning
        //if (!CanSpawnNPC())
        //{
            //Reset timer and try again later
            //ResetSpawnTimer();
            //return;
        //}

        //SpawnNPC();
        //ResetSpawnTimer();
    //}

    bool CanSpawnNPC()
    {
        //Condition 1: Cafe must be open
        if (!cafeOpen)
        {
            Debug.Log("Can't spawn: Cafe is closed.");
            return false;
        }

        //Condition 2: Max NPC limit
        if (currentNPCs >= maxNPCs)
        {
            Debug.Log($"Can't spawn: Max NPCs reached ({currentNPCs}/{maxNPCs})");
            return false;
        }

        //Condition 3: Check if chairs are available
        if (AreAllChairsOccupied())
        {
            Debug.Log("Can't spawn: All chairs are occupied.");
            return false;
        }

        //Condition 4: Check if spawn point exists
        if (spawnPoint == null)
        {
            Debug.LogError("Can't spawn: No spawn point assigned!");
            return false;
        }

        return true;
    }

    bool AreAllChairsOccupied()
    {
        //Check via chair manager
        if (ChairManager.Instance != null)
        {
            return ChairManager.Instance.AvailableChairCount == 0;
        }

        //Fallback: find chairs manually
        Chair[] chairs = FindObjectsOfType<Chair>();
        if (chairs.Length == 0)
        {
            Debug.LogWarning("No chairs found in scene!");
            return false;
        }

        foreach (Chair chair in chairs)
        {
            if (!chair.IsOccupied)
                return false; //Found an empty chair
        }

        return true; //ALL chairs are occupied
    }

    void SpawnNPC()
    {
        GameObject prefabToSpawn = GetRandomCustomerPrefab();
        if (prefabToSpawn == null) return;

        //Create the NPC
        GameObject npc = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

        //Give it a random name
        npc.name = GenerateRandomName();

        //Increment NPC count
        currentNPCs++;

        ResetSpawnTimer();

        Debug.Log($"Spawned {npc.name}. Total NPCs: {currentNPCs}/{maxNPCs}");
    }

    string GenerateRandomName()
    {
        string[] names =
        {
            "Carl", "Ben", "Luis", "Emory", "Roland", "Marcus"
        };

        return names[Random.Range(0, names.Length)];
    }

    void ScheduleNextSpawn()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        spawnTimer = Time.time + delay;
        Debug.Log($"Next spawn scheduled in {delay:F1} seconds");
    }

    //Called when the NPC leaves
    public void OnNPCLeft()
    {
        currentNPCs = Mathf.Max(0, currentNPCs - 1);
        Debug.Log($"NPC left. Total NPCs: {currentNPCs}/{maxNPCs}");
    }

    //Public method to control cafe status
    public void OpenCafe()
    {
        cafeOpen = true;
        Debug.Log("The Flame & Fork Bistro is now OPEN!");

        ResetSpawnTimer();
    }

    public void CloseCafe()
    {
        cafeOpen = false;
        Debug.Log("The Flame & Fork Bistro is now CLOSED!");

        spawnTimer = 0f;
    }

    public void ToggleCafe()
    {
        cafeOpen = !cafeOpen;
        Debug.Log($"Cafe is now {(cafeOpen ? "OPEN" : "CLOSED")}");

        if (cafeOpen)
        {
            ResetSpawnTimer();
        }
        else
        {
            spawnTimer = 0f;
        }
    }

    //For debugging/UI
    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 28;

        GUI.Label(new Rect(10, 10, 250, 25),
            $"Cafe: {(cafeOpen ? "OPEN" : "CLOSED")}", style);

        GUI.Label(new Rect(10, 35, 250, 25),
            $"Customers: {currentNPCs}/{maxNPCs}");

        //Calculate time remaining
        float timeRemaining = Mathf.Max(0, currentSpawnDelay = spawnTimer);

        //Show countdown if cafe is open
        if (cafeOpen)
        {
            GUI.Label(new Rect(10, 60, 250, 25),
                $"Next spawn in: {timeRemaining:F1}s");
        }
        else
        {
            GUI.Label(new Rect(10, 60, 250, 25),
                     "Next spawn: CLOSED");
        }

        // Show chair status
        int availableChairs = GetAvailableChairCount();
        GUI.Label(new Rect(10, 85, 250, 25),
                 $"Available chairs: {availableChairs}");
    }

    int GetAvailableChairCount()
    {
        if (ChairManager.Instance != null)
        {
            return ChairManager.Instance.AvailableChairCount;
        }

        //Manual count
        Chair[] chairs = FindObjectsOfType<Chair>();
        int available = 0;
        foreach (Chair chair in chairs)
        {
            if (!chair.IsOccupied) available++;
        }
        return available;
    }
}
