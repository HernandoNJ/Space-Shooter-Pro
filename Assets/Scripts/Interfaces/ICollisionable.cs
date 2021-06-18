using UnityEngine;

namespace Interfaces
{
public interface ICollisionable  
{
    int ColDamage{ get; set; }
    void CollisionDamage(int colDamage);
}
}
