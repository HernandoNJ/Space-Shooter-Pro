using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Inventory.Weapon
{
[CreateAssetMenu(fileName = "WeaponsListData", menuName = "ScriptableObject/Inventory/WeaponsListData")]
public class WeaponsListData : ScriptableObject
{
    public List<GameObject> weapons;
}
}
