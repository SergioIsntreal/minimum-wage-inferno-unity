using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChairStatus
{
    Empty,
    Occupied
}

public class Chair : MonoBehaviour
{
    public ChairStatus status;
    private Color empty = Color.gray;
    private Color occupied = Color.cyan;
    private Collider2D col;
    private SpriteRenderer sr;
    private CustomerBehaviour customerBehaviour;

    void Start()
    {
        col = GetComponent<Collider2D>();
        if (status == ChairStatus.Occupied) { sr.color = occupied; }
        if (status == ChairStatus.Empty) { sr.color = empty; }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (status == ChairStatus.Empty)
        {
            collision.GetComponent<CustomerBehaviour>().cstatus = CustomerStatus.Sitting;
        }

    }
}
    // Current mission; have the customers move to the seats when spawned in, then change the chairs status
