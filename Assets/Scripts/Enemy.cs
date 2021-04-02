using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
            Debug.LogError("There is not Player script in player");
    }

    private void Update()
    {
        MoveEnemy();
    }
    
    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y <= -5.0f)
        {
            // place enemy at top (reuse the enemy) in random pos.x
            float randomXPos = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector2(randomXPos, 5.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.Damage(1);
            Destroy(gameObject);
        }

        if (other.CompareTag("Laser"))
        {
            player.AddScore(10);  // add 10 to score
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}


