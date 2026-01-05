using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] EmployeeMovement employee;
    [SerializeField] StoreManager storeManager;
    [HideInInspector] public Transform navPoint;

    void Start()
    {
        navPoint = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        storeManager.Clicked(transform);
        Debug.Log("Clicked");
        //employee.target = navPoint.transform;
        //employee.target = transform;
    }
}
