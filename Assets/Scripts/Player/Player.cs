using ScriptableObjects.Player;
using UnityEngine;

namespace Player
{
public class Player : MonoBehaviour
{
    public PlayerData playerData;

    private void Start()
    {
        transform.position = Vector3.down * 3;
    }

}
}
