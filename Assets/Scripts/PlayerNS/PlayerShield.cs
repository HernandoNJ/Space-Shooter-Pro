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
    [SerializeField] private bool isShieldEnabled;

    public static event Action<bool> OnShieldActive;

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
        DisableShield();
    }

    public void ShieldObject()
    {
        shieldStrength = 3;
        EnableShield();
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
                    DisableShield();
                    Debug.Log("default value in ShieldColor");
                }

                break;
        }
    }

    private void EnableShield()
    {
        isShieldEnabled = true;
        OnShieldActive?.Invoke(isShieldEnabled);
        shield.SetActive(true);
        shield.GetComponent<SpriteRenderer>().enabled = true;
        SetShieldColor(shieldStrength);
    }

    private void DisableShield()
    {
        isShieldEnabled = false;
        OnShieldActive?.Invoke(isShieldEnabled);
        shield.SetActive(false);
        shield.GetComponent<SpriteRenderer>().enabled = false;
    }

    public int CheckShieldStrength()
    {
        return shieldStrength;
    }
}
}
