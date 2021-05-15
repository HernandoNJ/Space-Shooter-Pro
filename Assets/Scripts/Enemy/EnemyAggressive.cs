namespace EnemyLib
{
    public class EnemyAggressive :Enemy
    {

        private void Start()
        {
            ConfigureEnemy(EnemyType.Aggressive, EnemyMove.Aggressive, EnemyWeapon.Laser, 1, 3.5f, 0.1f);
        }

     


    }
}
