using Interfaces;
using UnityEngine;

namespace Weapon.Lasers
{
#region Required components
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
#endregion 
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
        var iShoot = other.GetComponent<IShootable>();
        if (iShoot == null || other.CompareTag(parentName))
        {
            Debug.LogWarning("iShootable is null or other.tag == parent tag");  
            return;
        }
        
        iShoot.TakeDamage(damage);
        Destroy(gameObject);
    }
}
}
