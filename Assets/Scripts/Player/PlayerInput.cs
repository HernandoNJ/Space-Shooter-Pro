using System;
using UnityEngine;

namespace Player
{
public class PlayerInput : MonoBehaviour
{
    public float Horizontal{ get; private set; }
    public float Vertical{ get; private set; }
    public bool FireWeapon{ get; private set; }

    public event Action OnFireActive;

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        FireWeapon = Input.GetKeyDown(KeyCode.Space);

        if (FireWeapon) OnFireActive?.Invoke();
    }
}
}
