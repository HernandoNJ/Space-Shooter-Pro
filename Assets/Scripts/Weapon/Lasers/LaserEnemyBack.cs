using Weapon;

public class LaserEnemyBack : WeaponBase
{
    protected override void SetEnemyInitialValues()
    {
        var scale = transform.localScale;
        scale.x = 1.5f;
        scale.y = 1.5f;
        base.SetEnemyInitialValues();
    }

    // Note: size increased because it will be instantiated in scannerPoint - EnemyBackwards prefab
}
