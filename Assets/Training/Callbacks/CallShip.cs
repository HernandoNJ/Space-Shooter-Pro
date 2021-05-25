using UnityEngine;

public class CallShip : MonoBehaviour
{

    private void Start()
    {
        CallThem.onActionCalled += _OnActionCalled;
        CallThem.onActionCalled(7);
    }
    private void _OnActionCalled(int n)
    {
        var a = n * 5;
        Debug.Log("Ship " + a);
    }

    private void Update()
    {

    }
}
