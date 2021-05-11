using UnityEngine;

public class EnemyBasic : Enemy //IShot
{
    // ASK useless? It doesn't work in play mode
    // public EnemyBasic(EnemyType enemyType, int lives, float speed, float fireRate) : base(EnemyType.Basic, 4, speed, fireRate) { }

    private void Start()
    {
        ConfigureEnemy(EnemyType.Basic, EnemyMove.Normal, EnemyWeapon.Laser, 1,3f,0.15f);
    }

    protected override void MoveEnemy()
    {
        MoveNormal();
    }

}
