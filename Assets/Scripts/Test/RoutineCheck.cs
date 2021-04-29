using System.Collections;
using UnityEngine;

public class RoutineCheck : MonoBehaviour
{
    private MeshRenderer mesh;
    private WaitForSeconds waitTime = new WaitForSeconds(0.05f);

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine(){
        while(true){
            mesh.enabled = true;
            mesh.material.color = SetColor();
            yield return waitTime;
            mesh.enabled = false;
            yield return waitTime;
        }
    }

    private Color SetColor(){
        return new Color(Random.value, Random.value, Random.value);
    }
}
