using System;
using Interfaces;
using UnityEngine;
using Player = PlayerNS.Player;

namespace Weapon
{
public class RocketEnemy : WeaponBase, IDamageable
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D rb;

    public static event Action OnLookingForPlayer;

    protected override void OnEnable()
    {
        Player.OnSendPlayerPosition += SetTargetPosition;
        OnLookingForPlayer?.Invoke();

        rb = GetComponent<Rigidbody2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        parentName = weaponData.parentName;
    }

    private void OnDisable()
    {
        Player.OnSendPlayerPosition -= SetTargetPosition;
    }

    protected override void SetAdditionalValues()
    {
        transform.Rotate(0,0,180);
        Destroy(gameObject, weaponData.timeAlive);
    }

    private void SetTargetPosition(Transform targetTransform)
    {
        target = targetTransform;
    }

    protected override void Update()
    {
        directionToMove = (target.position - transform.position);
        directionToMove.Normalize();
        var rotationValue = Vector3.Cross(directionToMove, transform.up).z;
        rb.angularVelocity = -rotationValue * 500;
        rb.velocity = transform.up * weaponData.fireForce;
    }

    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
    }

}
}
