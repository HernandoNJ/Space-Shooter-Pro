using UnityEngine;

enum EnemyType { BasicEnemy, DoubleShotEnemy, ShieldedEnemy, AggressiveEnemy, ChaserEnemy, BackShootEnemy, AvoidShotEnemy, BossEnemy, Null }
enum EnemyWeapon { Laser, DoubleLaser, Rocket, BackwardLaser, Null }
enum EnemyMove { Normal, ZigZag, ChasePlayer, AvoidShot, Null };

public class Enemy : MonoBehaviour
{

    // Separate 
    // TODO: write logic for enemy shield

    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject doubleLaserPrefab;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject rechargedLaser;
    [SerializeField] private GameObject shield;
    [SerializeField] private Player player;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private bool isEnemyAlive;
    [SerializeField] private bool hasShield;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float timeTime;
    [SerializeField] private int leftRightSpeed;
    [SerializeField] private int enemyDirection = 1;

    [SerializeField] private int[] levelID = { 0, 1, 2, 3, 4, 5, 6, 7 };


    private float fireRate = 3f;
    private float canFire;

    /*

BasicEnemy			move: normal		    Fire: laser
DoubleShotEnemy		move: normal		    Fire: doubleLaser 
ShieldedEnemy		move zig-zag			Fire Laser 			Shield
AggressiveEnemy
ChaserEnemy		    move zz && ChasePlyr	Fire: Laser 
BackShootEnemy	move: normal 		    Fire: laser			Backw laser
AvoidShotEnemy		move: normal		    Fire: laser			AvoidShot
BossEnemy			
 
    Boss: 
    Move up/down, left/right -  Stay in center - Random
    Attack laser - double - circular (1 shot) - rocket - chargedRocket - rocket follows player
    */



    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.LogError("There is not Player script in player");

        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("anim is null in Enemy script");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("audioSource is null in Enemy script");

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) Debug.LogError("spawnmanager is null in Enemy script");

        EnemyWeapon enemyWeapon;
        EnemyMove enemyMove;
        EnemyType enemyType;

        isEnemyAlive = true;
        leftRightSpeed = 1;
        shield.SetActive(false);

    }

    private void Update()
    {
        timeTime = Time.time;
        //MoveEnemy();
        MoveEnemy2();

        // Shot single laser for the weakest enemy
        // double for the stronger one and so on

        ShotDoubleLaser();
    }

    private void MoveEnemy()
    {
        // TODO: change speed depending on the enemy level 
        // FIX configure move code with enemyLevel ID
        // Remove other enemy movements



        transform.Translate(Vector2.down * speed * Time.deltaTime);
        CheckBottomPosition();
    }

    private void MoveEnemy2()
    {
        Vector2 vec = new Vector2(leftRightSpeed * enemyDirection, -1 * speed);
        transform.Translate(vec * Time.deltaTime);

        // set xpos = pos x
        // if posx > xpos + 2
        Vector2 xPos = transform.position;

        if (transform.position.x >= 2f)
        {
            enemyDirection = -1;
        }

        if (transform.position.x <= -2f)
        {
            enemyDirection = 1;
        }

        CheckBottomPosition();
    }

    private void CheckBottomPosition()
    {
        if (transform.position.y <= -6.0f)
        {
            // place enemy at top (reuse the enemy) in random pos.x
            transform.position = new Vector2(Random.Range(-8.0f, 8.0f), 5.0f);
        }
    }

    public void ShotDoubleLaser()
    {
        if (Time.time > canFire && isEnemyAlive)
        {
            Instantiate(doubleLaserPrefab, transform.position, Quaternion.Euler(Vector2.down));

            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
        }
    }

    private void AvoidShot()
    {
        // Move away from the player’s laser
        // Checkout create with code -black ball in moving platform
    }

    private void MoveLaserBackwards()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision if other is an Enemy
        if (other.transform.parent != null && other.transform.parent.CompareTag("EnemyLaser"))
            return;

        if (other.CompareTag("Player"))
        {
            player.Damage(1);
            speed = 0f;
            anim.SetTrigger("OnEnemyDestroyed");
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            gameObject.SetActive(false); // added because enemy was hitting the player twice
            Destroy(gameObject, 2.0f);
        }
    }

    // Called by a laser when hit
    public void OnEnemyDestroyed()
    {
        isEnemyAlive = false;
        anim.SetTrigger("OnEnemyDestroyed");
        speed = 0f;
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, 2.0f);
    }

    private void OnDestroy()
    {
        spawnManager.DecreaseEnemiesAmount();
    }
}