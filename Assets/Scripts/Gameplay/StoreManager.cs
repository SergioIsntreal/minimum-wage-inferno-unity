using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] EmployeeMovement[] employees;
    [SerializeField] EmployeeMovement employeeMovement;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked(Transform item)
    {
        foreach (var employee in employees)
        {
            if (employee.status == Status.Idle)
            {
                employee.target = item.GetComponent<Click>().navPoint;
                item.transform.GetChild(0).GetComponent<Table>().employeeName = employee.name;
                employee.status = Status.Walking;
                return;

                //if (employeeMovement == item)
                //{
                //.status = Status.Idle;
                //}
                // Currently this does NOT reset their status to idle after they reach their destination, HOWEVER it does allow them to move one by one for some reason...
                // Alternatively, once the employee reaches the destination, a timer is started for the interaction (assuming the object state is available) this changes
                // the status to 'Busy'
            }


        }
    }
}
