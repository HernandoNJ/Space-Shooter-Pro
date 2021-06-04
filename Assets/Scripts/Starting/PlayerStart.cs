using System;
using System.Collections;
using Weapon;
using Managers;
using UnityEngine;
using static UnityEngine.Debug;
using GameObject = UnityEngine.GameObject;

namespace Starting
{
public class PlayerStart : MonoBehaviour, ITakeDamage
{
    #region Variables

    [SerializeField] private GameObject multipleShotPrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject leftEngine;
    [SerializeField] private GameObject rightEngine;
    [SerializeField] private Transform firePoint0;
    [SerializeField] private Transform firePoint1;
    [SerializeField] private Transform firePoint2;


    [SerializeField] private bool hasAmmo;
    [SerializeField] private bool isTripleLaserActive;
    [SerializeField] private bool isSpeedBoostActive;
    [SerializeField] private bool isShieldActive;
    [SerializeField] private float canFire = 0f;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float speedUpSpeed = 2f;
    [SerializeField] private float leftShiftSpeedMult = 1.5f;
    [SerializeField] private float totalSpeed;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private int health = 3;
    [SerializeField] private int score;
    [SerializeField] private int ammoAvailable;
    [SerializeField] private int ammoMax;

    private UIManager uiManager;
    private SpawnManager spawnManager;
    private LasersManager lasersManager;

    public static Action<int> onAddScore;
    public static Action<int> onScoreUpdated;
    public static Action<int, int> onAmmoUpdated;
    public static Action onPlayerDestroyed;

    #endregion

    private void OnEnable() => onAddScore += AddScore;
    private void OnDisable() => onAddScore -= AddScore;
    private void Start() => SetInitialValues();

    private void Update()
    {
        MovePlayer();
        if (FiringActive()) FireLaser();
    }

    #region Functions

    private void SetInitialValues()
    {
        spawnManager = SpawnManager.Instance;
        uiManager = UIManager.Instance;
        lasersManager = LasersManager.Instance;

        transform.position = new Vector2(0f, -3.0f);

        ammoMax = 15;
        ammoAvailable = 15;
        UpdateAmmo();
        shield.SetActive(false);
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Increase speed with powerup
        if (isSpeedBoostActive)
        {
            totalSpeed = playerSpeed * speedUpSpeed;
            transform.Translate(moveDirection * (totalSpeed * Time.deltaTime));
            UIManager.Instance.IncreaseThrusterBar(0.004f);
        }
        // Increase speed with Left shift key
        else if (SpeedIncreased())
        {
            totalSpeed = playerSpeed * leftShiftSpeedMult;
            transform.Translate(moveDirection * (totalSpeed * Time.deltaTime));

            uiManager.IncreaseThrusterBar(0.004f);
        }
        // Normal PlayerStart movement
        else
        {
            totalSpeed = playerSpeed; // Just for testing
            transform.Translate(moveDirection * (playerSpeed * Time.deltaTime));
            uiManager.DecreaseThrusterBar(0.004f);
        }

        // Set up player movement constraints
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (yPos >= 0f)
            transform.position = new Vector2(xPos, 0f);
        else if (yPos <= -3.5f)
            transform.position = new Vector2(xPos, -3.5f);

        if (xPos >= 10.4f)
            transform.position = new Vector2(-10.4f, yPos);
        else if (xPos <= -10.4f)
            transform.position = new Vector2(10.4f, yPos);
    }

    private static bool SpeedIncreased() => Input.GetKey(KeyCode.LeftShift);

    private bool FiringActive() => Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && hasAmmo;

    private void FireLaser()
    {
        // Shot triple laser
        if (isTripleLaserActive)
        {
            canFire = Time.time + fireRate;

            /* TODO fix triple laser in player*/
            //Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        // Shot one laser
        else
        {
            canFire = Time.time + fireRate;
            lasersManager.GetOneBullet();
            Debug.Break();
        }

        ammoAvailable--;


        UpdateAmmo();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdatePlayerState(health);

        if (health <= 0) Destroy(gameObject);
    }

    private void UpdatePlayerState(int playerLives)
    {
        uiManager.UpdateLives(health);

        if (isShieldActive) ChangeShieldColor();

        if (health == 3)
        {
            leftEngine.SetActive(false);
            rightEngine.SetActive(false);
        }

        if (health == 2)
        {
            leftEngine.SetActive(true);
            rightEngine.SetActive(false);
        }
        else if (health == 1)
        {
            leftEngine.SetActive(true);
            rightEngine.SetActive(true);
        }
    }

    private void AddScore(int points)
    {
        score += points;
        UpdateScore(() => { onScoreUpdated?.Invoke(score); });
    }

    private void UpdateScore(Action newAction = null)
    {
        Log("newAction event with anonymous method Lambda implementation will be called");
        newAction?.Invoke();
    }

    private void OnDestroy()
    {
        onPlayerDestroyed?.Invoke();
    }

    #endregion

    #region Activate powerups mechanics

    public void ActivateTripleLaser()
    {
        isTripleLaserActive = true;
        StartCoroutine(TripleLaserRoutine());
    }

    IEnumerator TripleLaserRoutine()
    {
        yield return new WaitForSeconds(5f);
        isTripleLaserActive = false;
    }

    public void ActivateMultipleShot()
    {
        Transform objParent = (lasersManager.firePoints[0]);
        objParent.transform.position = Vector3.up * 2;
        Instantiate(multipleShotPrefab, objParent);
        Destroy(multipleShotPrefab, 3);
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5f);
        isSpeedBoostActive = false;
    }

    public void ActivateShield()
    {
        isShieldActive = true;
        shield.SetActive(true);
        health = 3;
        UpdatePlayerState(health);
    }

    private void ChangeShieldColor()
    {
        switch (health)
        {
            case 3:
                shield.GetComponent<SpriteRenderer>().color = new Color(0.337f, 0.906f, 0.374f, 1f);
                break;
            case 2:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f, 0.7f);
                break;
            case 1:
                shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0.2f, 0f, 0.6f);
                break;
            case 0:
                spawnManager.PlayerIsDeath();
                Destroy(gameObject);
                break;
        }
    }

    private void UpdateAmmo()
    {
        if (ammoAvailable > ammoMax) ammoAvailable = ammoMax;
        onAmmoUpdated?.Invoke(ammoAvailable, ammoMax);
        hasAmmo = (ammoAvailable >= 1) ? true : false;
        if (hasAmmo) return;
        onAmmoUpdated?.Invoke(ammoAvailable, ammoMax);
    }

    public void RefillAmmo()
    {
        ammoAvailable = 15;
        UpdateAmmo();
        uiManager.OnFullAmmo();
    }

    public void RecoverHealth()
    {
        if (health >= 3) return;
        health++;
        UpdatePlayerState(health);
    }

    #endregion
}
}