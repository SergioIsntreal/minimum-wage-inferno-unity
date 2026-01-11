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
    [HideInInspector] public ChairStatus status;
    NavMeshAgent agent;
    public float moveSpeed = 2f;
    private Chair chair;
    private object getposition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        chair = FindAnyObjectByType<Chair>();
        cstatus = CustomerStatus.Sitting;
    }

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //int index = Random.Range(0, chairs.Length);
        //transform.position = chairs[index].transform.position;
    }
        //if (cstatus == CustomerStatus.Sitting)
        //{
        //    transform.position += Vector3.down * Time.deltaTime;
        //}

}
// Current mission; have the customers move to the seats when spawned in, then change the chairs status
