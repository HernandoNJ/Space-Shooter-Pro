using UnityEngine;

public class Enemi : MonoBehaviour
{
    private ManagerUI uI;
    private void OnEnable()
    {
        Spawner.enemyCount++;
        uI = GameObject.Find("ManagerUI").GetComponent<ManagerUI>();
        uI.UpdateEnemyCount();
        Die();
    }

    private void OnDisable()
    {
        Spawner.enemyCount--;
        uI.UpdateEnemyCount();
    }

    void Die() => Destroy(gameObject, 2f);
}

