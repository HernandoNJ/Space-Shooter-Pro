using ScriptableObjects.Inventory.Weapon;
using UnityEngine;

namespace Lasers{
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class LaserBase : MonoBehaviour{

    [SerializeField] public WeaponData weaponData;
    [SerializeField] private float speed;
    [SerializeField] private int damageAmount;
    [SerializeField] protected string parentTag;
    [SerializeField] protected Vector3 laserMoveDirection;
    [SerializeField] private AudioSource audioSource;

    protected virtual void OnEnable()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        GetComponent<SpriteRenderer>().sprite = weaponData.model;
        audioSource = GetComponent<AudioSource>();

        gameObject.tag = "Laser";
        speed = weaponData.speed;
        damageAmount = weaponData.damageAmount;

        SetAdditionalValues();
        ShowAdditionalInfo();
    }

    protected virtual void SetAdditionalValues() { }

    private void Update()
    {
        MoveLaser();
    }

    private void MoveLaser()
    {
        transform.Translate(laserMoveDirection * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<ITakeDamage>();

        if (iDamage == null) Debug.LogWarning("iDamage is null");
        else
        {
            iDamage.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    private void ShowAdditionalInfo()
    {
        Debug.LogWarning("Trigger, SortingLayerName and audioSource set in OnEnable");
        Debug.LogWarning("Set other values with <b>SetAdditionalValues()</b> overriding onEnable");
    }
}
}