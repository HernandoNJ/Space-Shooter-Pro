using UnityEngine;

namespace Training.Others
{
    public class Barrel : MonoBehaviour, ITakeDamage
    {
    
        public void TakeDamage(int damage)
        {
            Destroy(gameObject);
        }
    }
}