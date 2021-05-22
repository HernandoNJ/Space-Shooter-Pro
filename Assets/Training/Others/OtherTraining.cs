using System;
using UnityEngine;

public class OtherTraining : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up * (2 * Time.deltaTime));
    }
}