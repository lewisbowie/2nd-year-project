using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnightAI : MonoBehaviour
{
    [SerializeField]
    private bool chase;
    [Header("Speed of character")]
    public float speed;
    public float distance;

    [Header("Detections")]
    public Transform GroundDetection;
    public Transform WallDetection;
    public Transform Player;

    //private variables
    private Vector2 direction = new Vector2(0, 1);
    private bool moveRight = true;
    [SerializeField]
    private bool Move = true;
    private Vector3 newXPosition;
    private bool CheckGround = true;
    private bool CheckWall = true;
    private bool delay = false;
    private float delayFloat;
    public GameObject Collectible;



    [Header("Damage Options")]
    public float AttackRangeMelee;
    public int damage;
    public float DelayAttack;
    public float minDistance;
    private float lastAttack;

    [Header("Enemy Attributes")]
    public float Health;

    private void Start()
    {
        Move = true;
        CheckWall = true;
        CheckGround = true;
        chase = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (chase)
        {
            Move = false;
            MovetoPlayer();
           
        }
        if (!chase) 
        {
            Move = true;

        }
       

        //moves enemy along platform
        if (Move)
        {
            NormalMove();
        }
        if (delay)
        {
            StartCoroutine("delayNormal");
           // Move = true;
            CheckWall = true;
            CheckGround = true;
        }

        //uses raycast to detect when platform edge is beside the enemy.
        if (CheckGround)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, /*Vector2.down*/ -transform.up, distance);

            if (groundInfo.collider == false)
            {
                if (moveRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    moveRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    moveRight = true;
                }
            }

        }

        if (CheckWall && Move)
        {
            RaycastHit2D Wall = Physics2D.Raycast(WallDetection.position, direction, distance);

            if (Wall == true)
            {
                if (Wall.collider.CompareTag("Wall"))
                {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();
                    speed *= -1;
                    direction *= -1;
                    moveRight = true;
                }
                if (Wall.collider.CompareTag("Player"))
                {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();
                    speed *= -1;
                    direction *= -1;
                    moveRight = true;
                }
            }
        }

        //Attacking the Player melee
        float distancefromPlayer = Vector2.Distance(transform.position, Player.position);


        if (distancefromPlayer < AttackRangeMelee)
        {
            chase = true;
            Move = false;
            // MovetoPlayer();
            if (Time.time > lastAttack + DelayAttack)
            {
                Player.SendMessage("TakeDamage", damage);
                lastAttack = Time.time;
             

            }

        }
        else
        {
            chase = false;
            Move = true;
        }

       if (distancefromPlayer < minDistance)
        {
            //sets movement and wall and ground checks to false
            Move = false;
            CheckWall = false;
            CheckGround = false;

            //calls function to move enemy towards the player
            //MovetoPlayer();
            //delay = false;
            //delayFloat = Time.deltaTime + 3;

        }

           else 
        {
            delay = true;
           
        }

    }

    //changes rotation of enemy when colliding with wall
    void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void FlipMovement()
    {
        speed *= -1;
    }

    void MovetoPlayer()
    {
        //moves the enemy to the player when in range
       
        

        float horizontalDifference = this.transform.position.x - Player.transform.position.x;
        float verticalDifference = this.transform.position.y - Player.transform.position.y;

        PlayerController PC = Player.gameObject.GetComponent<PlayerController>();
      

            if (((speed > 0f) && (horizontalDifference > 0f)) || ((speed < 0f) && (horizontalDifference < 0f)))
            {

                //Rotate();
                //PlayerController pC = Player.gameObject.GetComponent<PlayerController>();
                
            if ((moveRight && (speed < 0f)) || (!moveRight && (speed > 0f)))
            {
                FlipMovement();

                
            }


        }//transform.LookAt(Player.position, Vector3.back * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, transform.position.y), Mathf.Abs(speed * Time.deltaTime));



        // transform.LookAt(Player.transform.eulerAngles = new Vector2( 0, 0));
        // print(Vector2.MoveTowards(transform.position, new Vector2(Player.position.x, transform.position.y), speed * Time.deltaTime));
    }
    void NormalMove()
    {
        //the normal movement for the enemy along the platform
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    IEnumerator Delay()
    {

        yield return new WaitForSeconds(1);

    }
    public void TakeDamage(int damage)
    {
        Health =- damage;
        
        if (Health <= 0)
        {
            StartCoroutine("Delay");
            Destroy(gameObject);
            Instantiate(Collectible, transform.position, transform.rotation);
        }
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    IEnumerator delayNormal(){
       
            yield return new WaitForSeconds(3);

    }
 
} 


