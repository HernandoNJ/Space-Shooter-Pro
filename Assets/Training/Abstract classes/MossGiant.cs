using UnityEngine;

namespace Abstracts
{
    public class MossGiant : Enemy
    {
        private void Start()
        {
            Die();
        }

        override public void Attack()
        {

        }

        public override void Die()
        {
            // Add custom particle system
            base.Die();
            Debug.Log("Gameobject is destroyed");
            Debug.Log("GameObject name: " + gameObject.name);
        }
    }
}