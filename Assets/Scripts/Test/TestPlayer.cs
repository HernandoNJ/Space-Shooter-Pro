using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    /*
Create a 5 pos array
method with random index
method to set the position to that sub index
    */

    public Vector3[] positions;
    private int posIndex;

    private void Start()
    {
        posIndex = SetIndex();
        transform.position = SetPosition(posIndex);
        Debug.Log("Index: " + posIndex);
    }

    private int SetIndex() => Random.Range(1, 5);
    private Vector3 SetPosition(int index)=> positions[index];


}

/* 
********* Apply Damage *************

public float health = 30f;
void Update(){{
    if (Input.GetKeyDown(KeyCode.Space) & !IsDead())
        Damage(Random.Range(1f, 4f)); }

public void Damage(float damage){
    health -= damage;
    if (IsDead()) { health = 0; Debug.Log("Player is dead"); } }
****************** ----- ***************

************* Get All Players ***********
public GameObject[] players;

    private void Start() => players = GetPlayers();

    GameObject[] GetPlayers() {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player"); }
****************** ----- ***************

************* Random Position ***********

public Vector3[] positions;
private int posIndex;

private void Start()
{
    posIndex = SetIndex();
    transform.position = SetPosition(posIndex);
    Debug.Log("Index: " + posIndex);
}

private int SetIndex() => Random.Range(1, 5);
private Vector3 SetPosition(int index)=> positions[index];


*/