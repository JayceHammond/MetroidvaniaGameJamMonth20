using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public bool groundState;
    public Rigidbody2D rb;
    public float fCutJumpHeight;
    public ArrayList healthBar = new ArrayList();
    public int maxHealth;
    public int health;
    public Canvas healthCanvas;
    public Image highHealth;
    public Image medHealth;
    public Image lowHealth;
    private bool faceBackward = false;

    private void Start() {
        health = maxHealth;
        for(int i = 0; i < maxHealth; i++){
            healthBar.Add(highHealth);
        }

        int pos = 0;
        foreach(Image frog in healthBar){
            Image img = Instantiate(frog);
            img.transform.SetParent(healthCanvas.transform, false);
            img.transform.position = new Vector2(img.transform.position.x + pos, img.transform.position.y);
            pos += 100;
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.UpArrow) && groundState == true){
            //transform.Translate(0, Mathf.Pow(10, jumpSpeed) * Time.deltaTime, 0);
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
        if(health == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
        
    }

    public void Move(){
        if(Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1.25f, LayerMask.GetMask("Ground"))){
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down), Color.green);
            groundState = true;
        }
        else{
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down), Color.red);
            groundState = false;
        }

        if(Input.GetKey(KeyCode.RightArrow)){
            if(faceBackward == true){
                this.GetComponent<SpriteRenderer>().flipX = false;
                faceBackward = false;
            }
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            if(faceBackward == false){
                this.GetComponent<SpriteRenderer>().flipX = true;
                faceBackward = true;
            }
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
    }

    public void TakeDamage(){
            Debug.Log("Destroy");
            Destroy(healthCanvas.transform.GetChild(health - 1).gameObject);
            health -= 1;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Enemy"){
            TakeDamage();
        }
        if(col.gameObject.tag == "Bounds"){
            TakeDamage();
            transform.position = new Vector2(0, 0);
        }
        Image[] arr = healthCanvas.transform.GetComponentsInChildren<Image>();
        if(health == 2){
            foreach(Image img in arr){
                img.sprite = medHealth.sprite;
            }
        }else if(health == 1){
            foreach(Image img in arr){
                img.sprite = lowHealth.sprite;
            }
        }
    }
}
