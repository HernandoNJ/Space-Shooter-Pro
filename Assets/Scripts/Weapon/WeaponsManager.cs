using System;
using UnityEngine;

namespace Weapon
{
public class WeaponsManager : MonoBehaviour
{
    public WeaponLauncher[] weapons = new WeaponLauncher[5];

    private void Awake()
    {
        weapons = GetComponents<WeaponLauncher>();

    }
}
}
