using UnityEngine;

public class Test1 : MonoBehaviour
{
    public string objName;
    public float objStrength;
    public int objForce;

    public StonesT stonesT;

    private void Start()
    {
        stonesT = new StonesT("Stone1", 3f, 5);
        objName = stonesT.objName;
        objStrength = stonesT.stregth;
        objForce = stonesT.force;

    }

    private void Update()
    {

    }
}
