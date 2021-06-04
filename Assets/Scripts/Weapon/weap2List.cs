using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class weap2List : MonoBehaviour
{
    public List<weap2> weap = new List<weap2>();

    private void OnEnable()
    {
        var w = new weap2("weapon1", gameObject.AddComponent<WeaponLauncher>());
        var w1 = new weap2("weapon2", gameObject.AddComponent<WeaponLauncher>());

        Debug.Log("weapon 1 name: " + w.weaponName);
    }
}
