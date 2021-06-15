using UnityEngine;

namespace PlayerNS
{
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
   public string playerName;
   public string description;
   public int collisionDamage;
   public int maxHealth;
   public float speed;
   
}
}
