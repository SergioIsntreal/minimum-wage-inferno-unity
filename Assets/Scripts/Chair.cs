using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public bool chairOccupied = false;
    private Collider2D col;
    private SpriteRenderer sr;
    private CustomerBehaviour customerBehaviour;
    [HideInInspector] public Transform navPoint;

    private void Start()
    {
        chairOccupied = false;
    }

    void Update()
    {
        col = GetComponent<Collider2D>();
        sr = transform.GetComponent<SpriteRenderer>();
        if(chairOccupied == true)
        {
            sr.color = Color.blue;
        }
        if (chairOccupied == false)
        {
            sr.color = Color.grey;
        }
        customerBehaviour = FindAnyObjectByType<CustomerBehaviour>();
        navPoint = transform; 
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            chairOccupied = true;
        }
        
        // Currently turns chairs from blue to grey, just need to set the chair as the target position
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        chairOccupied = false;
    }

    // Chair is a bool, Table is not, because it has 3 states, not 2
    // Current mission; have the customers move to the seats when spawned in, then change the chairs status
}
