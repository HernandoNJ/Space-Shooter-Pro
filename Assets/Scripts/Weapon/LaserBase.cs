using System;
using UnityEngine;

namespace Weapon
{
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class LaserBase : MonoBehaviour
{
    /* Notes
     * Laser moved by WeaponLauncher
     * gameObject.SetActive(false); --> pooling system
     */
    
    [SerializeField] protected int damageAmount;
    [SerializeField] protected string parentTag;

    protected virtual void OnEnable()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        gameObject.tag = "Laser";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<ITakeDamage>();

        if (iDamage == null || other.CompareTag(parentTag))
        {
            Debug.LogWarning("iDamage is null or other.tag == parent tag");

            return;
        }

        iDamage.TakeDamage(damageAmount);

        Destroy(gameObject);
    }
}
}
