using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private void Update()
    {
        MoveLaserUp();

        if (transform.position.y >= 7.5f)
        {
            Destroy(gameObject);

            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }

    public void MoveLaserUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

}
