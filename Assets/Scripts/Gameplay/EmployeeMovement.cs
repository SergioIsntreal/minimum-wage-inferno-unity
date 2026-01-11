using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Status
{
    Idle,
    Walking,
    Busy
}

public class EmployeeMovement : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [SerializeField] public Status status;

    NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target.position = transform.position;
    }

    
    void Update()
    {
        agent.SetDestination(target.position);
    
        // May want to adjust the 'Order in Layer' as their Y position changes, so that they aren't
        // incorrectly clipping through objects
        // OR. If they're a prefab, I notice there are Layer Override settings...
    }
}
