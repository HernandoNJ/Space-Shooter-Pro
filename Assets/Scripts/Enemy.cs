using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Declare only fields and methods common for all enemies
    // Additional behaviors like Shield, Shot and Damage must be implemented as interfaces

    public enum EnemyType { Default, Basic, DoubleShooter, Shielded, Aggressive, Chaser, BackShooter, ShotAvoider, Boss }
    public enum EnemyWeapon { Default, Laser, DoubleLaser, BackwardLaser, Rocket }
    public enum EnemyMove { Default, Normal, ZigZag, Aggressive, ChasingPlayer, AvoidingShot }

    /*

    Abstract:   what an enemy could have? properties, actions
                what actions and properties must be static? abstract (mandatory)?                
                what variables and actions must be shown in editor?
                can the method be implemented for several kinds of enemies?

    Interface:  the behaviour is a must.
                is the method/property unique for each enemy? 
                what behaviours will the enemy have? what properties will those behaviors require?

    Move: is an action, a behavior. Most Enemies must move. Define a method
    Weapon: is a part of the enemy, not an action. Define a field. 

    TODO Implement events, Code to customize enemy in inspector
    TODO create a Laser base class for laser, doubleLaser and Backwards Laser

    ASK Children constructors can be public, protected or private?

    */

    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected EnemyMove enemyMove;
    [SerializeField] protected EnemyWeapon enemyWeapon;
    [SerializeField] private float fireRate;
    [SerializeField] private float speed;
    [SerializeField] private int lives;
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Lives { get => lives; set => lives = value; }
    public float Speed { get => speed; set => speed = value; }

    // ASK useless in Unity?
    // public Enemy(EnemyType enemyType, int lives, float speed, float fireRate)
    // {
    //     //ConfigureEnemy(enemyType);
    //     Lives = lives;
    //     Speed = speed;
    //     FireRate = fireRate;
    // }

    protected void ConfigureEnemy(EnemyType type, EnemyMove move, EnemyWeapon weapon, int livesAmount, float speedAmount, float fireRateAmount)
    {
        enemyType = type;
        enemyMove = move;
        enemyWeapon = weapon;
        Lives = livesAmount;
        Speed = speedAmount;
        FireRate = fireRateAmount;
    }

    private void Start()
    {

    }

    protected abstract void MoveEnemy();
    protected virtual void MoveNormal()
    {
        transform.Translate(Vector3.down.normalized * Speed * Time.deltaTime);
    }

    // protected virtual void MoveZigZag() { }
    // protected virtual void MoveAggressive() { }
    // protected virtual void ShieldEnemy() { }
    // protected virtual void SetAgressive() { }
    // protected virtual void ChasePlayer() { }
    // protected virtual void ShootBack() { }
    // protected virtual void AvoidShot() { }
    protected virtual void Destroyed()
    { Destroy(gameObject); }

}






/*

explosionPrefab
shieldPrefab
laserPrefab
doubleLaserPrefab

isShieldActive
shieldStrength
spawnRate
lives
maxLives
damageAmount
damagePoints
fireRate
moveSpeed
isBoss
bossPositions


public enum EnemyMovementType 
{ 
Default, 
ZigZag,
Hover,
Follow,
Aggressive,
Patrol,
Panic,
ToAnchorPoint,
LeftRight,
Juke,
}

    In the start() function of EnemyMovement script, I put this:
8:59 AM
if (_enemy.GetEnemyType() == EnemyType.Infantry)
            InitInfantryMovementType();
        else if (_enemy.GetEnemyType() == EnemyType.Assault)
            InitAssaultMovementType();
        else if (_enemy.GetEnemyType() == EnemyType.Aggressor)
            InitAggressorMovementType();
9:00 AM
If the enemy is of type infantry or any other enemy type, then initialise a function that will choose a random movement type. (edited)
9:01 AM
so for instance, in the "InitInfatnryMovementType(), I put this:
9:01 AM
private void InitInfantryMovementType()
    {
        int randomIntValue = Random.Range(0, 2);
        if (randomIntValue == 0)
            _enemyMovementType = EnemyMovementType.Default;
        else if (randomIntValue == 1)
            _enemyMovementType = EnemyMovementType.ZigZag;
    }
9:02 AM
I created a random int value.If the random int value is 0, then I want the enemy of type Infantry to have the default movement type. if the random int is 1, I want the movement type to be zigzag.

GetEnemyType() returns the type of enemy. Here's what it is in code. Very simple.
public EnemyType GetEnemyType()
{
    return _enemyType;
}




private void Movement(){
    if(enemymoveType == EnemyMoveType.Default)
    transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);

    if(enemymoveType == EnemyMoveType.ZigZag)
    if(!canSwitch){ randomSide = Random.Range(1,3); canSwitch = true; }

    if(randomeSide == 1)
    transform.Translate((Vector3.down + _right).normalized * speed * Time.deltaTime);
    else if(randomeSide == 2)
    transform.Translate((Vector3.down + _left).normalized * speed * Time.deltaTime);

    if(enemymoveType == EnemyMoveType.Hover)
    if(transform.position.y >4f) { 
    transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);
    isHovering = true; }
    else if(isHovering) { 
    Vector3 newPos = new Vector3(transform.position.x, 4f,0);
    transform.position = newPos;
    StartCoroutine(HoverRoutine())
    }
    else transform.Translate(Vector3.down.normalized * hoverSpeed ...5... * Time.deltaTime
    }

    if (enemyMoveType == EnemyMove.Follow)
    {
        if (target == null) return;
        if (Vector3.Distance(transform.position,target.position) > 5f)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * time.DeltaTime);
    }

    if(enemyMove == EnemyMove.Aggressive) {
        isAggressive = true;
        transform.Translate(Vector3.down * (speed * 2) * Time.deltaTime);
    }
    else isAgressive = false;

    if(enemyMove == EnemyMove.Patrol){
        isPatrolling = true;
        Vector3 pos = transform.position;
        pos = Vector3.MoveTowards(transform.position, bossPositions[randomPoint].position, speed * Time.deltaTime);
        transform.position = pos;

        if(Vector3.Distance(pos, bossPositions[randomPoint].position)<0.2f)
        if(waitTime <=0)
            randomPoint = Random.Range(0, bossPositions.Length);
    }
}

*/
