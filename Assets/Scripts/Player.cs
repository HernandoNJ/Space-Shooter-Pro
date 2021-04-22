using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] private AudioClip laserSound;
    [SerializeField] private GameObject multipleShotPrefab;
    [SerializeField] private GameObject firepoint;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject leftEngine;
    [SerializeField] private GameObject rightEngine;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private bool hasAmmo;
    [SerializeField] private bool isMultipleShotActive;
    [SerializeField] private bool isTripleLaserActive;
    [SerializeField] private bool isSpeedBoostActive;
    [SerializeField] private bool isShieldActive;
    [SerializeField] private float canFire = 0f;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float speedUpSpeed = 2f;
    [SerializeField] private float leftShiftSpeedMult = 1.5f;
    [SerializeField] private float totalSpeed;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private float MultipleShotFireRate = 5.0f;
    [SerializeField] private int lives = 3;
    [SerializeField] private int score;
    [SerializeField] private int ammoAvailable;
    [SerializeField] private int ammoMax;

    private SpawnManager spawnManager;
    private AudioSource audioSource;

    private bool isLeftShiftKeyPressed;

    #endregion


    // TODO: Fix laser clone in multShot destroy ***

    // TODO create negative pickup
    // Create powerup prefab
    // Set values
    // Define a method in player
    // Reduce 2 lives
    // DONE negative powerup mechanic 

    // FIXED multishot mechanic - code and time delay

    // TODO fix engines disable after health powerup, with or without shield
    // INFO: the updatedamage method had only left engine to false in some if conditions. Added right engine
    // DONE

    // TODO add behaviors to activeMultiShot mechanic
    // if activeMultShot, ammo ++ 
    // ammo = 1 .. turn off onemptyammo .. 
    // fire multiple shot .. 
    // DONE

    // DONE: created a new UpdateAmmo function to check ammo amount and if hasAmmo

    // INFO
    // when to update ammo ...
    //  FireLaser() refillAmmo () ActMultShot()

    // When to increase ammo ...
    //  refill ammo powerup
    //  multipleShot powerup

    // When to reduce ammo ...
    //  FireLaser()

    private void Start()

    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        { Debug.LogError("There is no AudioSource component in Player script"); }

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        { Debug.LogError("There is no SpawnManager script in spawnManager"); }

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uiManager == null)
        { Debug.LogError("No UI Manager found. Null"); }

        transform.position = new Vector2(0f, -3.0f);
        audioSource.clip = laserSound;
        ammoMax = 15;
        ammoAvailable = 15;
        UpdateAmmo();

        shield.SetActive(false);
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);
    }

    private void Update()
    {
        MovePlayer();

        if (FiringActive())
        { FireLaser(); }
    }

    #region Functions
    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Increase speed with powerup
        if (isSpeedBoostActive)
        {
            totalSpeed = playerSpeed * speedUpSpeed;
            transform.Translate(moveDirection * totalSpeed * Time.deltaTime);
            uiManager.IncreaseThrusterBar(0.004f);
        }
        // Increase speed with Left shift key
        else if (SpeedIncreased())
        {
            totalSpeed = playerSpeed * leftShiftSpeedMult;
            transform.Translate(moveDirection * totalSpeed * Time.deltaTime);

            uiManager.IncreaseThrusterBar(0.004f);
        }
        // Normal Player movement
        else
        {
            totalSpeed = playerSpeed; // Just for testing
            transform.Translate(moveDirection * playerSpeed * Time.deltaTime);
            uiManager.DecreaseThrusterBar(0.004f);
        }

        // Set up player movement constraints 
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (yPos >= 0f)
        { transform.position = new Vector2(xPos, 0f); }
        else if (yPos <= -3.5f)
        { transform.position = new Vector2(xPos, -3.5f); }

        if (xPos >= 10.4f)
        { transform.position = new Vector2(-10.4f, yPos); }
        else if (xPos <= -10.4f)
        { transform.position = new Vector2(10.4f, yPos); }
    }

    private bool FiringActive()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time > canFire && hasAmmo;
    }

    private bool SpeedIncreased()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private void FireLaser()
    {
        // Fire MultipleShot
        if (isMultipleShotActive)
        {
            canFire = Time.time + MultipleShotFireRate;
            Instantiate(multipleShotPrefab, firepoint.transform.position, Quaternion.identity);
            isMultipleShotActive = false;
        }
        // Shot triple laser
        else if (isTripleLaserActive)
        {
            canFire = Time.time + fireRate;
            Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        // Shot one laser
        else
        {
            canFire = Time.time + fireRate;
            Vector2 laserPos = transform.position + new Vector3(0, 1.0f);
            Instantiate(laserPrefab, laserPos, Quaternion.identity);
        }

        ammoAvailable--;
        audioSource.Play();

        UpdateAmmo();
    }

    public void Damage(int damage)
    {
        lives -= damage;
        UpdatePlayerState(lives);

        if (lives == 0)
        { Destroy(gameObject); }
    }

    private void UpdatePlayerState(int playerLives)
    {
        uiManager.UpdateLives(lives);

        if (isShieldActive) ChangeShieldColor();

        if (lives == 3)
        {
            leftEngine.SetActive(false);
            rightEngine.SetActive(false);
        }
        if (lives == 2)
        {
            leftEngine.SetActive(true);
            rightEngine.SetActive(false);
        }
        else if (lives == 1)
        {
            leftEngine.SetActive(true);
            rightEngine.SetActive(true);

        }
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
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
        isMultipleShotActive = true;
        ammoAvailable++;
        UpdateAmmo();
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
        lives = 3;
        UpdatePlayerState(lives);
    }

    public void ChangeShieldColor()
    {
        switch (lives)
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
                spawnManager.OnPlayerDestroyed();
                Destroy(gameObject);
                break;
        }
    }

    public void UpdateAmmo()
    {
        if (ammoAvailable > ammoMax)
        { ammoAvailable = ammoMax; }

        uiManager.UpdateAmmo(ammoAvailable, ammoMax);

        hasAmmo = (ammoAvailable >= 1) ? true : false;

        if (!hasAmmo)
        {
            uiManager.UpdateAmmo(ammoAvailable, ammoMax);
            uiManager.OnEmptyAmmo();
        }
    }

    public void RefillAmmo()
    {
        ammoAvailable = 15;
        UpdateAmmo();

        uiManager.OnFullAmmo();
    }

    public void RecoverHealth()
    {
        if (lives >= 3) return;
        lives++;
        UpdatePlayerState(lives);
    }

    #endregion
}