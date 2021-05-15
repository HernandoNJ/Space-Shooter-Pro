using EnemyLib;

public class TestEnemy2 : Enemy
{
    private void Start()
    {
        ConfigureEnemy(EnemyType.Shielded, EnemyMove.ZigZag, EnemyWeapon.Rocket, 3, 5, 2.7f);

    }


}

