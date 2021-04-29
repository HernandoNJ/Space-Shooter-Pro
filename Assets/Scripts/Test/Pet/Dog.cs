using UnityEngine;

public class Dog : Pet
{
    protected override void Sound()
    {
        Debug.Log("Bark! -- " + gameObject.name);
    }
}
