using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CustomerStatus
{
    Sitting,
    ReadyToOrder,
    OrderTaken,
    Eating,
    BillPlease,
    Leaving
}

public class CustomerBehaviour : MonoBehaviour
{
    [SerializeField] public CustomerStatus cstatus;
    [SerializeField] public GameObject chair;
    NavMeshAgent agent;
    public float moveSpeed = 2f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //if (cstatus == CustomerStatus.Sitting)
        //{
        //    transform.position += Vector3.down * Time.deltaTime;
        //}
    }

}
// Current mission; have the customers move to the seats when spawned in, then change the chairs status
