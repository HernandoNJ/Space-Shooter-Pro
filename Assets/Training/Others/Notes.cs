using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea(5, 7)]
    [Tooltip("Just for writing notes")]
    public string notes = "Write your notes here";

}
