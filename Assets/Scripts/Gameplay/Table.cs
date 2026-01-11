using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableStatus
{
    Empty,
    Occupied,
    Dirty
}

public class Table : MonoBehaviour
{
    public TableStatus status;
    private Collider2D col;
    [HideInInspector] public string employeeName = "";
    private Color empty = Color.yellow;
    private Color occupied = Color.red;
    private Color dirty = Color.green;
    private SpriteRenderer sr;
    public FoodManager foodManager;
    [HideInInspector] public int orderIndex;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = transform.parent.GetComponent<SpriteRenderer>();
        if(status == TableStatus.Occupied) { sr.color = occupied; }
        if (status == TableStatus.Empty) { sr.color = empty; }
        if (status == TableStatus.Dirty) { sr.color = dirty; }
        foodManager = FindAnyObjectByType<FoodManager>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.name == employeeName)
        {
            if(status == TableStatus.Empty)
            {
                collision.GetComponent<EmployeeMovement>().status = Status.Idle;
                Debug.Log("Idle");
            }

            if (status == TableStatus.Occupied)
            {
                collision.GetComponent<EmployeeMovement>().status = Status.Busy;
                Debug.Log("Busy");

                StartCoroutine(Order());  
            }
        }
    }

    IEnumerator Order()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Order");
        foodManager.ShowOrder(this);
        yield return new WaitForSeconds(5f);
        GetComponent<EmployeeMovement>().status = Status.Idle;
    }
}
