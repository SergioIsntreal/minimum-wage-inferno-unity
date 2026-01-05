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
        int index = Random.Range(0, menu.Length);
        Instantiate(menu[index], table.transform.parent.GetChild(1).transform.position, Quaternion.identity);
        table.orderIndex = index;
    }
}
