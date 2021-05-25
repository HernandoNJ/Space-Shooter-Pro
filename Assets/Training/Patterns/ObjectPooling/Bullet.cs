using UnityEngine;

namespace Training.Patterns.ObjectPooling
{
    public class Bullet : MonoBehaviour
    {
        private void OnEnable()
        {
            Invoke("Hide",1);

        }

        private void Hide()
        {
            // SetActive this bullet to false to be re used
            gameObject.SetActive(false);
        }

    }
}