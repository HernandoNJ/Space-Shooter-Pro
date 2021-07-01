using System;
using System.Collections;
using UnityEngine;
using static PowerupType;

public enum PowerupType
{
    Default,
    TripleLaser,
    SpeedBoost,
    Shield,
    RefillAmmo,
    RecoverHealth,
    MultipleShot,
    DamagePickup,
    LaserMultiSeekPickup
}

namespace Powerups
{
public class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip powerupSound;
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private float speed;
    [SerializeField] private bool isMagnetActive;
    [SerializeField] private GameObject player;

    public static event Action<PowerupType> OnWeaponPowerupCollected;
    public static event Action<PowerupType> OnHealthPowerupCollected;
    public static event Action<PowerupType> OnLaserMultiSeekPowerupCollected;
    public static event Action<float> OnMovementPowerupCollected;
    public static event Action OnShieldPowerupCollected;

    private void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log("Set powerup speed in inspector");
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.C)) { StartCoroutine(BringPowerupCloserRoutine()); }

        CheckBottom();
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }

    private void CheckBottom()
    {
        if (transform.position.y < -5f)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private IEnumerator BringPowerupCloserRoutine()
    {
        var waitTime = 0f;
        isMagnetActive = true;

        while (isMagnetActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3f* Time.deltaTime);
            yield return new WaitForEndOfFrame();

            waitTime += Time.deltaTime;

            if (waitTime> 1f)
            {
                isMagnetActive = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        AudioSource.PlayClipAtPoint(powerupSound, transform.position, 0.3f);
        SetPowerupInfo(powerupType);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }

    private void SetPowerupInfo(PowerupType powerupInfo)
    {
        switch (powerupInfo)
        {
            case TripleLaser:
            case MultipleShot:
            case RefillAmmo:
                OnWeaponPowerupCollected?.Invoke(powerupInfo);
                break;
            case RecoverHealth:
            case DamagePickup:
                OnHealthPowerupCollected?.Invoke(powerupInfo);
                break;
            case Shield:
                OnShieldPowerupCollected?.Invoke();
                break;
            case SpeedBoost:
                OnMovementPowerupCollected?.Invoke(1.5f);
                break;
            case LaserMultiSeekPickup:
                OnLaserMultiSeekPowerupCollected?.Invoke(powerupInfo);
                break;
            default:
                Debug.Log("Configure powerup");
                throw new ArgumentOutOfRangeException(nameof(powerupInfo), powerupInfo, null);
        }
    }
}
}
