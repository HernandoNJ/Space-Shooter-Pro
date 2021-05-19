using UnityEngine;

namespace EnemyLib
{
    public abstract class Enemy : MonoBehaviour, IDamage, IMove, IShoot
    {
        public EnemyData enemyData;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] protected int currentHealth;
        private EnemyType enemyType;
        private float speed;
        private int health;
        private int fireRate;


        // TODO Define shield in SO
        // todo *** Inventory Items in SO for shield, weapons, explosion

        private void Start()
        {
            SetEnemyValues(enemyData);

        }

        private void Update()
        {
            Move(speed);
            FireWeapon(fireRate);
            TestClickDamage();

        }

        private void OnDisable()
        {
            enemyData.updatedHealth = currentHealth;
        }

        private void SetEnemyValues(EnemyData _data)
        {
            currentHealth = _data.maxHealth;
            speed = _data.speed;
            health = _data.maxHealth;
            enemyType = _data.enemyType;
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }
            
        public virtual void Move(float speed)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // TODO CheckBottomPosition();
        }
        public virtual void FireWeapon(int firingRate)
        {

        }

        public void TestClickDamage()
        {
            if (Input.GetMouseButtonDown(0)) 
                TakeDamage(2);

            if (Input.GetMouseButtonDown(1))
                TakeDamage(5);
        }

    }











}

/*

TODO *** Create blog explaining how to create enemies. First, showing how it would be to create each enemy from scratch. Later, explaining modularization through inheritance.

TODO *** Implement interfaces in children

TODO Implement events, Code to customize enemy in inspector
TODO create a Laser base class for laser, doubleLaser and Backwards Laser
TODO raycast for enemy and backward laser chasing player

DONE use enums to populate parameters in ConfigureEnemy.For example: ConfigureEnemy(EnemyType enemyType).  *** if (enemyType == EnemyType.Basic) ==> Set basic Enemy values

DONE*** create a way to make a new Enemy with EnemyType.Default, which settings can be set in inspector


*/


// SetWeapon(EnemyWeapon weapontype)
// if (weapontype = EnemyWeapon.Laser) weaponItem = Laser

// Dictionary weapons Laser, DoubleLaser, BackwardLaser, Rocket
// if boss, create a weapon prefabs array
// Require component: rigidbody, 

// ASK how to edit a line of code in multiple files, for example, namespace EnemyLib?
// TODO set values for each enemytype
// TODO create weapons game objects
// TODO make a weapons dictionary
// TODO create a miscellaneous items dictionary (explosion, shield)
// TODO create a doc in the project to explain how to create a new enemy with hardcode or in editor. if in editor, new empty, attach EnemyDefault script, set values, save as prefab


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


speed = moveSpeed;
transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);

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
