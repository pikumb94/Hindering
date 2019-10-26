using UnityEngine;
using System.Collections;

namespace SpaceRage {

    public class BackgroundScroll : MonoBehaviour {
    
        float speed = -.5f;
        Vector3 start_position;
    
        // Use this for initialization
        void Start () {
            start_position = transform.position;
        }
        
        // Update is called once per frame
        void Update () {
            transform.position = start_position + transform.up * Mathf.Repeat(Time.time * speed, 8f);
        }
    }
    
}