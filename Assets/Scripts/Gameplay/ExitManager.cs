using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance;

    [SerializeField] private Transform exitPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            //Auto-find exit if not assigned
            if (exitPoint == null)
            {
                GameObject exitObj = GameObject.FindGameObjectWithTag("Exit");
                if (exitObj != null)
                {
                    exitPoint = exitObj.transform;
                }
                else
                {
                    // Create default exit
                    GameObject newExit = new GameObject("CafeExit");
                    newExit.transform.position = new Vector3(-12, 0, 0);
                    exitPoint = newExit.transform;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetExitPoint()
    {
        return exitPoint;
    }
}
