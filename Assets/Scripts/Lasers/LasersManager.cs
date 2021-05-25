using System.Collections.Generic;
using UnityEngine;

namespace Lasers{
public class LasersManager : SingletonBP<LasersManager>{

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

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private List<GameObject> laserPool;
    [SerializeField] private GameObject[] laserFirePoints;

    private void Start()
    {
        LasersGenerator(10);
        Debug.Log("Hello");
    }

    private List<GameObject> LasersGenerator(int bulletsAmount)
    {
        for (int i = 0; i < bulletsAmount; i++)
        {
            GameObject laser = Instantiate(laserPrefab, gameObject.transform, true);
            laser.SetActive(false);
            laserPool.Add(laser);
        }

        return laserPool;
    }

    public GameObject GetOneBullet()
    {
        foreach (var laser in laserPool)
        {
            if (laser.activeInHierarchy == false)
            {
                laser.SetActive(true);

                return laser;
            }
        }

        GameObject newLaser = Instantiate(laserPrefab, laserFirePoints[0].transform, true);
        laserPool.Add(newLaser);

        return newLaser;
    }
}
}