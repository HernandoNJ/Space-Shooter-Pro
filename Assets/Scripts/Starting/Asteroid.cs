using Managers;
using UnityEngine;
using static UnityEngine.Debug;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotateSpeeed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null) LogError("SpawnManager is null in Asteroid");

        if (explosionPrefab == null) LogError("Explosion prefab is null in Asteroid");

        transform.position = Vector3.up * 3f;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //GetComponent<SpriteRenderer>().enabled = false;
            spawnManager.StartSpawnning();
            Destroy(gameObject, 0.1f);
        }
    }
}
