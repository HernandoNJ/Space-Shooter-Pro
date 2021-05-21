using System;
using UnityEngine;

namespace EnemyLib
{
    public abstract class Enemy : MonoBehaviour, ITakeDamage, IMove, IShoot
    {
        public EnemyData enemyData;
        [SerializeField] private int collisionDamage;
        [SerializeField] private int currentHealth;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject model;
        [SerializeField] private GameObject weapon;
        [SerializeField] private GameObject firePoint;
        [SerializeField] private bool isAlive;
        [SerializeField] private float speed;
        [SerializeField] private float fireRate;
        [SerializeField] private float timeToFire;

        public static Action onEnemyDestroyed;

        private void Start()
        {
            ConfigureEnemy(enemyData);
        }
        private void Update()
        {
            Move(speed);
            FireWeapon(fireRate);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Ignore collision if other is an Enemy
            if (other.transform.parent != null && other.transform.parent.CompareTag("EnemyLaser")) return;

            if (other.CompareTag("Player"))
            {
                other.GetComponent<ITakeDamage>().TakeDamage(collisionDamage); 
                TakeDamage(collisionDamage);
            }
        }
        private void OnDestroy()
        { onEnemyDestroyed?.Invoke(); }
        private void CheckBottomPosition()
        {
            if (transform.position.y <= -6.0f)
                // Reuse enemy in random pos.x
                transform.position = new Vector2(UnityEngine.Random.Range(-8.0f, 8.0f), 5.0f);
        }
        private void ConfigureEnemy(EnemyData _data)
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            collisionDamage = _data.collisionDamage;
            currentHealth = _data.maxHealth;
            fireRate = _data.fireRate;
            isAlive = true;
            model = _data.modelData.model;
            weapon = _data.weaponData.weapon;
            speed = _data.speed;
            explosionPrefab = _data.explosionPrefab;

            SetModel();
        }
        private void EnemyDestroyed()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            speed = 0f;
            animator.SetTrigger("OnEnemyDestroyed");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            gameObject.SetActive(false); // added because enemy was hitting the player twice
            Destroy(gameObject, 2.0f);
        }
        public virtual void FireWeapon(float fireRate)
        {
            if (Time.time > timeToFire && isAlive)
            {
                Instantiate(weapon, firePoint.transform.position, Quaternion.Euler(Vector2.down));
                fireRate = UnityEngine.Random.Range(2f, 5f);
                timeToFire = Time.time + fireRate;
            }
        }
        private void SetModel()
        {
            GameObject modelGo = Instantiate(model);
            modelGo.transform.SetParent(this.transform);
            modelGo.transform.localPosition = Vector3.zero;
            modelGo.transform.rotation = Quaternion.identity;
        }
        public virtual void Move(float speed)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            CheckBottomPosition();
        }
        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            { isAlive = false; EnemyDestroyed(); }
        }
    }
}

/*

TODO

* Implement events, Code to customize enemy in inspector
* create a Laser base class for laser, doubleLaser and Backwards Laser
* raycast for enemy and backward laser chasing player
* set values for each enemytype
* create weapons game objects
* make a weapons dictionary
* create a miscellaneous items dictionary (explosion, shield)
* create a doc in the project to explain how to create a new enemy with hardcode or in editor. if in editor, new empty, attach EnemyDefault script, set values, save as prefab

 Dictionary weapons Laser, DoubleLaser, BackwardLaser, Rocket
 if boss, create a weapon prefabs array
 Require component: rigidbody, 

isBoss
bossPositions

private void MoveEnemy2() --Zigzag
    {
    Vector2 vec = new Vector2(leftRightSpeed * enemyDirection, -1 * speed);
    transform.Translate(vec * Time.deltaTime);

    // set xpos = pos x
    // if posx > xpos + 2
    Vector2 xPos = transform.position;

    if (transform.position.x >= 2f) enemyDirection = -1;
    if (transform.position.x <= -2f) enemyDirection = 1;

    CheckBottomPosition();
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
