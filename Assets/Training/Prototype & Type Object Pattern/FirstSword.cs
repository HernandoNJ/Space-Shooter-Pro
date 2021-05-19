using UnityEngine;

public class FirstSword : MonoBehaviour
{
    public int damage = 1;
    public void Attack(Target target)
    {
        target.TakeDamage(damage);
        Debug.Log("Damage applied to : -- " + target.name);
    }

}
