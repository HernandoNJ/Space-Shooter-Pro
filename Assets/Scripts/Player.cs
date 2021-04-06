using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip laserSound;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject leftEngine;
    [SerializeField] private GameObject rightEngine;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private bool tripleLaserActive;
    [SerializeField] private bool speedBoostActive;
    [SerializeField] private float canFire = 0f;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float speedUpSpeed = 2f;
    [SerializeField] private float leftShiftSpeedMult = 1.5f;
    [SerializeField] private float totalSpeed;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private int lives = 4;
    [SerializeField] private int score;
    [SerializeField] private int ammoCount;

    private SpawnManager spawnManager;
    private AudioSource audioSource;

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

        shield.SetActive(false);
        leftEngine.SetActive(false);
        rightEngine.SetActive(false);

        //Limit the lasers fired by the player to only 15 shots.
        ammoCount = 15;
        uiManager.UpdateAmmo(ammoCount);
        
    }

    private void Update()
    {
        MovePlayer();

        // if Time.time = 5, Time.time > 0f => true
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            // player must wait until Time.time = 5.8f to shoot again
            FireLaser();
        }
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        // Increase speed with speedUp powerup
        if (speedBoostActive)
        {
            totalSpeed = playerSpeed * speedUpSpeed;
            transform.Translate(moveDirection * totalSpeed * Time.deltaTime);
        }
        // Increase speed with Left shift key
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            totalSpeed = playerSpeed * leftShiftSpeedMult;
            transform.Translate(moveDirection * totalSpeed * Time.deltaTime);
        }
        // Normal Player movement
        else
        {
            totalSpeed = playerSpeed; // Just for testing
            transform.Translate(moveDirection * playerSpeed * Time.deltaTime);
        }

        // Set up player movement constraints 
        float xPos = transform.position.x;
        float yPos = transform.position.y;

        if (yPos >= 0f) transform.position = new Vector2(xPos, 0f);
        else if (yPos <= -3.5f) transform.position = new Vector2(xPos, -3.5f);

        if (xPos >= 10.4f) transform.position = new Vector2(-10.4f, yPos);
        else if (xPos <= -10.4f) transform.position = new Vector2(10.4f, yPos);
    }

    private void FireLaser()
    {
        //When the player is out of ammo,display empty ammo anim in UI
            // If ammo = 0
                // return
                // play empty ammo anim in UI
            // If player shots
                // decrease ammo amount
            
        if(ammoCount == 0)
        {
            uiManager.OnEmptyAmmo();
            return;
        }     

        // canFire = 5 + 0.7f = 5.7f
        canFire = Time.time + fireRate;

        audioSource.Play();

        // Shot triple laser
        if (tripleLaserActive)
        {
            Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
            ammoCount -= 1;
            if (ammoCount > 0) uiManager.UpdateAmmo(ammoCount);
        }
        // Shot one laser
        else
        {
            Vector2 laserPos = transform.position + new Vector3(0, 1.0f);
            Instantiate(laserPrefab, laserPos, Quaternion.identity);
            ammoCount -= 1;
            if(ammoCount > 0) uiManager.UpdateAmmo(ammoCount);
        }
    }

    public void Damage(int damage)
    {
        lives -= damage;
        Debug.Log("Player lives: " + lives);
        Debug.Break();

        uiManager.UpdateLives(lives);

        // Change shield color
        switch (lives)
        {
            case 3:
                shield.GetComponent<Renderer>().material.color = new Color(1,1,1,0.8f);
                break;
            case 2:
                shield.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.6f);
                leftEngine.SetActive(true);
                break;
            case 1:
                shield.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.3f);
                rightEngine.SetActive(true);
                break;
            case 0:
                spawnManager.OnPlayerDestroyed();
                Destroy(gameObject);
                break;
        }

        //switch (lives)
        //{
        //    case 3:
        //        shield.GetComponent<Renderer>().material.color = Color.gray;
        //        break;
        //    case 2:
        //        shield.GetComponent<Renderer>().material.color = Color.yellow;
        //        leftEngine.SetActive(true);
        //        break;
        //    case 1:
        //        shield.GetComponent<Renderer>().material.color = Color.red;
        //        rightEngine.SetActive(true);
        //        break;
        //    case 0:
        //        spawnManager.OnPlayerDestroyed();
        //        Destroy(gameObject);
        //        break;
        //}
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
        shield.SetActive(true);
    }
    //Create a powerup to fill the ammo count allowing the player to fire again
    // Create pic
    // Create anim
    // Create powerup prefab
        // Set the ammoAmount value to 15
        // update ammo value with ui.updateammo
        // Call onfullammo in UI
        // Increase powerup array value in spawnmanager
        // attach poweru☺p.cs
        // Create a new case in powerup.cs switch in ontrigger
        // setup powerup sound in prefab
        // set powerup id
        // Update info in Start for UI with ShowStartAmmo (when game starts or restarted
        //
        
    public void RefillAmmoPowerup()
    {
        ammoCount = 15;
        uiManager.UpdateAmmo(ammoCount);
        uiManager.OnFullAmmo();
    }

    #endregion
}