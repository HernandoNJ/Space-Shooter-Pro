using System;
using UnityEngine;

namespace PlayerNS
{
public class PlayerInput : MonoBehaviour
{
    public float Horizontal{ get; private set; }
    public float Vertical{ get; private set; }
    public bool FireIsActive{ get; private set; }

    public static event Action OnFireActive;

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        FireIsActive = Input.GetKeyDown(KeyCode.Space);

        if (FireIsActive) OnFireActive?.Invoke();
    }
}
}
