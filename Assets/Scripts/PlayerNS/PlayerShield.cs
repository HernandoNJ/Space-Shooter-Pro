using Interfaces;
using Powerups;
using UnityEngine;

namespace PlayerNS
{
public class PlayerShield : MonoBehaviour, IShieldable
{
    [SerializeField] private GameObject shield;
    [SerializeField] private int shieldStrength;
    [SerializeField] private bool shieldActive;
    [SerializeField] private Color shieldColor;

    private void OnEnable()
    {
        Powerup.OnShieldPowerupCollected += ShieldObject;
        shield.SetActive(false);
        shieldColor = GetComponent<Renderer>().material.color;
    }

    private void OnDisable()
    {
        Powerup.OnShieldPowerupCollected -= ShieldObject;
    }

    public void ShieldObject()
    {
        shieldStrength = 3;
        shield.SetActive(true);
        shieldActive = true;
        shieldColor = Color.green;
    }

    public void DamageShield(int damageValue)
    {
        shieldStrength -= damageValue;
        if (shieldStrength == 2) shieldColor = Color.blue;
        if (shieldStrength == 1) shieldColor = Color.red;
        if (shieldStrength > 0) return;
        shield.SetActive(false);
        shieldActive = false;
    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }
}
}
