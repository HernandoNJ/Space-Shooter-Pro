namespace EnemyLib
{
    public class EnemyDoubleShooter :Enemy
    {
        private void Start()
        {
            ConfigureEnemy(EnemyType.DoubleShooter, EnemyMove.Normal, EnemyWeapon.DoubleLaser, 1, 3f, 0.4f);
        }
      


    }
}
