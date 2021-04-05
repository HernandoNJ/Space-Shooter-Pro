using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private GameObject blueShieldSprite;
    [SerializeField] private GameObject leftEngine;
    [SerializeField] private GameObject rightEngine;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private float playerSpeed = 5f; 
    [SerializeField] private float speedUpSpeed = 2f;
    [SerializeField] private float leftShiftSpeedMult = 1.5f;
    [SerializeField] private float fireRate = 2.0f; 
    [SerializeField] private int lives = 3, score;
    [SerializeField] private string playerSp;

    private SpawnManager spawnManager;
    private AudioSource audioSource;

    private float  canFire = 0f;
    private bool tripleLaserActive; 
    private bool speedBoostActive; 
    private bool shieldActive; 
    
    private void Start()
    {
        transform.position = new Vector2(0f, -3.0f);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("There is no AudioSource component in Player script");

        audioSource.clip = laserSound;

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (spawnManager == null)
            Debug.LogError("There is no SpawnManager script in spawnManager");

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uiManager == null)
            Debug.LogError("No UI Manager found. Null");

        blueShieldSprite.SetActive(false);
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);
    }

    private void Update()
    {
        // if Time.time = 5, Time.time > 0f => true
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            // player must wait until Time.time = 5.8f to shoot again
            FireLaser(); 
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Increase speed with speedUp powerup
        if (speedBoostActive)
        {
            float speedBoost = playerSpeed * speedUpSpeed;
            transform.Translate(moveDirection * speedBoost * Time.deltaTime);
            playerSp = speedBoost.ToString();
        }
        // Increase speed with Left shift key
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            float LeftShiftSpeedBoost = playerSpeed * leftShiftSpeedMult;
            transform.Translate(moveDirection * LeftShiftSpeedBoost * Time.deltaTime);
            playerSp = LeftShiftSpeedBoost.ToString();
        }
        // Normal Player movement
        else
        {
            transform.Translate(moveDirection * playerSpeed * Time.deltaTime);
            playerSp = playerSpeed.ToString();
        }

        // Player movement constraints
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        
        if (yPos >= 0f) transform.position = new Vector2(xPos, 0f);
        else if (yPos <= -3.5f) transform.position = new Vector2(xPos, -3.5f);

        if (xPos >= 10.4f) transform.position = new Vector2(-10.4f, yPos);
        else if (xPos <= -10.4f) transform.position = new Vector2(10.4f, yPos);
    }

    private void FireLaser()
    {
        // canFire = 5 + 0.7f = 5.7f
        canFire = Time.time + fireRate;

        audioSource.Play();

        if (tripleLaserActive)
        {
            Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Vector2 laserPos = transform.position + new Vector3(0, 1.0f);
            Instantiate(laserPrefab, laserPos, Quaternion.identity);
        }
    }

    public void Damage(float damage)
    {
        if (shieldActive)
        {
            shieldActive = false;
            blueShieldSprite.SetActive(false);
            return;
        }

        lives -= (int)damage;

        uiManager.UpdateLives(lives);

        if (lives == 2) leftEngine.SetActive(true);
        if (lives == 1) rightEngine.SetActive(true);
        
        if (lives == 0)
        {
            spawnManager.OnPlayerDestroyed();
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    #region Powerups
    public void ActivateTripleLaser()
    {
        tripleLaserActive = true;
        StartCoroutine(TripleLaserRoutine());
    }

    IEnumerator TripleLaserRoutine()
    {
        yield return new WaitForSeconds(5f);
        tripleLaserActive = false;
    }

    public void ActivateSpeedBoost()
    {
        speedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }

    IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5f);
        speedBoostActive = false;
    }

    public void ActivateShield()
    {
        shieldActive = true;
        blueShieldSprite.SetActive(true);
    }
    #endregion
}