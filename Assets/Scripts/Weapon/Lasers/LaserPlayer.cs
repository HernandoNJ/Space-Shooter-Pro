﻿using UnityEngine;

namespace Weapon.Lasers
{
public class LaserPlayer : WeaponBase
{
    protected override void SetAdditionalValues()
    {
        directionToMove = Vector3.up;
        Destroy(gameObject, weaponData.timeAlive);
    }
}
}
