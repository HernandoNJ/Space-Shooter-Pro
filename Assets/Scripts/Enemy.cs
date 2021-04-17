using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject doubleLaserPrefab;
    [SerializeField] private Player player;
    [SerializeField] private SpawnManager spawnManager;

    [SerializeField] private bool isEnemyAlive;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int leftRightSpeed;
    [SerializeField] private int enemyDirection = 1;

    private float fireRate = 3f;
    private float canFire = -1;

    public int rotateCounter;
    public float canMove;
    public bool isRotating;
    public float timeTime;

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

        isEnemyAlive = true;
        leftRightSpeed = 1;
        canMove = 3;

    }

    private void Update()
    {
        timeTime = Time.time;

        if (!isRotating) { MoveEnemy3(); }
        else RotateEnemy();

        ShotDoubleLaser();
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        CheckBottomPosition();
    }

    private void MoveEnemy2()
    {
        Vector2 vec = new Vector2(leftRightSpeed * enemyDirection, -1 * speed);
        transform.Translate(vec * Time.deltaTime);

        if (transform.position.x >= 4f)
        {
            enemyDirection = -1;
        }

        if (transform.position.x <= -4f)
        {
            enemyDirection = 1;
        }

        CheckBottomPosition();
    }

    /* f (tt > timeToMove * moveAgain)
          tt    tm    mov   !mov      m+r +
           1    3 * 1 true   1          1
           2    3     true   1          1
           3.1  3     false  true            1
           4.1
       * 
       * 
       */

    private void MoveEnemy3()
    {
        if (Time.time < canMove)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else isRotating = true;

        CheckBottomPosition();
    }

    private void RotateEnemy()
    {
        rotateCounter += 5;
        transform.Rotate(Vector3.forward * 5f);
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (rotateCounter == 360)
        {
            rotateCounter = 0; // without this line, next time it will be 365
            isRotating = false;
            canMove = Time.time + 2;
        }
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
        // cf = 0
        // tt = 2
        // if tt>cf0 --> true
        // shoot
        // cf = tt +2 = 4
        // ***
        // cf = 4
        // tt = 2.1
        // if tt2.1 > cf4 --> false
        // if tt3.5 > cf4 --> false

        if (Time.time > canFire && isEnemyAlive)
        {
            Instantiate(doubleLaserPrefab, transform.position, Quaternion.Euler(Vector2.down));

            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;
        }
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

/* FIXED: Enemies are destroying themselves with double laser - changing tag not working
 * FIXED: Enemy shooting after destroyed
 * 
 */

