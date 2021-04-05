using UnityEngine;

// Bug: Enemies are destroying themselves with double laser - changing tag not working

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject doubleLaserPrefab;

    [SerializeField] private float speed = 4f;

    private float fireRate = 3f;
    private float canFire = -1;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.LogError("There is not Player script in player");

        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("anim is null in Enemy script");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) Debug.LogError("audioSource is null in Enemy script");
    }

    private void Update()
    {
        MoveEnemy();
        ShotDoubleLaser();
    }
    
    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= -6.0f)
        {
            // place enemy at top (reuse the enemy) in random pos.x
            float randomXPos = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector2(randomXPos, 5.0f);
        }
    }

    public void ShotDoubleLaser()
    {
        if (Time.time > canFire)
        {
            fireRate = Random.Range(3f, 7f);
            canFire = Time.time + fireRate;

            GameObject enemyDoubleLaser =
                Instantiate(doubleLaserPrefab, transform.position, Quaternion.identity);

            Laser[] laserScripts = enemyDoubleLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < laserScripts.Length; i++)
            {
                laserScripts[i].SetIsEnemyLaser();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent != null && other.transform.parent.tag == "EnemyLaser") 
            return;

        if (other.CompareTag("Player"))
        {
            player.Damage(1);
            speed = 0f;
            anim.SetTrigger("OnEnemyDestroyed");
            audioSource.Play();
            Destroy(gameObject, 2.0f);
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            player.AddScore(10);  
            anim.SetTrigger("OnEnemyDestroyed");
            speed = 0f;
            audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2.0f);
        }
    }
}

