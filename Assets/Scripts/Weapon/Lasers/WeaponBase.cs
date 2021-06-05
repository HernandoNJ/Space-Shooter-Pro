using Interfaces;
using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

namespace Weapon.Lasers
{
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class WeaponBase : MonoBehaviour
{
    /* Notes:
     * Size increased because parent's transform is reduced
     * Laser moved by WeaponLauncher
     * gameObject.SetActive(false); --> pooling system
     * damage and parent name just for being seen in inspector
     */
     
    public WeaponData weaponData;
    [SerializeField] protected float damage;
    [SerializeField] protected string parentName;

    protected virtual void OnEnable()
    {
        // values are set here just in case
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        damage = weaponData.damage;
        parentName = weaponData.parentName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<ITakeDamage>();
        if (iDamage == null || other.CompareTag(parentName))
        {
            Debug.LogWarning("iDamage is null or other.tag == parent tag");  return;
        }
        
        iDamage.TakeDamage(damage);
        Destroy(gameObject);
    }
}
}
