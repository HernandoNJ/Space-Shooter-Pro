namespace EnemyLib
{
    public class EnemyChaser :Enemy
    {

        private void Start()
        {
            ConfigureEnemy(EnemyType.Chaser, EnemyMove.ChasingPlayer, EnemyWeapon.Laser, 1, 3f, 0.15f);
        }
       


    }
}
