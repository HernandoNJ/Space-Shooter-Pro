using System;
using UnityEngine;

namespace Weapon
{
public class EnemyLaser : LaserBase
{
    private void Start()
    {
        Debug.Log("Enemy tag to show: " + parentTag);
        Debug.Log("Damage amount in enemy: " + damageAmount);
        
    }
}
}
