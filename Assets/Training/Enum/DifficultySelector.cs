using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public enum Difficulty
    {
        Easy = 1, Normal = 14, Hard = 30, Expert = 50
    }

    [SerializeField] private Difficulty currentLevel;

    private void Start()
    {
        switch (currentLevel)
        {
            case Difficulty.Easy: 
                Debug.Log("Easy selected"); break;
            case Difficulty.Normal:
                Debug.Log("Normal selected"); break;
            case Difficulty.Hard:
                Debug.Log("Hard selected"); break;
        }
    }

}
