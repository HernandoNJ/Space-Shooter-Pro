using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool isEnemyLaser;
    
    private void Update()
    {
        if (isEnemyLaser == false) MoveLaserUp();
        else MoveLaserDown();
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

    public void MoveLaserDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -7.5f)
        {
            Destroy(gameObject);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }

    public void SetIsEnemyLaser()
    {
        isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null) player.Damage(0.5f);
        }
    }
}
