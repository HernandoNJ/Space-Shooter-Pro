using System;
using UnityEngine;

namespace Events
{
    public class Player : MonoBehaviour
    {
        public delegate void Death();
        public static event Death OnDeath;

        public static Action<int> OnDamageReceived;

        public int Health { get; set; }

        private void Start()
        {
            Health = 10;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnDeath();
            
            if(Input.GetKeyDown(KeyCode.D))
                Damage();
        }

        private void Damage()
        {
            Health--;

            if(OnDamageReceived != null) OnDamageReceived(Health);
            else Debug.Log("OnDamageReceived Event is null");
        }

    }
}
