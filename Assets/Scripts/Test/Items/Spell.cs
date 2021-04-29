using UnityEngine;

[System.Serializable]
public class Spell
{
    public string name;
    public int levelRequired;
    public int itemIdRequired;
    public int expGained;

    public Spell(string name, int levelRequired, int itemIdRequired, int exp)
    {
        this.name = name;
        this.levelRequired = levelRequired;
        this.itemIdRequired = itemIdRequired;
        this.expGained = exp;
    }

    // Throw spell
    public void Cast()
    {
        Debug.Log("Casting spell: " + name);
    }
}
