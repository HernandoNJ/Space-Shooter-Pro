using System;
using UnityEngine;

namespace Others
{
[RequireComponent(typeof(PlayerNS.PlayerInput))]
public class SpeedBooster : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    
    private PlayerNS.PlayerInput playerInput;
    
    private float lastThrust = float.MinValue;
    
    public event Action<float> OnThrustChanged;

    private void Awake()
    {
        playerInput = GetComponent<PlayerNS.PlayerInput>();
    }

    private void Update()
    {
        if(lastThrust != playerInput.Vertical)
            OnThrustChanged(playerInput.Vertical);
        
        lastThrust = playerInput.Vertical;
        
        transform.position += transform.forward * (Time.deltaTime * playerInput.Vertical * speed);
        transform.Rotate(Vector3.up * (playerInput.Horizontal * turnSpeed * Time.deltaTime));
    }
}
}
