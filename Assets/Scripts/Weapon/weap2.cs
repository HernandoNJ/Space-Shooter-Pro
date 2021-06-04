using Weapon;

[System.Serializable]
public class weap2 
{
    public string weaponName;
    public WeaponLauncher weaponLauncher;

    public weap2(string weaponName, WeaponLauncher weaponLauncher)
    {
        this.weaponName = weaponName;
        this.weaponLauncher = weaponLauncher;
    }
    
}
