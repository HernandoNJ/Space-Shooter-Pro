using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string notes = "This component shouldn't be removed, it does important stuff.";

}
