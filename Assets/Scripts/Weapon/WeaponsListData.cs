using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
[CreateAssetMenu(fileName = "WeaponsListData", menuName = "ScriptableObject/Weapon/WeaponsListData")]
public class WeaponsListData : ScriptableObject
{
    public List<GameObject> weapons;
}
}
