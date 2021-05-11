public class EnemyShotAvoider : Enemy
{
    private void Start()
    {
        ConfigureEnemy(EnemyType.ShotAvoider, EnemyMove.AvoidingShot, EnemyWeapon.Laser, 1, 3f, 0.15f);
    }
    protected override void MoveEnemy()
    {
        // TODO implement
    }
}
