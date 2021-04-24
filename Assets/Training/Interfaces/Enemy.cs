using UnityEngine;

namespace Interfaces
{
    public class Enemy : MonoBehaviour, IDamage, IShoot
    {

        public int Health { get; set; }

        public void Damage(int damageAmount)
        {
            Health -= damageAmount;
            GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public void Shoot()
        {

        }


    }
}
