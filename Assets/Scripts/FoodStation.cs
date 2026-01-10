using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FStationStatus
{
    Empty,
    PreparingFood,
    Ready
}

public class FoodStation : MonoBehaviour
{
    [SerializeField] public FStationStatus fstatus;
    [SerializeField] public GameObject dish;
    public FoodManager foodManager;

    void Start()
    {
        fstatus = FStationStatus.Empty;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fstatus == FStationStatus.Empty)
        {
            collision.GetComponent<EmployeeMovement>().status = Status.Busy;
            StartCoroutine(PreparingFood());
        }
    }

    IEnumerator PreparingFood()
    {
        fstatus = FStationStatus.PreparingFood;
        yield return new WaitForSeconds(6f);
        foodManager.MakeFood(this);
        fstatus = FStationStatus.Ready;
    }

    // Not working, think it may have something to do with the colliders

    void Update()
    {
        
    }
}
