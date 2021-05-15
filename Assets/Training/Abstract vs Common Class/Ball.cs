using System;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int myInt = 75;
    public string myString = "Hey";

    private void Start()
    {
        Debug.Log($"int in Ball: {myInt}");
        Debug.Log(String.Format("myString in Ball: {0}", myString));
    }

    public void DefineName(string n) => Debug.Log("SetBallName: " + n);

    public void DefineVolume(float vol) => Debug.Log("Define Volume: " + vol);
}
