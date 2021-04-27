using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutineCheck : MonoBehaviour
{
    public bool isVisible;

    private void Start()
    {
        isVisible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && isVisible)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            Debug.Log("Entering routine");
            StartCoroutine(HideShowRoutine());
            isVisible = false;
        }
    }

    IEnumerator HideShowRoutine()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Renderer>().enabled = true;
        Debug.Log("Exiting 5 seconds");
        isVisible = true;
    }
}
