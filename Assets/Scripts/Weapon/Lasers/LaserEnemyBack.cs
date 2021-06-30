using UnityEngine;
using Weapon;

public class LaserEnemyBack : WeaponBase
{
    protected override void SetAdditionalValues()
    {
        var scale = transform.localScale;
        scale.x = 1.5f;
        scale.y = 1.5f;
        directionToMove = Vector3.up;
        base.SetAdditionalValues();
    }

    // Note: size increased because it will be instantiated in scannerPoint - EnemyBackwards prefab
}
