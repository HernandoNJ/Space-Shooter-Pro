using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public Text activeEnemiesText;

    public void UpdateEnemyCount() => 
        activeEnemiesText.text = "Active Enemies: " + Spawner.enemyCount;

}
