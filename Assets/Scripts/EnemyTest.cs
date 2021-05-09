public class EnemyTest : EnemyBase
{
    public EnemyTest(EnemyType enemyType, EnemyMove enemyMove, EnemyWeapon enemyWeapon) : base(enemyType, enemyWeapon, enemyMove)
    {
        enemyType = EnemyType.Basic;
        enemyMove = EnemyMove.Normal;
        enemyWeapon = EnemyWeapon.Laser;
    }


    // public EnemyTest(EnemyTypes enemyType) : base(enemyType)
    // {
    // }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
