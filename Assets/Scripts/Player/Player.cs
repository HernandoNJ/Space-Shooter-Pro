using System;
using ScriptableObjects.Player;
using UnityEngine;

namespace Player
{
public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerInput moveInput;
    public float Speed{ get; private set; }

    public event Action<Collision2D> onCollisionPlayer;
    public event Action<Collider2D> onTriggerPlayer;

    private void Start()
    {
        // Set default position
        transform.position = Vector3.down * 3;
        Speed = playerData.speed * Time.deltaTime;
    }

    private void Update()
    {
        MovePlayer(moveInput.Horizontal, moveInput.Vertical, Speed);
    }

    private void MovePlayer(float x, float y, float speed)
    {
        transform.position += new Vector3(x * speed, y * speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        onCollisionPlayer?.Invoke(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerPlayer?.Invoke(other);
    }

}
}
