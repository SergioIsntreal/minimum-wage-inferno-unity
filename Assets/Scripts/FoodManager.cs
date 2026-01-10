using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public GameObject[] menu;

    void Start()
    {
        
    }

    public void ShowOrder(Table table)
    {
        // The index picks a number from the menu, each game object being assigned a number in the array
        int index = Random.Range(0, menu.Length);
        // Instantiate allows it to run multiple times
        Instantiate(menu[index], table.transform.parent.GetChild(1).transform.position, Quaternion.identity);
        table.orderIndex = index;
    }

    public void MakeFood(FoodStation foodStation)
    {
        Instantiate(GetComponent<FoodStation>().dish, foodStation.transform.position, Quaternion.identity);
    }
}
