using UnityEngine;
using  Training.Others;

public class EndTrigger : MonoBehaviour
{
    public delegate void OnEndReached(Vector3 pos, Mech mech);
    public static event OnEndReached onEndReached;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other: "+ other.name);
        var mech = other.GetComponent<Mech>();
        if(other.tag == "Enemy") onEndReached?.Invoke(transform.position, mech);
    }
}
