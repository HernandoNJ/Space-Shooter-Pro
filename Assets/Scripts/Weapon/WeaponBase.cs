using Interfaces;
using UnityEngine;

namespace Weapon
{
#region Required components

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

#endregion
public class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] protected string parentName;
    [SerializeField] protected Vector3 directionToMove;

    protected virtual void OnEnable()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        parentName = weaponData.parentName;
    }

    private void Start()
    {
        SetAdditionalValues();
        Destroy(gameObject, weaponData.timeAlive);
    }

    protected virtual void SetAdditionalValues()
    {
        // Set direction to move
    }

    protected virtual void Update()
    {
        MoveWeapon();
    }

    protected virtual void MoveWeapon()
    {
        transform.Translate(directionToMove * (weaponData.fireForce * Time.deltaTime));
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<IDamageable>();
        if (iDamage == null || other.CompareTag(parentName))
        {
            Debug.LogWarning($"IDamageable in ... {other.name} is null or other tag is... {other.tag}");
            return;
        }

        iDamage.TakeDamage(weaponData.damage);
        Destroy(gameObject);
    }
}
}
