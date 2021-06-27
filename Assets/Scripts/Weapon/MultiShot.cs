using UnityEngine;

namespace Weapon
{
public class MultiShot : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;

    private void Start()
    {
        for (int shotPosition = 0; shotPosition < 360; shotPosition += 12)
        {
            var newShot = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            newShot.GetComponent<AudioSource>().enabled = false;
            newShot.transform.eulerAngles += Vector3.forward * shotPosition; // rotate in z axis
            //newShot.transform.position += Vector3.up * 5f;  // todo ask Austin how to move lasers a bit up when  instantiating
        }
        Destroy(gameObject, 2.5f);
    }
}
}
