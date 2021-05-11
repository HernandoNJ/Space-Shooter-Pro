public class EnemyChaser : Enemy
{

    private void Start()
    {
        // TODO checkout these parameters in all enemies
        ConfigureEnemy(EnemyType.Chaser, EnemyMove.ChasingPlayer, EnemyWeapon.Laser, 1, 3f, 0.15f);
    }
    protected override void MoveEnemy()
    {

    }


}
