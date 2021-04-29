using UnityEngine;

namespace Testing
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Players p1 = new Players();
            Players p2 = new Players();
            Players p3 = new Players();
            Players p4 = new Players();
            Players p5 = new Players();
            Players p6 = new Players();

            Debug.Log("Players count: " + Players.playersConnected);

        }
    }
    public class Players
    {
        public int id;
        public string name;

        // This variable is shared among every Players instance
        public static int playersConnected;
        public Players()
        {
            playersConnected++;
        }
    }
}