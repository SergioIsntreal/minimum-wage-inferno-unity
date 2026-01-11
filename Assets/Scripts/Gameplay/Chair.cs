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
    private Color occupied = Color.blue;
    private Collider2D col;
    private SpriteRenderer sr;
    private CustomerBehaviour customerBehaviour;
    [HideInInspector] public Transform navPoint;

    private void Start()
    {
        status = ChairStatus.Empty;
    }

    void Update()
    {
        col = GetComponent<Collider2D>();
        sr = transform.GetComponent<SpriteRenderer>();
        if (status == ChairStatus.Empty) { sr.color = empty; }
        if (status == ChairStatus.Occupied) { sr.color = occupied; }
        customerBehaviour = FindAnyObjectByType<CustomerBehaviour>();
        navPoint = transform; 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            status = ChairStatus.Occupied;
        }
        // When the customer leaves the collision area, reset to empty
        // Currently turns chairs from blue to grey, just need to set the chair as the target position
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        status = ChairStatus.Empty;
    }


    // Above doesn't work; still editing it

    // Current mission; have the customers move to the seats when spawned in, then change the chairs status
}
