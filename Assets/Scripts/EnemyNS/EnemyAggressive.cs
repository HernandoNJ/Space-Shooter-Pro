using PlayerNS;
using UnityEngine;

namespace EnemyNS
{
public class EnemyAggressive : EnemyBase
{
    [SerializeField] private Transform target;
    [SerializeField] private int xFlip = 1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float maxRotation = 60f;
    [SerializeField] private float detectionArea;
    [SerializeField] private Player player;

    protected override void ConfigureEnemy(EnemyData _data)
    {
        base.ConfigureEnemy(_data);
        player = GameObject.Find("Player").GetComponent<Player>();
        detectionArea = 4f;
    }

    // todo: ask a coach about line 25
    protected override void MoveEnemy()
    {
        //RamWithRaycast();
        RamTarget();
        CheckBottomPosition();
    }

    private void RamTarget()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionArea)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }
        else
            transform.Translate(Vector3.down * (enemySpeed * Time.deltaTime));
    }

    private void RamWithRaycast()
    {
        rotationSpeed += xFlip;
        if (rotationSpeed > maxRotation) xFlip = -1;
        if (rotationSpeed < -maxRotation) xFlip = 1;
        transform.rotation = Quaternion.Euler(0, 0, rotationSpeed);

        var origin = transform.position;
        var direction = transform.rotation * Vector3.down; // for rotating draw gizmo
        Debug.DrawRay(origin, direction * 3f, Color.cyan);
        bool objectHit = Physics.Raycast(new Ray(origin, direction), out RaycastHit hitInfo);
        if (objectHit)
        {
            Debug.Log("object hit: " + hitInfo.collider.tag);
            if (hitInfo.collider.CompareTag("Player"))
                transform.position = hitInfo.transform.position - transform.position;
        }
        else transform.position += Vector3.down * (enemySpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionArea);
    }
}
}
