using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cube1;

    void Start()
    {
        ChangeColor(cube1, Color.blue);
    }

    public void ChangeColor(GameObject g, Color c)
    {
        g.GetComponent<Renderer>().material.color = c;
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
