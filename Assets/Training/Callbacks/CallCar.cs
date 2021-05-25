using UnityEngine;

public class CallCar : MonoBehaviour
{
    
    private void Start()
    {
        CallThem.onActionCalled += MoveVehicle;
        CallThem.onActionCalled(300);
    }

    private void MoveVehicle(int m)
    {
        Debug.Log("Car " + m);
    }
}
