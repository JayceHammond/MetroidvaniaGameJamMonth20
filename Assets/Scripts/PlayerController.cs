using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
        private void Start() {
        
    }

    private void FixedUpdate() {
        
    }

    private void Update() {
        if(Input.GetKey(KeyCode.RightArrow)){
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            this.transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            this.transform.Translate(0 ,jumpSpeed * Time.deltaTime, 0);
        }
        
    }
}
