using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [Header("Navigation")]
    private NavMeshAgent agent;
    private Chair targetChair;

    [Header("Settings")]
    [SerializeField] private float arrivalThreshold = 0.1f;
    [SerializeField] private float sitPatience = 5f;
    private float sitTimer = 0f;
    private bool isSitting = false;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer npcRenderer;
    [SerializeField] private Color walkingColor = Color.white;
    [SerializeField] private Color sittingColor = Color.gray;

    [Header("Exit Settings")]
    [SerializeField] private bool shouldLeaveAfterSitting = true;
    [SerializeField] private float destroyDelay = 1f;
    private bool isLeaving = false;
    private Transform exitPoint;

    void Start()
    {
        //Get components
        agent = GetComponent<NavMeshAgent>();

        //Configure for 2D
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        //Find and move to available chair
        FindAndMoveToChair();

        //Get exit from manager
        if (ExitManager.Instance != null)
        {
            exitPoint = ExitManager.Instance.GetExitPoint();
        }
        else
        {
            Debug.LogWarning("No ExitManager found. Creating exit manually.");
            FindExitPoint();
        }

        // Set initial visual
        UpdateVisual();
    }

    void Update()
    {
        if (targetChair == null && !isSitting && !isLeaving)
        {
            FindAndMoveToChair();
            return;
        }

        if (!isSitting && !isLeaving && targetChair != null)
        {
            CheckIfArrivedAtChair();
        }
        else if (isSitting)
        {
            //NPC is sitting
            sitTimer += Time.deltaTime;

            if (sitTimer >= sitPatience)
            {
                //Patience runs out, customer leaves
                if (shouldLeaveAfterSitting)
                {
                    LeaveCafe();
                }
                else
                {
                    GetUpFromChair();
                }
            }
        }
        else if (isLeaving)
        {
            //Check if exit is reached
            if (agent != null && !agent.pathPending && agent.remainingDistance <= arrivalThreshold)
            {
                ExitCafe();
            }
        }
    }

    void FindAndMoveToChair()
    {
        if (ChairManager.Instance != null)
        {
            targetChair = ChairManager.Instance.GetAvailableChair();

            if (targetChair != null && agent != null)
            {
                //Move to the chair's sit position
                Vector2 targetPosition = targetChair.SitPosition.position;
                agent.SetDestination(targetPosition);
            }
        }
    }

    void CheckIfArrivedAtChair()
    {
        if (targetChair == null || agent == null) return;

        //Check if they've reached the chair
        if (!agent.pathPending && agent.remainingDistance <= arrivalThreshold)
        {
            SitOnChair();
        }
    }

    void SitOnChair()
    {
        isSitting = true;
        sitTimer = 0f;

        //Stop navigating
        if (agent != null)
        {
            agent.isStopped = true;
        }

        //Snap to chair position
        transform.position = targetChair.SitPosition.position;

        //Notify the chair's now occupied
        targetChair.OccupyChair(this);

        UpdateVisual();

        Debug.Log($"{gameObject.name} is now waiting.");
    }

    void GetUpFromChair()
    {
        isSitting = false;

        //Notify ChairManager
        if (targetChair != null)
        {
            ChairManager.Instance.ReleaseChair(targetChair);
            targetChair = null;
        }

        //Resume navigation
        if (agent != null)
        {
            agent.isStopped = false;
        }

        UpdateVisual();

        //Leave the cafe after delay (ASK AI)
        Invoke(nameof(FindAndMoveToChair), 1f);
    }

    void FindExitPoint()
    {
        // Simple fallback
        GameObject exitObj = GameObject.FindGameObjectWithTag("Exit");
        if (exitObj != null)
        {
            exitPoint = exitObj.transform;
        }
        else
        {
            //Creating exit from far left
            GameObject newExit = new GameObject("Exit");
            newExit.transform.position = new Vector3(-12, 0, 0);
            exitPoint = newExit.transform;
        }
    }

    void LeaveCafe()
    {
        isSitting = false;
        isLeaving = true;

        //Free up the chair
        if (targetChair != null)
        {
            ChairManager.Instance.ReleaseChair(targetChair);
            targetChair = null;
        }

        //Head to exit
        if (exitPoint != null && agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(exitPoint.position);

            //Update visual for leaving state
            if (npcRenderer != null)
            {
                npcRenderer.color = Color.magenta;
            }

            Debug.Log($"{gameObject.name} is leaving the cafe.");
        }
        else
        {
            //No exit found, just destroy
            ExitCafe();
        }
    }

    void ExitCafe()
    {
        Debug.Log($"{gameObject.name} has left the cafe.");

        //Destroy the NPC
        Destroy(gameObject, 0.5f);
    }

    void UpdateVisual()
    {
        if (npcRenderer != null)
        {
            npcRenderer.color = isSitting ? sittingColor : walkingColor;
        }
    }

    //For debugging
    private void OnDrawGizmos()
    {
        if (targetChair != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetChair.transform.position);
        }
    }
}
