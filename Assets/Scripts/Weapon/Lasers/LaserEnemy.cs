using UnityEngine;

namespace Weapon.Lasers
{
public class LaserEnemy : WeaponBase
{
    protected override void SetAdditionalValues()
    {
        parentName = "Enemy";
        directionToMove = Vector3.down;
        Destroy(gameObject, weaponData.timeAlive);
    }

}
}
