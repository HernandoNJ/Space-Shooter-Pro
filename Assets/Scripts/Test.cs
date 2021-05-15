using System.Runtime.InteropServices;
using UnityEngine;
using EnemyLib;

public class Test : MonoBehaviour
{
    public int livesT;
    public float speedT;
    public float fireRateT;
    public EnemyAggressive enemyAg;

    private void Start()
    {
        enemyAg = new EnemyAggressive();

        livesT = enemyAg.Lives;
        speedT = enemyAg.Speed;
        fireRateT = enemyAg.FireRate;

        Debug.Log("ag lives: " + livesT + "speed: " + speedT);
    }

}


/*

[SerializeField] private int weaponId;

switch (weaponId)
        {
            case 1: Debug.Log("Gun"); break;
            case 2: Debug.Log("Knife"); break;
            case 3: Debug.Log("Machine Gun"); break;
        }

*/
