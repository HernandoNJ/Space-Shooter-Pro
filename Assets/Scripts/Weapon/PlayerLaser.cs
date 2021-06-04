//Size increased because parent's transform is reduced

using System;
using UnityEngine;

namespace Weapon
{
public class PlayerLaser : LaserBase
{
    private void Awake()
    {
        //parentTag = "Player";
    }

    private void Start()
    {
        Debug.LogWarning("Tag:" + parentTag);
        Debug.Log("Player tag to show: " + parentTag);
        Debug.Log("Damage amount in player: " + damageAmount);
    }
}
}
