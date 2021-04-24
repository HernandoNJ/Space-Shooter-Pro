using UnityEngine;

namespace Events
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Player.OnDeath += ResetPlayer;

        }
        public void ResetPlayer() => Debug.Log("Reset player");

        private void OnDisable()
        {
            Player.OnDeath -= ResetPlayer;
        }

    }
}
