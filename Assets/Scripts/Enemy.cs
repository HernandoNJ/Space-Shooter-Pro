using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
   
    void Update()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        // move down 4 units/sec
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // if bottom of screen
        if (transform.position.y <= -5.0f)
        {
            // place enemy at top (reuse the enemy) in random pos.x
            float randomXPos = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomXPos, 5.0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage(1);
                Destroy(gameObject);
            }
            else Debug.Log("There is not Player script in other");
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Laser destroyed");
        }
    }
}


