using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon.Lasers;

namespace Weapon
{
public class WeaponsBossManager : MonoBehaviour
{
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private int laserIndex;
    [SerializeField] private int rocketIndex;

    private void Start()
    {
        SetWeaponsValues();
        StartCoroutine(FireBossWeaponsRoutine());
    }

    private void SetWeaponsValues()
    {
        laserIndex = 0;
        rocketIndex = 1;
    }

    private void FireLaser()
    {
        foreach (var t in firePoints)
        {
            Instantiate(weapons[laserIndex], t.position, t.rotation);
        }
    }

    private void FireRocket()
    {
        Instantiate(
            weapons[rocketIndex],
            firePoints[0].position,
            firePoints[0].rotation);
    }

    private IEnumerator FireBossWeaponsRoutine()
    {
        yield return new WaitForSeconds(1);

        var startTime = Time.time;
        float actualTime = 0;

        while (actualTime - startTime < 30)
        {
            Debug.Log("actual time: " + actualTime);
            for (int i = 0; i < 10; i++)
            {
                FireLaser();
                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(2);

            for (int i = 0; i < 3; i++)
            {
                FireRocket();
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(2);
            actualTime = Time.time;
            Debug.Log("actual time: " + actualTime);
        }
    }
}
}
