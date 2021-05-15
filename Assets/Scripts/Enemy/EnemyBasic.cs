namespace EnemyLib
{
    public class EnemyBasic : Enemy //IShot
    {
        // ASK useless? It doesn't work in play mode
        // public EnemyBasic(EnemyType enemyType, int lives, float speed, float fireRate) : base(EnemyType.Basic, 4, speed, fireRate) { }

        protected void Start()
        {
            ConfigureEnemy(EnemyType.Basic, EnemyMove.Normal, EnemyWeapon.Laser, 1,3f,0.15f);
            //FireRate = 4f;
        }


    }
}
