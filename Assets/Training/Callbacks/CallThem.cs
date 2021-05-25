using System;
using UnityEngine;

public class CallThem : MonoBehaviour
{
    public delegate int OnDel(int n);
    public static Action<int> onActionCalled;
    
    private void Start()
    {
        onActionCalled(500);

    }

    private void Update()
    {
        
    }
   
}
