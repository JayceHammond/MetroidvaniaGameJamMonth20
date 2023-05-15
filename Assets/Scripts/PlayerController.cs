using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public bool groundState;
    private void Start() {

    }

    private void FixedUpdate() {
        
    }

    private void Update() {
        RaycastHit hit;


        if(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1.25f, LayerMask.GetMask("Ground"))){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down), Color.green);
            groundState = true;
        }
        else{
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down), Color.red);
            groundState = false;
        }

        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && groundState == true){
            transform.Translate(0, Mathf.Pow(10, jumpSpeed) * Time.deltaTime, 0);
        }
        
    }
}
