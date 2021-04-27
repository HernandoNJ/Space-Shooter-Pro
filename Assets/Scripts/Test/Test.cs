using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cube;

    void Start()
    {
        ChangeColor(cube, Color.blue);
    }

  public void ChangeColor(GameObject g, Color c){
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
