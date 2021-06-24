using System;
using Interfaces;
using Powerups;
using UnityEngine;

namespace PlayerNS
{
public class PlayerShield : MonoBehaviour, IShieldable
{
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private Transform shieldPoint;
    [SerializeField] private int shieldStrength;
    // public int ShieldStrength{get;private set;} to access the value from outside

    public static event Action<bool> OnShieldActiveChanged;

    private void OnEnable()
    {
        Powerup.OnShieldPowerupCollected += ShieldObject;
    }

    private void OnDisable()
    {
        Powerup.OnShieldPowerupCollected -= ShieldObject;
    }

    private void Start()
    {
        shield = Instantiate(shieldPrefab, shieldPoint.transform);
        EnableShield(false);
    }

    public void ShieldObject()
    {
        shieldStrength = 3;
        EnableShield(true);
    }

    public void DamageShield(int damageValue)
    {
        shieldStrength -= damageValue;
        SetShieldColor(shieldStrength);
    }

    private void SetShieldColor(int strength)
    {
        switch (strength)
        {
            case 3:
                shield.GetComponent<SpriteRenderer>().material.color = Color.green;
                break;
            case 2:
                shield.GetComponent<SpriteRenderer>().material.color = Color.blue;
                break;
            case 1:
                shield.GetComponent<SpriteRenderer>().material.color = Color.red;
                break;
            default:
                if (strength <= 0)
                {
                    EnableShield(false);
                    Debug.Log("default value in ShieldColor");
                }

                break;
        }
    }

    private void EnableShield(bool isShieldEnabled)
    {
        OnShieldActiveChanged?.Invoke(isShieldEnabled);
        shield.SetActive(isShieldEnabled);
        shield.GetComponent<SpriteRenderer>().enabled = isShieldEnabled;
        if (isShieldEnabled) SetShieldColor(shieldStrength);
    }

    public int CheckShieldStrength()
    {
        return shieldStrength;
    }
}
}
