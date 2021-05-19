using UnityEngine;

public class Target : MonoBehaviour, IDamage
{
    /* It can be anything like a player or enemy */

    [SerializeField] private int currentHealth = 10;
    //[SerializeField] private FloatingText floatingText;
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // if(floatingText != null)
        //     floatingText.SetText("<color=red>" + damageAmount + "</color>");



    }

    public void Freeze(int seconds)
    {
        Debug.Log("Frozen for: " + seconds);
        // if (floatingText != null)
        //     floatingText.SetText("<color=blue>Frozen</color>");


    }

}
