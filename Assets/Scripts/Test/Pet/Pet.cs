using UnityEngine;

public class Pet : MonoBehaviour
{
    protected string name;

    private void Start()
    {
        Sound();
    }

    protected virtual void Sound()
    {
        Debug.Log("This is my sound");
    }
}
