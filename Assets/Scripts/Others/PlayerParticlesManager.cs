using PlayerNS;
using UnityEngine;

namespace Others
{
public class PlayerParticlesManager : MonoBehaviour
{
    [SerializeField] private GameObject thrusterParticles;
    [SerializeField] private GameObject deathParticles;

    private void OnEnable()
    {
        GetComponent<SpeedBooster>().OnThrustChanged += ThrustChangedHandler;
        
        // If player has not PlayerHealth, it will be invulnerable
        if (GetComponent<PlayerHealth>() != null)
            GetComponent<PlayerNS.PlayerHealth>().OnPlayerDie += PlayerDeathHandler;
    }

    private void OnDisable()
    {
        GetComponent<SpeedBooster>().OnThrustChanged -= ThrustChangedHandler;
    }

    private void ThrustChangedHandler(float thrustValue)
    {
        thrusterParticles.SetActive(thrustValue > 0f);
        // scale the particle based on thrust here
    }

    private void PlayerDeathHandler()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}
}
