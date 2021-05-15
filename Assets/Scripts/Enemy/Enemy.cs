using UnityEngine;

namespace EnemyLib
{
    public abstract class Enemy : MonoBehaviour, IDamage, IDestroy, IMove, IShield, IShoot
    {

        public enum EnemyType { Default, Basic, DoubleShooter, Shielded, Aggressive, Chaser, BackShooter, ShotAvoider, Boss }
        public enum EnemyWeapon { Default, Laser, DoubleLaser, BackwardLaser, Rocket }
        public enum EnemyMove { Default, Normal, ZigZag, Aggressive, ChasingPlayer, AvoidingShot }

        /*

        TODO Implement events, Code to customize enemy in inspector
        TODO create a Laser base class for laser, doubleLaser and Backwards Laser
        TODO raycast for enemy and backward laser chasing player

        INFO Abstract methods don't have implementation and must be overriden in children
        INFO Virtual method may be called and overriden, and may keep parent's method implementation

        */

        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyWeapon enemyWeapon;
        [SerializeField] private EnemyMove enemyMove;
        [SerializeField] private GameObject explosionPrefab;

        [SerializeField] private float fireRate;
        [SerializeField] private float speed;
        [SerializeField] private int lives;
        [SerializeField] private int maxLives;

        public float FireRate { get => fireRate; set => fireRate = value; }
        public int Lives { get => lives; set => lives = value; }
        public float Speed { get => speed; set => speed = value; }

        protected void ConfigureEnemy(EnemyType type, EnemyMove move, EnemyWeapon weapon, int maxLives, float speedAmount, float fireRateAmount)
        {
            enemyType = type;
            enemyMove = move;
            enemyWeapon = weapon;
            Lives = maxLives;
            Speed = speedAmount;
            FireRate = fireRateAmount;
        }

        private void Start()
        {
            lives = maxLives;
        }

        public virtual void Destroyed()
        {
            Destroy(gameObject);
            // TODO Set Explosion animation
        }
        public virtual void FireWeapon(int firingRate) { }
        public virtual void Move(float moveSpeed)
        {

        }
        public virtual void Shield(int shieldForce) { }
        public virtual void TakeDamage(int damage) { }

    }
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
