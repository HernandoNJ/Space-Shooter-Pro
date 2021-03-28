using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float fireRate = 2.0f;
    private float canFire = 0f;

    private void Update()
    {
        // if Time.time = 5, Time.time > 0f => true
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;

        transform.Translate(moveDirection * speed * Time.deltaTime);

        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (yPos >= 0f) transform.position = new Vector3(xPos, 0f, 0f);
        else if (yPos <= -3.5f) transform.position = new Vector3(xPos, -3.5f, 0f);

        if (xPos >= 10.4f) transform.position = new Vector3(-10.4f, yPos, 0f);
        else if (xPos <= -10.4f) transform.position = new Vector3(10.4f, yPos, 0f);
    }

    private void FireLaser()
    {
        // canFire = 5 + 0.7f = 5.7f
        canFire = Time.time + fireRate; // player must wait until Time.time = 5.8f to shoot again

        Vector3 laserPos = transform.position + new Vector3(0, 1.4f, 0);
        Instantiate(laserPrefab, laserPos, Quaternion.identity);
        Debug.Log(Time.time);
    }
}
