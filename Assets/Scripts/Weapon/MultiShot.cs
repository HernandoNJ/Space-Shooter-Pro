using UnityEngine;

namespace Weapon
{
public class MultiShot : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;

    private void Start()
    {
        for (int shotPosition = 0; shotPosition < 360; shotPosition+=12)
        {
            // laserPrefab.GetComponent<AudioSource>().enabled = false; // todo ask Austin
            // Instantiate(laserPrefab, transform.position, Quaternion.identity);
            // laserPrefab.transform.eulerAngles += Vector3.forward * shotPosition; // rotate in z axis

            var newShot = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            newShot.GetComponent<AudioSource>().enabled = false;
            newShot.transform.eulerAngles += Vector3.forward * shotPosition; // rotate in z axis
        }
        Destroy(gameObject,2.5f);
    }
}
}
