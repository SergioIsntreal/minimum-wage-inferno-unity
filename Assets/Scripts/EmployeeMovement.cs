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

    public int moveSpeed;
    [SerializeField] public Transform target;
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
    
    }
}
