using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager Instance;

    [SerializeField] private List<Chair> allChairs = new List<Chair>();
    private Queue<Chair> availableChairs = new Queue<Chair>();
    public int AvailableChairCount => availableChairs.Count;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //Find all chairs in the scene
            FindAllChairs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FindAllChairs()
    {
        // Find all Chair Objects in the scene
        Chair[] chairs = FindObjectsOfType<Chair>();
        allChairs.AddRange(chairs);

        //Initialize available chairs
        foreach (Chair chair in allChairs)
        {
            if (!chair.IsOccupied)
            {
                availableChairs.Enqueue(chair);
            }
        }
    }

    //Get an available chair for an NPC
    public Chair GetAvailableChair()
    {
        if (availableChairs.Count > 0)
        {
            Chair chair = availableChairs.Dequeue();
            chair.ReserveChair(); //Marks as reserved, but no occupied
            return chair;
        }
        return null; //No chairs available
    }

    //Called when an NPC leaves a chair
    public void ReleaseChair(Chair chair)
    {
        if (chair != null)
        {
            chair.VacateChair();
            availableChairs.Enqueue(chair);
        }
    }

    //For debugging: Show available chairs count
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), $"Available Chairs: {availableChairs.Count}");
    }
}
