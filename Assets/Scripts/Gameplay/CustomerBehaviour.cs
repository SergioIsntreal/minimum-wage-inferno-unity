using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateMachine
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
    [SerializeField] public StateMachine currentState;
    NavMeshAgent agent;
    public float moveSpeed = 2f;
    private Chair chair;
    private object getposition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        chair = FindAnyObjectByType<Chair>();

        currentState = StateMachine.Sitting;
    }

    private void Update()
    {
        switch(currentState)
        {
            case StateMachine.Sitting:
                Sitting();
                break;
            case StateMachine.ReadyToOrder:
                ReadyToOrder();
                break;
            case StateMachine.OrderTaken:
                OrderTaken();
                break;
            case StateMachine.Eating:
                Eating();
                break;
            case StateMachine.BillPlease:
                BillPlease();
                break;
            case StateMachine.Leaving:
                Leaving();
                break;
        }

        // bool for specific conditions; all of these rely on different things, like their position
        // relateive to other objects (eg. if they're in a chair, at a table, if they've ordered etc.)

        if(currentState != StateMachine.Sitting)
        {
            currentState = StateMachine.Sitting;
        }
        else if(currentState != StateMachine.ReadyToOrder)
        {
            currentState = StateMachine.ReadyToOrder;
        }
        else if (currentState != StateMachine.OrderTaken)
        {
            currentState = StateMachine.OrderTaken;
        }
        else if (currentState != StateMachine.Eating)
        {
            currentState = StateMachine.Eating;
        }
        else if (currentState != StateMachine.BillPlease)
        {
            currentState = StateMachine.BillPlease;
        }
        else if (currentState != StateMachine.Leaving)
        {
            currentState = StateMachine.Leaving;
        }
        //transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        //int index = Random.Range(0, chairs.Length);
        //transform.position = chairs[index].transform.position;
    }
        //if (cstatus == CustomerStatus.Sitting)
        //{
        //    transform.position += Vector3.down * Time.deltaTime;
        //}

    void Sitting()
    {
        // if target location is empty, set target location to an empty chair
        // requires some way of verifying which chairs are taken and empty.
    }

    void ReadyToOrder()
    {
        // if 'current location == table' && 'bool food == false' (verifying food on table)
        // && 'bool employee == false' (verifying if an employee has spoken to them yet)
        // && No. of times served < 3 (don't want a customer eating forever due to bad rng)
        // engage first phase of ordering
    }

    void OrderTaken()
    {
        // if 'current location == table' && 'bool food == false' (verifying food on table)
        // && 'bool employee == true', randomise and display order
    }

    void Eating()
    {
        // if 'current location == table' && 'bool food == true' (verifying food on table)
        // countdown for customer eating their food, No. of times served ++ 1;
        // enable a 50/50 on whether they go to 'BillPlease' or 'ReadyToOrder'
    }

    void BillPlease()
    {
        // set target location = till
        // if employee is @ till, customer pays for food
    }

    void Leaving()
    {
        // set target location = door
        // once they reach a certain point, they despawn
    }
}
// Current mission; have the customers move to the seats when spawned in, then change the chairs status
