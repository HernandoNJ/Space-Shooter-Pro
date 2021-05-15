using UnityEngine;

public abstract class AbsBall : MonoBehaviour
{

    public string ballName;
    public float volume;

    private void Start()
    {

    }

    public abstract void SetBallName(string n);

    public void SetVolume(float vol)
    {
        Debug.Log("Volume: " + vol);
    }

}
