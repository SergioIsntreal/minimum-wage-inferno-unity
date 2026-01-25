using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    [Header("Sit Position")]
    [SerializeField] private Transform sitPosition; //Child nodes
    private string sitPositionChildName = "SitPosition";

    [Header("Other")]
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private bool isReserved = false;
    //Visual Indicator Script
    [SerializeField] private SpriteRenderer chairRenderer;
    [SerializeField] private Color emptyColor = Color.green;
    [SerializeField] private Color occupiedColor = Color.red;
    [SerializeField] private Color reservedColor = Color.yellow;
    //Reference to the customer using the chair
    private NPCController currentNPC;

    void Start()
    {
        if (sitPosition == null)
        {
            FindSitPosition();
        }

        //Fallback to chair's transform if it's still null
        if (sitPosition == null)
        {
            sitPosition = transform;
            Debug.LogWarning($"No sit position found for chait {name}. Using chair position.");
        }

        UpdateVisual();
    }

    void FindSitPosition()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("SeatNode"))
            {
                sitPosition = child;
                return;
            }
        }
    }

    public bool IsOccupied => isOccupied;
    public bool IsReserved => isReserved;
    public bool IsAvailable => !isOccupied && !isReserved;
    public Transform SitPosition => sitPosition;

    public void ReserveChair()
    {
        if (!isOccupied)
        {
            isReserved = true;
            UpdateVisual();
        }
    }

    public void OccupyChair(NPCController npc)
    {
        isOccupied = true;
        isReserved = false;
        currentNPC = npc;
        UpdateVisual();
    }

    public void VacateChair()
    {
        isOccupied = false;
        isReserved = false;
        currentNPC = null;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (chairRenderer != null)
        {
            if (isOccupied)
                chairRenderer.color = occupiedColor;
            else if (isReserved)
                chairRenderer.color = reservedColor;
            else
                chairRenderer.color = emptyColor;
        }
    }

    // Draw gizmo for easy editing
    void OnDrawGizmos()
    {
        Gizmos.color = IsAvailable ? Color.green :
                      (IsReserved ? Color.yellow : Color.red);
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0.1f));

        if (sitPosition != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(sitPosition.position, 0.1f);
        }
    }
}
