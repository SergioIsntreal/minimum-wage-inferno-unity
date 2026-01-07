using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customer;
    public GameObject chosenCustomer;
    public GameObject[] shuffleCustomers;

    [HideInInspector] TimeUI time;

    [SerializeField] float spawnCountdown;
    [SerializeField] float timeToSpawn;
    
    void Start()
    {
        spawnCountdown = timeToSpawn;
        // Inspector requires someone to be predetermined in the 'Customer' field. Otherwise spawner breaks
    }

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if(spawnCountdown <= 0)
        {
        
            Spawn();
            customer = chosenCustomer;
            spawnCountdown = timeToSpawn;

            if (TimeManager.Hour >= 18)
            {
                spawnCountdown = 0;
                timeToSpawn = 0;
                return;
            }

            // Unsure how to control WHERE these are spawning folder-wise - or if it matters at all
        }

    }

    void Spawn()
    {
        timeToSpawn = (Random.Range (6f, 18f));
        Instantiate(customer, transform.position, transform.rotation);
        
        int index = Random.Range(0, shuffleCustomers.Length);
        chosenCustomer = shuffleCustomers[index];
        
    }

}
