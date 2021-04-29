[System.Serializable]
public class Customer
{
    public string name;
    public string gender;
    public int age;

    public Customer(string name, int age, string gender)
    {
        this.name = name;
        this.age = age;
        this.gender = gender;
    }
}
