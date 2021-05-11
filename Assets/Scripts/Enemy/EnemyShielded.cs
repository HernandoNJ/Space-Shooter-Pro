public class EnemyShielded : Enemy
{
    private void Start()
    {
        ConfigureEnemy(EnemyType.Shielded, EnemyMove.Normal, EnemyWeapon.Laser, 2, 3f, 0.1f);
    }
    protected override void MoveEnemy()
    {
        MoveNormal();
    }


}
