using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float speed = 10.0f;
    public float FlySpeed = 12.0f;
    public float JumpForce = 10.0f;
    public float flyingTime = 1f;

    //other variables
    //private Vector2 moveDirection = Vector2.zero;
    private Vector2 fly;
    private Vector2 jump;
    private Rigidbody2D controller;
    private bool canJump;
    private Vector3 flyingposition;
    private bool canFly;
    private float flyingTimer;
    private int pickedUp;
    private float moveDirection;
    private bool facingRight;

    public Animator PlayerTakesDamage;

    [Header("Enemy")]
    public Transform Knight;

    [Header("Gem Activations")]
    public int JumpActivates;
    public int FlyActivates;
    public int BreathAttackActivates;
    private bool Stage1;
    private bool Stage2activate;
    private bool Stage3activate;
    private int Stage2 = 3;
    private int Stage3 = 10;

    [Header("Player Attributes")]
    public float Health;

    [Header("Melee Damage Options")]
    public float AttackRangeMelee;
    public int MeleeDamage;
    public int BreathDamage;
    public float DelayAttack;
    private float lastAttackMelee;
    public float AttackRangeBreath;
    private float LastAttackBreath;
    public Animator anim;

    public Text CollectibleNumber;
    public GameObject Collectible;
    public Transform Chest;
    public float Range;



    [Header("Checks")]
    public Transform GroundCheck;

    void Start()
    {
        //gets the controller on start
        controller = GetComponent<Rigidbody2D>();


        facingRight = true;

        // Gravity of Game object
        gameObject.transform.position = new Vector3(-30, 1, 0);

        //Declares Fly as Vector2
        fly = new Vector2(0, 19.8f * FlySpeed);

        //Declares jump force
        jump = Vector2.up * JumpForce;
        canJump = false;
        canFly = false;

        Stage1 = true;
        Stage2activate = false;
        Stage3activate = false;

        CollectiblesCollected();
    }

    void Fly()
    {
        //flys character

        canFly = true;
        flyingTimer = 0;
        controller.gravityScale = 0.0f;
    }

    void Jump()
    {
        //Jumps character
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500f));
        GetComponent<Rigidbody2D>().AddForce(jump);

    }

    void FixedUpdate()
    {
        moveDirection = Input.GetAxis("Horizontal");
        controller.velocity = new Vector2(moveDirection * speed, controller.velocity.y);
        anim.SetFloat("Speed", controller.velocity.x);

    }

    void CollectiblesCollected()
    {

        // CollectibleNumber.text = "Collectibles: " + pickedUp;
    }

    public void Update()
    {
        CollectiblesCollected();
        //checks if the player is on grounds
        if (Physics2D.OverlapCircle(GroundCheck.position, 0.2f))
        {


            //changes direction character faces to last key
            if (moveDirection > 0 && !facingRight || moveDirection < 0 && facingRight)
            {
                facingRight = !facingRight;
                Rotate();
            }
        }

        if (pickedUp >= JumpActivates)
        {


            //Sets jump on space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canJump)
                {
                    Jump();
                    controller.gravityScale = 5.0f;
                    anim.SetTrigger("Jump");
                }
            }
        }

        //evolution
        //if picked up 5 collectibles move to next sprite
    
        if (pickedUp >= Stage2)
        {
            Stage1 = false;
            Stage2activate = true;

            if (Stage2activate)
            {
                flyingTime = 2f;
                speed = 6f;

                if (Input.GetKeyDown(KeyCode.M) && !canFly)
                {
                    Fly();
                    anim.SetBool("Fly", true);

                }

                //returns if M is released flying stops
                if (Input.GetKeyUp(KeyCode.M))
                {
                    canFly = false;
                    anim.SetBool("Fly", false);
                    controller.gravityScale = 5.0f;
                }

                if (canFly)
                {
                    //  flyingposition.x = moveDirection.x *speed * Time.deltaTime;
                    flyingposition = transform.localPosition;
                    flyingposition.y += 2;

                    transform.position = Vector3.Lerp(transform.position, flyingposition, Time.deltaTime * 2);
                    flyingTimer += Time.deltaTime;

                    if (flyingTimer >= flyingTime)
                    {
                        canFly = false;
                        controller.gravityScale = 5.0f;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    anim.SetTrigger("Melee");
                    MeleeAttack();
                }
            }
            //first stage can fly for 3 seconds
            //movement is slower
        }

        //if picked up 10 collectibles 
        
        if (pickedUp >= Stage3)
        {
            Stage1 = false;
            Stage2activate = false;
            Stage3activate = true;

            if(Stage3activate)
            {
                flyingTime = 5f;
                speed = 10f;

                if (Input.GetKeyDown(KeyCode.X))
                {
                    anim.SetTrigger("Breath");
                    BreathAttack();
                }
            }
        
            //movement is fast
            //can fly for 10 seconds
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ChestOpen();
        }



    

      
     

    }

    public void ChestOpen()
    {
        float distancefromPlayerChest = Vector2.Distance(transform.position, Chest.position);
        if (gameObject.tag == "Chest")
        {
            if (distancefromPlayerChest < Range)
            {
         
                    Instantiate(Collectible, transform.position, transform.rotation);
                    print("ev");


            }
        }
    }

    //Checks if player is grounded as to jump
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
       {
            canJump = true;
           // canFly = true;
           
       }
   }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
           // canFly = false;
        }
    }
  

    //Adds collectible pick up
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Collectible")
        {
            collider.gameObject.SetActive(false);
            pickedUp += 1;
        }
    }

    
 
    public void TakeDamage(int damage)
    {
        Health -= damage;
        PlayerTakesDamage.Play("PlayerTakesDamage");
        if(Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void MeleeAttack()
    {
        float distancefromPlayerMelee = Vector2.Distance(transform.position, Knight.position);

        if(GameObject.FindGameObjectWithTag("Enemy"))
        {
            if (distancefromPlayerMelee < AttackRangeMelee)
            {

                Knight.SendMessage("TakeDamage", MeleeDamage);
                print("melleee");
                lastAttackMelee = Time.time;
               
            }
        }
       
    }

    public void BreathAttack()
    {
        float distancefromPlayerBreath = Vector2.Distance(transform.position, Knight.position);

        if (GameObject.FindGameObjectWithTag("Enemy"))      
        {
            if(distancefromPlayerBreath < AttackRangeBreath)
            {
                Knight.SendMessage("TakeDamage", BreathDamage);
                LastAttackBreath = Time.time;
            }
        }
    }
    void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
 
} 

