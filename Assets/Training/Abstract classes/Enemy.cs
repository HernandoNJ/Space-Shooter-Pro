using UnityEngine;

namespace Abstracts
{
    public abstract class Enemy : MonoBehaviour
    {
        public string speed;
        public int health;
        public int gems;

        public abstract void Attack();

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
