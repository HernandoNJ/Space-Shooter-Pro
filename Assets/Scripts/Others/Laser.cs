using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damageAmount;
    [SerializeField] private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        speed = 5f;
        damageAmount = 1;
    }
    private void Update()
    {
        MoveLaserUp();
    }
    public void MoveLaserUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= 7.5f)
        {
            Destroy(gameObject);
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            this.player.AddScore(10);
            Destroy(gameObject);
            other.GetComponent<ITakeDamage>().TakeDamage(damageAmount);
        }
    }
}