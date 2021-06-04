using System;
using Player;
using UnityEngine;

namespace Others
{
public class ParticlesSetup : MonoBehaviour
{
    [SerializeField] private GameObject thrusterParticles;
    [SerializeField] private GameObject deathParticles;

    private void Awake()
    {
        GetComponent<SpeedBooster>().OnThrustChanged += ThrustChangedHandler;
        
        // If player has not PlayerHealth, it will be invulnerable
        if(GetComponent<PlayerHealth>()!= null)
            GetComponent<Player.PlayerHealth>().OnDie += PlayerDeathHandler;
    }

    private void ThrustChangedHandler(float thrustValue)
    {
        thrusterParticles.SetActive(thrustValue >0f);
        // scale the particle based on thrust here
    }
    
    private void PlayerDeathHandler()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
}
