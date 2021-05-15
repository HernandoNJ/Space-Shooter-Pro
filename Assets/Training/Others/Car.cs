﻿using UnityEngine;

namespace Training.Others
{
    public class Car : MonoBehaviour, ITakeDamage
    {
        private int health = 5;
        
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0 ) Destroy(gameObject);
        }
    }
}