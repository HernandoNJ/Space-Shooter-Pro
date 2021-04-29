using UnityEngine;

public class Wizard : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public Spell[] spells;

    private void Update()
    {
        // Cast Ice blast only if in required level
        // Check if it has the proper Id
        // When hitting space, Cast only the spell in accordance to the level

        // Iterate through spells[] and compare to my current level
        // Cast spell
        // Modify level value in inspector for testing

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var spell in spells)
            {
                if (spell.levelRequired == level)
                {
                    spell.Cast();
                    // here can add particle effect
                    exp += spell.expGained;
                }
            }
        }
    }

}
