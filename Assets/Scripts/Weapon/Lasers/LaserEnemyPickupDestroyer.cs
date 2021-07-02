using UnityEngine;

namespace Weapon.Lasers
{
public class LaserEnemyPickupDestroyer : WeaponBase
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            return;
        }

        base.OnTriggerEnter2D(other);
    }
}
}
