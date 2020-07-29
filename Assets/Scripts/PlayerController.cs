using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D theRB;
    public float runSpeed;
    private float defaultRunSpeed;
    private float defaultGravity;
    public float dashSpeed;
    public float dashGravityScale;
    public float jumpForce;
    public float swingForce;
    public Animator theSwordAnimator;  //animator of sword (swing anim is in that animator)
    private bool touchingGround;
    public bool shouldRun;

    public bool inJump, inDash, inSwing, inNextDash;
    public bool canNextSwing, canNextDash;

    public ParticleSystem swordParticle;

    private bool shouldControlPlayer;

    public bool playerShouldDie;
    public bool killedEnemy;

    public bool zoomOut = false;

    public Material swordTrailMaterial;
    public Material swordBloom;

    public GameObject playerBody;
    public Sprite whitePlayerSkin, blackPlayerSkin;
    //private Sprite defaultPlayerSkin;
    //public bool whiteSkin, blackSkin = false;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        shouldRun = true;
        defaultRunSpeed = runSpeed;
        swordParticle.enableEmission = false;

        defaultGravity = theRB.gravityScale;
        dashGravityScale = defaultGravity * 0.6f;
    }

   
    void Update()
    {
        // SKIN PICKING
        Sprite defaultPlayerSkin = blackPlayerSkin;
        if (PlayerPrefs.GetString("SkinPicked") == "white skin") playerBody.GetComponent<SpriteRenderer>().sprite = whitePlayerSkin;
        
        if (PlayerPrefs.GetString("SkinPicked") == "black skin") playerBody.GetComponent<SpriteRenderer>().sprite = blackPlayerSkin;

        if (PlayerPrefs.GetString("SkinPicked") == null) playerBody.GetComponent<SpriteRenderer>().sprite = blackPlayerSkin;





        if (!inDash)
        {
            runSpeed = defaultRunSpeed + (defaultRunSpeed * GameManager.instance.scorePoints * 0.001f);
        }
        
        if(canNextDash)
        {
            zoomOut = false;
        }

        if(!killedEnemy && canNextDash)
        {
            zoomOut = true;
        }

        
        if (shouldRun)
        {
            theRB.velocity = new Vector2(runSpeed, theRB.velocity.y);
        }

        if(Input.GetMouseButtonDown(0))    // NOT CONTROLLING PLAYER ON UPPER PART OF SCREEN OR IG GAME IS PAUSED
        {
            CheckToControlPlayer();
        }
        


        // JUMP
        if (Input.GetKeyDown(KeyCode.Mouse0) && touchingGround && !inJump && shouldControlPlayer)
        {
            Jump();
        }

        // SWING SWORD
        if(inDash && !inSwing && !touchingGround && Input.GetKeyDown(KeyCode.Mouse0) && shouldControlPlayer)
        {
            Swing();
            Debug.Log("SWING");
        }


        // DASH
        if(inJump && !touchingGround && !inDash && Input.GetKeyDown(KeyCode.Mouse0) && shouldControlPlayer)
        {
            Dash();
        }

        

        // NEXT SWING
        if(inNextDash && Input.GetKeyDown(KeyCode.Mouse0) && !touchingGround && killedEnemy && canNextSwing && shouldControlPlayer)
        {
            NextSwing();
            Debug.Log("NEXT SWING");
        }

        // NEXT DASH
        if (Input.GetKeyDown(KeyCode.Mouse0) && !touchingGround && killedEnemy && canNextDash && shouldControlPlayer)
        {
            NextDash();
            Debug.Log("NEXT DASH");
            inNextDash = true;
        }

    }

    private void FixedUpdate()
    {
        dashSpeed = runSpeed * 2;
    }


    // METHODS
    public void Jump() // jump method
    {
        theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        inJump = true;
        zoomOut = true;  // set bool for zooming camera
    }

    public void Dash()  // dash method
    {
        runSpeed = dashSpeed;
        theRB.gravityScale = dashGravityScale;  // setting gravity when dashing
        inDash = true;

    }

    public void Swing()  // swing method
    {
        theRB.velocity = new Vector2(theRB.velocity.x, 0); //

        theRB.AddForce(Vector2.down * -swingForce, ForceMode2D.Impulse);
        runSpeed *= .8f;
        theSwordAnimator.SetTrigger("SwordUppercut");
        inSwing = true;
        swordParticle.enableEmission = true;

        canNextDash = true;

    }

    public void NextDash()
    {
        runSpeed = dashSpeed;

        theRB.velocity = new Vector2(theRB.velocity.x, 0); //

        theRB.AddForce(Vector2.up * swingForce, ForceMode2D.Impulse); 
        theRB.gravityScale = dashGravityScale;  // setting gravity when dashing
        
        canNextDash = false;
        canNextSwing = true;

        zoomOut = true;
    }

    public void NextSwing()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, 0); //

        theRB.AddForce(Vector2.down * -swingForce, ForceMode2D.Impulse);
        theSwordAnimator.SetTrigger("SwordUppercut");
        runSpeed *= .5f;

        canNextSwing = false;
        canNextDash = true;

        

        killedEnemy = false;
    }

    private void CheckToControlPlayer()
    {
        if (Input.mousePosition.y < Screen.height * 4 / 5)
        {
            shouldControlPlayer = true;
            //Debug.Log("CONTROL PLAYER");
        }
        if (Input.mousePosition.y > Screen.height * 4 / 5)
        {
            shouldControlPlayer = false;
            //Debug.Log("NOT CONTROL PLAYER");
        }

        if (GameManager.instance.gamePaused)
        {
            shouldControlPlayer = false;
        }
    }

    




    // ON ENTER...
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Platform"))   // touch with ground
        {
            touchingGround = true;
            inJump = false;
            inDash = false;
            inSwing = false;
            swordParticle.enableEmission = false;
            inNextDash = false;

            canNextDash = false;

            zoomOut = false; // set bool for zooming camera

            
            // DEFAULTING VALUES
            runSpeed = defaultRunSpeed + (defaultRunSpeed * GameManager.instance.scorePoints * 0.001f);
            theRB.gravityScale = defaultGravity;;
            killedEnemy = false;


        }

        if(playerShouldDie)
        {
            if (other.CompareTag("Bullet") || other.CompareTag("Enemy"))   // PLAYER DIES
            {
                GameManager.instance.gamePaused = true;
                GameManager.instance.youDieText.gameObject.SetActive(true);
                GameManager.instance.youEarnText.gameObject.SetActive(true);
                GameManager.instance.restartButton.gameObject.SetActive(true);
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            touchingGround = true;
            inDash = false;
            inSwing = false;
            
        }

        

    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            touchingGround = false;
        }

    }

    
}
