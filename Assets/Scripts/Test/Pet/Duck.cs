using UnityEngine;

public class Duck : Pet
{
    Pet pet;
    protected override void Sound()
    {
        Debug.Log("Pet name: " +  gameObject.name + "  -- Quack Quack");
        
    }
}
