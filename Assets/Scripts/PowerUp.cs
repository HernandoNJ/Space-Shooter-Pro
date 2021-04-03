using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    // 0: tripleshot 1: speed 2: shield
    [SerializeField] private int powerupID; 

    void Update()
    {
        MoveTripleShotPowerup();

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    private void MoveTripleShotPowerup()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0: player.ActivateTripleLaser(); break;
                    case 1: player.ActivateSpeedBoost(); break;
                    case 2: player.ActivateShield(); break;
                    default: Debug.Log("Default message in switch"); break;
                }
            }
            else
                Debug.LogError("There is no Player script in player (other)");

            Destroy(gameObject);
        }
    }
}
