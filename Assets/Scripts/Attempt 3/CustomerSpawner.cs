using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] GameObject Customer;

    [SerializeField] float spawnCountdown;
    [SerializeField] float timeToSpawn;
    
    void Start()
    {
        spawnCountdown = timeToSpawn;
    }

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if(spawnCountdown <= 0)
        {
            spawnCountdown = timeToSpawn;
            Spawn();
            // Unsure how to control WHERE these are spawning folder-wise - or if it matters at all
            
        }
    }

    void Spawn()
    {
        Instantiate(Customer, transform.position, transform.rotation);
        timeToSpawn = (Random.Range (15f, 30f));
    }
}
