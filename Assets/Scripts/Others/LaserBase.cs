using UnityEngine;

public class LaserBase : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] private float speed;
    [SerializeField] private int damageAmount;
    [SerializeField] private string parentTag;
    [SerializeField] private Vector3 moveDirection;

    private void Start()
    {
        gameObject.tag = "Laser";
        speed = weaponData.speed;
        damageAmount = weaponData.damageAmount;
    }
    
    private void Update()
    {
        MoveLaser();
    }

    protected virtual void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    protected virtual void SetParentTag(string parentTagString)
    {
        parentTag = parentTagString;
    }
    
    private void MoveLaser()
    {
        transform.Translate(moveDirection * (speed * Time.deltaTime));
        CheckOutboundPosition();
    }
    
    private void CheckOutboundPosition()
    {
        if (!(transform.position.y >= 7.5f)) return;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iDamage = other.GetComponent<ITakeDamage>();

        if (iDamage == null)
        {
            Debug.Log("iDamage is null?: " + iDamage == null);
        }
        else if(other.gameObject.CompareTag(parentTag))
        {
            Debug.Log("other tag: " + other.gameObject.tag);
            Debug.Log("parent tag: " + parentTag);
        }
        else
        {
            iDamage.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
