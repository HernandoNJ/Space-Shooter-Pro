using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    void Update()
    {
        MoveLaserUp();

        if (transform.position.y >= 7.5f) 
            Destroy(gameObject);
    }

    public void MoveLaserUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

}
