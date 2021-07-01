using System.Collections;
using System.Collections.Generic;
using EnemyNS;
using PlayerNS;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace Managers
{
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ammoImage;
    [SerializeField] private GameObject emptyAmmoImage;
    [SerializeField] private Image livesImage;
    [SerializeField] private Image thrusterBar;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bossHealthText;
    [SerializeField] private Text playerHealthForBossText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Sprite[] livesSprites;
    [SerializeField] private int playerScore;
    [SerializeField] private int playerHealthValue;
    [SerializeField] private int bossHealthValue;
    [SerializeField] private bool bossActive;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("UIManager is null");
            return _instance;
        }
    }

    private void Awake() => _instance = this;

    private void OnEnable()
    {
        WeaponsManager.OnAmmoChanged += UpdateAmmoCounter;

        PlayerHealth.OnHealthChanged += SetPlayerHealthSprites;
        PlayerHealth.OnPlayerDestroyed += GameOverSequence;

        EnemyBase.OnEnemyDestroyed += UpdateScore;

        EnemyBoss.OnBossStarted += SetUIForBossWave;
        EnemyBoss.OnBossStarted += SetPlayerForBossWaveHealth;
        EnemyBoss.OnBossDamaged += UpdateBossHealth;
        EnemyBoss.OnBossDestroyed += PlayerWins;
    }

    private void OnDisable()
    {
        WeaponsManager.OnAmmoChanged -= UpdateAmmoCounter;

        PlayerHealth.OnHealthChanged -= SetPlayerHealthSprites;
        PlayerHealth.OnPlayerDestroyed -= GameOverSequence;

        EnemyBase.OnEnemyDestroyed -= UpdateScore;

        EnemyBoss.OnBossStarted -= SetUIForBossWave;
        EnemyBoss.OnBossStarted -= SetPlayerForBossWaveHealth;
        EnemyBoss.OnBossDamaged -= UpdateBossHealth;
        EnemyBoss.OnBossDestroyed -= PlayerWins;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("gameManager in UI Manager is null");

        playerScore = 0;
        scoreText.text = "Score: " + playerScore;

        bossHealthText.gameObject.SetActive(false);
        playerHealthForBossText.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        SetPlayerHealthSprites(3);
        OnFullAmmo();
        thrusterBar.fillAmount = 0.5f;
    }

    private void UpdateScore()
    {
        playerScore++;
        scoreText.text = "Score: " + playerScore;
    }

    public void SetPlayerHealthSprites(int currentLives)
    {
        playerHealthValue--;
        playerHealthForBossText.text = "Player health: " + playerHealthValue;
        if (playerHealthValue <= 3 && playerHealthValue >= 0 && currentLives >=0)
            livesImage.sprite = livesSprites[currentLives];
    }

    private void SetPlayerForBossWaveHealth(int intValue)
    {
        playerHealthValue = 100;
        playerHealthForBossText.gameObject.SetActive(true);
        UpdatePlayerForBossWaveHealth(playerHealthValue);
        livesImage.gameObject.SetActive(false);
        thrusterBar.gameObject.SetActive(false);
    }

    private void UpdatePlayerForBossWaveHealth(int newHealth)
    {
        playerHealthForBossText.text = "Player health: " + newHealth;
    }

    private void SetUIForBossWave(int bossInitialHealth)
    {
        bossActive = true;
        bossHealthValue = bossInitialHealth;
        bossHealthText.text = "Boss health: " + bossHealthValue;
        bossHealthText.gameObject.SetActive(true);
    }

    private void UpdateBossHealth(int bossHealth)
    {
        bossHealthText.text = "Boss health: " + bossHealth;
        UpdateScore();
    }

    private void SetAmmoValues(int ammoAvailable, int ammoTotal)
    {
        ammoText.text = ammoAvailable.ToString() + "/" + ammoTotal.ToString();
    }

    private void UpdateAmmoCounter(int playerAmmoAvailable, int playerAmmoTotal)
    {
        SetAmmoValues(playerAmmoAvailable, playerAmmoTotal);
        if (playerAmmoAvailable <= 0) OnEmptyAmmo();
        if (playerAmmoAvailable == playerAmmoTotal) OnFullAmmo();
    }

    private void OnEmptyAmmo()
    {
        emptyAmmoImage.SetActive(true);
        emptyAmmoImage.GetComponent<Animator>().SetBool("isAmmoEmpty", true);
        ammoImage.SetActive(false);
    }

    public void OnFullAmmo()
    {
        emptyAmmoImage.SetActive(false);
        ammoImage.SetActive(true);
    }

    public void IncreaseThrusterBar(float fillAmount)
    {
        if (thrusterBar.fillAmount >= 1.0f) return;
        thrusterBar.fillAmount += fillAmount;
    }

    public void DecreaseThrusterBar(float emptyAmount)
    {
        if (thrusterBar.fillAmount <= 0.5f) return;
        thrusterBar.fillAmount -= emptyAmount;
    }

    private void PlayerWins()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine("Player Wins !!!"));
        gameManager.SetGameOver();
    }

    private void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine("Game Over"));
        gameManager.SetGameOver();
    }

    private IEnumerator GameOverFlickerRoutine(string gameOverMessage)
    {
        var showMessageTime = Time.time + 5f;
        while (showMessageTime > Time.time)
        {
            gameOverText.text = gameOverMessage;
            yield return new WaitForSeconds(0.35f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }
}
}
