using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patroling, Chasing, Attacking, Death
    }

    public EnemyState currentEnemyState;

    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Patroling:
                Debug.Log("Patroling");
                if (Time.time > 5)
                    // Change enemy state
                    currentEnemyState = EnemyState.Attacking;
                break;
            case EnemyState.Chasing:
                Debug.Log("Chasing");
                if (Time.time > 5)
                    currentEnemyState = EnemyState.Attacking;
                break;
            case EnemyState.Attacking:
                Debug.Log("Attacking"); break;
            case EnemyState.Death:
                Debug.Log("Death"); break;
        }

        // Modify enemy state
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentEnemyState = EnemyState.Patroling;
        }
    }
}
