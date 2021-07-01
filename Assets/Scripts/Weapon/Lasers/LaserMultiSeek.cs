using Interfaces;
using UnityEngine;

namespace Weapon.Lasers
{
public class LaserMultiSeek : WeaponBase
{
    protected override void MoveWeapon()
    {
        transform.position += transform.up * (weaponData.fireForce *3 * Time.deltaTime);
        CheckBounds();
    }

    private float CalculateRotationAngle(Transform target)
    {
        var targetPos = target.position;
        var position = transform.position;

        var xDistance = targetPos.x - position.x;
        var yDistance = targetPos.y - position.y;

        var angle = Mathf.Atan2(yDistance, xDistance) * Mathf.Rad2Deg;
        return angle - 90;
    }

    private Collider2D FindNearestEnemy(Collider2D[] objects, Collider2D initialEnemy)
    {
        Collider2D nearest = null;
        foreach (var col in objects)
        {
            if (col.CompareTag("Enemy") && col != initialEnemy)
            {
                if (nearest == null) nearest = col;

                else if (Vector3.Distance(col.transform.position, transform.position) <
                         Vector3.Distance(nearest.transform.position, transform.position)) nearest = col;
            }
        }

        return nearest;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var iDamage = other.GetComponent<IDamageable>();
            var nearestObjects = Physics2D.OverlapCircleAll(transform.position, 100f);
            var nearest = FindNearestEnemy(nearestObjects, other);

            if (nearest != null)
            {
                float angle = CalculateRotationAngle(nearest.transform);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            iDamage?.TakeDamage(weaponData.damage);
        }
    }

    private void CheckBounds()
    {
        var pos = transform.position;
        if(pos.x > 10f || pos.x < -10f || pos.y > 5 || pos.y < -5 ) Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
}
