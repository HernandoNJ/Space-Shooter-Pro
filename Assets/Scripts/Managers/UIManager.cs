using System;
using System.Collections;
using EnemyLib;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject ammoCounter;
    [SerializeField] private GameObject ammoImage;
    [SerializeField] private GameObject emptyAmmoImage;
    [SerializeField] private Image livesImage;
    [SerializeField] private Image thrusterBar;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Sprite[] livesSprites;

    [SerializeField] private int score;

    private void OnEnable()
    {
        Player.onAmmoUpdated += UpdateAmmo; 
        Player.onAddScore += UpdateScore;
    }
    
    private void OnDisable()
    {
        Player.onAmmoUpdated -= UpdateAmmo; 
        Player.onAddScore -= UpdateScore;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("gameManager in UI Manager is null");

        score = 0;
        scoreText.text = "Score: " + score;
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        OnFullAmmo();
        thrusterBar.fillAmount = 0.5f;
    }

    private void UpdateScore(int scorePoints)
    {
        score += scorePoints;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];
        if (currentLives > 0) return;
            GameOverSequence();
    }

    private void SetAmmoValues(int ammoAvailable, int AmmoTotal)
    {
        ammoText.text = ammoAvailable.ToString() + "/" + AmmoTotal.ToString();
    }

    private void UpdateAmmo(int playerAmmoAvailable, int playerAmmoTotal)
    {
        SetAmmoValues(playerAmmoAvailable, playerAmmoTotal);
        if(playerAmmoAvailable <= 0) OnEmptyAmmo();
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
        if (thrusterBar.fillAmount >= 1.0f)
            return;

        thrusterBar.fillAmount += fillAmount;
    }

    public void DecreaseThrusterBar(float emptyAmount)
    {
        if (thrusterBar.fillAmount <= 0.5f)
            return;

        thrusterBar.fillAmount -= emptyAmount;
    }

    private void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        gameManager.OnGameOver();
    }

    private IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.35f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.35f);
        }
    }
}

// TODO fix Multi shot mechanic
