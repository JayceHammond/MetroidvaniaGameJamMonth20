using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb; //Get Player rigidbody

    [Header("Public Vars")]
    public GameObject meleeSlashR;
    public GameObject meleeSlashL;
    public GameObject meleeSlashUp;
    public GameObject meleeSlashDown;
    public float fCutJumpHeight;
    public float moveSpeed;
    public float fJumpPressedRemember;
    public float fGroundedRemember = 0;
    public float fJumpPressedRememberTime;
    public float fGroundedRememberTime;
    public float fJumpVelocity;
    public float fallMultiplier = 2.5f;
    public float fHorizontalDamping;
    public bool isFacingRight;
    public bool isFacingLeft;
    public bool grounded;
    private Vector2 facingLeft;

    [Header("Private Vars")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;


    private GameObject meleeAttack;


    // Start is called before the first frame update

    void Start()
    {
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = rb.velocity.x; 
        //horizontalInput += Input.GetAxisRaw("Horizontal");
        //horizontalInput *= Mathf.Pow(1f- fHorizontalDamping, Time.deltaTime * moveSpeed);
        if(horizontalInput > 0 && isFacingLeft){
            isFacingLeft = false;
            isFacingRight = true;
            Flip();
        } else if(horizontalInput < 0 && isFacingRight){
            isFacingLeft = true;
            isFacingRight = false;
            Flip();
        }
        rb.velocity = new Vector2(horizontalInput, rb.velocity.y);
    }

    void Update(){
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        if(fJumpPressedRemember > 0){
            fJumpPressedRemember -= Time.deltaTime;
            if(fJumpPressedRemember == 0){
                fJumpPressedRemember = 0;
            }
        }
  
        fGroundedRemember -= Time.deltaTime;
        if(grounded){
            fGroundedRemember = fGroundedRememberTime;
        }
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        //Movement
        playerJump();

        //Attack
        instantiateAttack();
    }

    public void Flip(){
        if(isFacingLeft){
            transform.localScale = facingLeft;
        }
        if(!isFacingLeft){
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void instantiateAttack(){
        if(Input.GetKeyDown(KeyCode.Z) && !Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.DownArrow)){
            if(isFacingRight){ //If you attack forward check if youre facing left or right
                meleeAttack = Instantiate(meleeSlashR, transform.position, transform.rotation); //Create slash effect
                meleeAttack.transform.parent = rb.transform;//Make slash effect stick with player
                meleeAttack.transform.position  +=  transform.right * 1.25f; //Make attack in front of player
            }
            if(isFacingLeft){
                meleeAttack = Instantiate(meleeSlashL, transform.position, Quaternion.Inverse(transform.rotation));
                meleeAttack.transform.parent = rb.transform;
                meleeAttack.transform.position  +=  -transform.right * 1.25f;
            }
            

            Destroy(meleeAttack, 0.25f);
        }
        else if(Input.GetKey(KeyCode.X) && Input.GetKeyDown(KeyCode.Z)){
            meleeAttack = Instantiate(meleeSlashUp, transform.position, transform.rotation);
            meleeAttack.transform.parent = rb.transform;
            meleeAttack.transform.position += transform.up * 1.25f;
            Destroy(meleeAttack, 0.25f);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z) && grounded == false){
            meleeAttack = Instantiate(meleeSlashDown, transform.position, transform.rotation);
            meleeAttack.transform.parent = rb.transform;
            meleeAttack.transform.position += -transform.up * 1.25f;
            Destroy(meleeAttack, 0.25f);
        }
    }
    public void playerJump(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            fJumpPressedRemember = fJumpPressedRememberTime;
        }
        if((fJumpPressedRemember > 0) && (fGroundedRemember > 0)){
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + fJumpVelocity);
        }

        if(Input.GetKeyUp(KeyCode.UpArrow)){
            if(rb.velocity.y > 0){
                rb.velocity = new Vector2(rb.velocity.x, fCutJumpHeight * rb.velocity.y);
            }
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }

}
