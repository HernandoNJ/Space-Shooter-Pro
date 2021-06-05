using System.Collections.Generic;
using UnityEngine;

namespace Weapon.Lasers
{
public class LasersPoolManager : SingletonBP<LasersPoolManager>
{

    /*
     * Define LasersPool
     * Define Lasers parents
     *    parents array
     * Define if lasers amount = 1, 2 or 3
     *  Create 3 laser weapons SO
     *  Create an array of lasers
     *  Define a parent for each one
     *
     *
     *
     */

    [SerializeField] private UnityEngine.GameObject laserPrefab;
    [SerializeField] private List<UnityEngine.GameObject> laserPool;
    public Transform[] firePoints;

    private void Start()
    {
        LasersGenerator(10);
    }

    private List<UnityEngine.GameObject> LasersGenerator(int lasersAmount)
    {
        for (int i = 0; i < lasersAmount; i++)
        {
            UnityEngine.GameObject laser = Instantiate(laserPrefab);
            laser.SetActive(false);
            laserPool.Add(laser);
        }

        return laserPool;
    }

    public UnityEngine.GameObject GetOneBullet()
    {
        foreach (var laser in laserPool)
        {
            if (laser.activeInHierarchy == false)
            {
                laser.transform.SetParent(firePoints[0]);
                laser.SetActive(true);
                return laser;
            }
        }

        UnityEngine.GameObject newLaser = Instantiate(laserPrefab, firePoints[0].transform);
        laserPool.Add(newLaser);

        return newLaser;
    }
}
}
