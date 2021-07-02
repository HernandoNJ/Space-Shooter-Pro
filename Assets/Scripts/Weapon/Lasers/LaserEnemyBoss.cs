using UnityEngine;

namespace Weapon.Lasers
{
public class LaserEnemyBoss : WeaponBase
{
    protected override void SetEnemyInitialValues()
    {
        parentName = weaponData.parentName;
    }

    protected override void MoveWeapon()
    {
        transform.Translate(Vector3.up * 3 * Time.deltaTime);
    }
}
}
