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
    private void MoveLaserUp()
    {
        transform.Translate(Vector2.up * (speed * Time.deltaTime));

        if (transform.position.y >= 7.5f)
        {
            Destroy(gameObject);
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<ITakeDamage>();
        if (iDamage == null)
        { 
            Debug.Log("ITakeDamage not found in other: " + other.gameObject.name); 
            return;
        }
        iDamage.TakeDamage(damageAmount);
        Destroy(gameObject);
    }
}