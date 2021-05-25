using UnityEngine;

public class CallAirplane : MonoBehaviour
{

    private void Start()
    {
        CallThem.onActionCalled += NewFunction;
        CallThem.onActionCalled(35);
    }

    private void NewFunction(int a)
    {
        var b = a * 4;
        Debug.Log("Airplane" + b);
    }
}
