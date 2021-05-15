using UnityEngine;
public class BasketBall : Ball
{


    private void Start()
    {
        myInt = 3;
        myString = "My string";

        DefineName("param in DefineName");
        DefineVolume(4.5f);
        Debug.Log("Bask int: " + myInt + " - Bask string: " + myString);

    }




}
