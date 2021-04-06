using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;  
    [SerializeField] private Text gameOverText;  
    [SerializeField] private Text restartText;  
    [SerializeField] private Image livesImage;  
    [SerializeField] private GameObject emptyAmmoImage;  
    [SerializeField] private GameManager gameManager;  
    [SerializeField] private Sprite[] livesSprites;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null) Debug.LogError("gameManager in UI Manager is null");

        scoreText.text = "Score: 0"; 
        gameOverText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        emptyAmmoImage.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Remove code lines");
        if (Input.GetKeyDown(KeyCode.R)) OnEmptyAmmo();
        if (Input.GetKeyDown(KeyCode.T)) OnFullAmmo();
    }

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        livesImage.sprite = livesSprites[currentLives];
        
        if (currentLives == 0)
            GameOverSequence();
    }

    public void OnEmptyAmmo()
    {
        emptyAmmoImage.SetActive(true);
        emptyAmmoImage.GetComponent<Animator>().SetBool("isAmmoEmpty", true);
    }

    public void OnFullAmmo()
    {
        emptyAmmoImage.SetActive(false);
        emptyAmmoImage.GetComponent<Animator>().SetBool("isAmmoEmpty", false);
    }

    public void GameOverSequence()
    {
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        gameManager.OnGameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.7f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.7f);
        }
    }
}
