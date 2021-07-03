using UnityEngine;

namespace z_others
{
public class TestScript : MonoBehaviour
{
    public int[] tableArray = {60, 30, 10};
    public int totalTableArray;


    private void Start()
    {
        foreach (var t in tableArray)
        {
            totalTableArray += t;
        }
    }
}
}
