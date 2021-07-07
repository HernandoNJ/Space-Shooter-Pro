using System;
using System.Collections;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyNS
{
public class EnemyBoss : MonoBehaviour, IDamageable
{
    public EnemyData bossData;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;
    [SerializeField] private Transform moveTo;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float maxRotation;
    [SerializeField] private float rotationValue;
    [SerializeField] private int flipValue;
    [SerializeField] private int randomPos;
    [SerializeField] private float speed;
    [SerializeField] private bool bossActive;
    [SerializeField] private bool rotateActive;


    public static event Action<int> OnBossStarted;
    public static event Action<int> OnBossDamaged;
    public static event Action OnBossDestroyed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        speed = bossData.speed;
        maxHealth = bossData.maxHealth;
        currentHealth = maxHealth;
        OnBossStarted?.Invoke(maxHealth);

        rotateActive = true;
        bossActive = true;
        maxRotation = 60;
        flipValue = 1;

        point1 = GameObject.Find("BossPoint1").transform;
        point2 = GameObject.Find("BossPoint2").transform;
        point3 = GameObject.Find("BossPoint3").transform;
        moveTo = point1;

        transform.position = new Vector3(0, 2.5f, 0);

        StartCoroutine(SwitchMoveRoutine());
    }

    private void Update()
    {
        MoveBoss();
        if(rotateActive) RotateBoss();
    }

    private void MoveBoss()
    {
        transform.position = Vector3.MoveTowards(transform.position,moveTo.position,speed*Time.deltaTime);
    }

    private void RotateBoss()
    {
        rotationValue += flipValue;
        if (rotationValue > maxRotation) flipValue = -1;
        if (rotationValue < -maxRotation) flipValue = 1;
        transform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        OnBossDamaged?.Invoke(currentHealth);
        if (currentHealth > 0) return;
        BossDestroyed();
    }

    private void SetRandomPos(int randomInt)
    {
        if (randomInt == 1) moveTo = point1;
        else if (randomInt == 2) moveTo = point2;
        else if (randomInt == 3) moveTo = point3;
    }

    private void BossDestroyed()
    {
        bossActive = false;
        OnBossDestroyed?.Invoke();
        Instantiate(bossData.explosionPrefab, transform.position, Quaternion.identity);
        speed = 0;
        animator.SetTrigger("OnEnemyDestroyed");
        audioSource.Play();
        Destroy(GetComponent<Collider2D>());
        gameObject.SetActive(false); // TODO: fix added because enemy was hitting the player twice
        StopAllCoroutines();
        Destroy(gameObject, 2.0f);
    }

    private IEnumerator SwitchMoveRoutine()
    {
        while (bossActive)
        {
            yield return new WaitForSeconds(2);
            rotateActive = false;
            speed += 2f;
            randomPos = Random.Range(1, 4);
            SetRandomPos(randomPos);
            RotateBoss();

            yield return new WaitForSeconds(2);
            rotateActive = true;
            speed  -= 2f;
            randomPos = Random.Range(1, 4);
            SetRandomPos(randomPos);
        }
    }
}
}
