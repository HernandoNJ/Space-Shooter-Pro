using System.Collections;
using UnityEngine;

// todo: when changing name to EnemyLaserBackwards to LaserUp, the reference in the editor was broken, and I had to drag and drop the game object again.

namespace EnemyNS
{
public class EnemyShotBackwards : EnemyBase
{
    [SerializeField] private bool shootBackActive;
    [SerializeField] private GameObject backwardsLaser;
    [SerializeField] private int enemyAttackLayer;

    protected override void ConfigureEnemy(EnemyData _data)
    {
        base.ConfigureEnemy(_data);
        enemyAttackLayer = 13;
        shootBackActive = false;
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        ShootBackwards();
    }

    // todo Ask to a coach
    private void ShootBackwards()
    {
        // var rayHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 10f,
        //     enemyAttackLayer);
        Debug.DrawRay(transform.position, Vector3.up * 3f, Color.red);

        var rayHit = Physics2D.Raycast(transform.position, transform.up, 10f);
        var rayHitCol = rayHit.collider;

        if (!rayHitCol.CompareTag("Player")) return;

        Debug.Log("hit found. tag: " + rayHitCol.tag);
        shootBackActive = true;
        StartCoroutine(ShootBackwardsLaserRoutine());
    }

    private IEnumerator ShootBackwardsLaserRoutine()
    {
        if (!shootBackActive) yield break;
        Instantiate(backwardsLaser);
        shootBackActive = false;
        yield return new WaitForSeconds(3);
    }
}
}
