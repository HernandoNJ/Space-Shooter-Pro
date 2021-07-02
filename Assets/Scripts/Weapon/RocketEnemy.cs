using System;
using Interfaces;
using UnityEngine;
using Player = PlayerNS.Player;

/*Vector3.RotateTowards - Unity
 https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
 */

namespace Weapon
{
public class RocketEnemy : WeaponBase
{
    [SerializeField] private Transform target;
    //[SerializeField] private LayerMask laserplayerMask;

    public static event Action OnLookingForPlayer;

    protected override void OnEnable()
    {
        Player.OnSendPlayerPosition += SetTargetPosition;
        OnLookingForPlayer?.Invoke();

        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        parentName = weaponData.parentName;
    }

    private void OnDisable()
    {
        Player.OnSendPlayerPosition -= SetTargetPosition;
    }

    private void SetTargetPosition(Transform targetTransform)
    {
        target = targetTransform;
        //laserplayerMask = 13;
    }

    protected override void MoveWeapon()
    {
        if (target == null) base.MoveWeapon();
        else
        {
            var targetPos = target.position;
            var targetDirection = targetPos - transform.position;
            var reachSpeed = weaponData.fireForce * Time.deltaTime; // calculate distance to move

            transform.position = Vector3.MoveTowards(transform.position, targetPos, reachSpeed); // it works without pos = v3.moveTowards
            transform.up = targetDirection; // Look at target with transform.up
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(parentName) || other.CompareTag("LaserEnemy"))
        {
            Debug.LogWarning($"IDamageable in ... {other.name} is null or other tag is... {other.tag}");
            Debug.Log("this object: " + gameObject.name + "... has parent name: " + parentName);
            return;
        }

        if (other.CompareTag("Player") || other.CompareTag("LaserPlayer"))
        {
            var iDamage = other.GetComponent<IDamageable>();
            iDamage?.TakeDamage(weaponData.damage);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
}
