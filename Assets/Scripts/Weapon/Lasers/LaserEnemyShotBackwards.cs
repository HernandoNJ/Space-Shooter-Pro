using UnityEngine;

namespace Weapon.Lasers
{
public class LaserEnemyShotBackwards : WeaponBase
{
    protected override void SetAdditionalValues()
    {
        base.SetAdditionalValues();
        directionToMove = Vector3.up;
    }
}
}
