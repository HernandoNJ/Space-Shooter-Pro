using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Training.Others
{
    public class Mech : MonoBehaviour
    {
        private MeshRenderer _render;
        private float speed = 5f;

        private void Start()
        {
            _render = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {    
            EndTrigger.onEndReached += FinishedWave; 
        }

        private void OnDisable()
        {
            EndTrigger.onEndReached -= FinishedWave;
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(h, 0, v);
            Vector3 velocity = direction * speed;
            transform.Translate(velocity * Time.deltaTime);
        }

        private void FinishedWave(Vector3 newPos, Mech mech)
        {
            if(this == mech) Debug.Log("this is me: "  + name);
            
            // if(transform.position = newPos) return;
            // transform.position = newPos;
        
            // float rand = Random.value;
            // Color newColor = new Color(rand,rand,rand);
            // _render.material.color = newColor;
        
            StartCoroutine(ColorChange());
        
        }

        private IEnumerator ColorChange()
        {
            yield return new WaitForSeconds(1);
            Color newColor = new Color(Random.value, Random.value, Random.value);
            _render.material.color = newColor;
        }
    }
}
