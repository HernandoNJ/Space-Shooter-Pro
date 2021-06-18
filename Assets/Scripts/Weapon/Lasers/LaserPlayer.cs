namespace Weapon.Lasers
{
public class LaserPlayer : WeaponBase
{
    protected override void SetAdditionalValues()
    {
        parentName = "Player";
        Destroy(gameObject, weaponData.timeAlive);
    }
}
}
