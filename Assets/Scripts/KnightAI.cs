using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement;  public class KnightAI : MonoBehaviour {      [Header("Speed of character")]     public float speed;     public float distance;      [Header("Detections")]     public Transform GroundDetection;     public Transform WallDetection;     public Transform Player;      //private variables     private Vector2 direction = new Vector2(0, 1);     private bool moveRight = true;     private bool Move = true;     private Vector3 newXPosition;     private bool CheckGround = true;     private bool CheckWall = true;     private bool delay = false;     private float delayFloat;     public GameObject Collectible;        [Header("Damage Options")]     public float AttackRangeMelee;     public int damage;     public float DelayAttack;     public float minDistance;     private float lastAttack;      [Header("Enemy Attributes")]     public float Health;      private void Start()
    {
        Move = true;         CheckWall = true;         CheckGround = true; 
    }

    // Update is called once per frame
    public void Update()     {


        //moves enemy along platform
        if (Move)         {             NormalMove();         }
        if (delay)         {             StartCoroutine("delayNormal");             Move = true;             CheckWall = true;             CheckGround = true;         }

        //uses raycast to detect when platform edge is beside the enemy.
        if (CheckGround)         {             RaycastHit2D groundInfo = Physics2D.Raycast(GroundDetection.position, Vector2.down, distance);              if (groundInfo.collider == false)             {                 if (moveRight == true)                 {                     transform.eulerAngles = new Vector3(0, -180, 0);                     moveRight = false;                 }                 else                 {                     transform.eulerAngles = new Vector3(0, 0, 0);                     moveRight = true;                 }             }          }         if (CheckWall)         {             RaycastHit2D Wall = Physics2D.Raycast(WallDetection.position, direction, distance);              if (Wall == true)             {                 if (Wall.collider.CompareTag("Wall"))                 {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();                     speed *= -1;                     direction *= -1;                     moveRight = true;                 }                 if (Wall.collider.CompareTag("Player"))                 {
                    //makes the enemy detect the wall with tag wall using raycast
                    Rotate();                     speed *= -1;                     direction *= -1;                     moveRight = true;                 }             }         }

        //Attacking the Player melee
        float distancefromPlayer = Vector2.Distance(transform.position, Player.position);           if (distancefromPlayer < AttackRangeMelee)         {             if (Time.time > lastAttack + DelayAttack)             {                 Player.SendMessage("TakeDamage", damage);                 lastAttack = Time.time;             }          }          if (distancefromPlayer < minDistance)         {
            //sets movement and wall and ground checks to false
            Move = false;             CheckWall = false;             CheckGround = false;

            //calls function to move enemy towards the player
            MovetoPlayer();
            //delay = false;             //delayFloat = Time.deltaTime + 3; 
        } 
           else          {             delay = true;                     } 
    }      //changes rotation of enemy when colliding with wall     void Rotate()     {         Vector3 scale = transform.localScale;         scale.x *= -1;         transform.localScale = scale;     }      void MovetoPlayer()     {
        //moves the enemy to the player when in range
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.position.x, transform.position.y), speed * Time.deltaTime);     }     void NormalMove()     {         //the normal movement for the enemy along the platform         transform.Translate(Vector2.right * speed * Time.deltaTime);     }     public void TakeDamage(int damage)     {         Health =- damage;         print("ser");         if (Health <= 0)         {             Destroy(gameObject);             Instantiate(Collectible, transform.position, transform.rotation);         }
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }       IEnumerator delayNormal(){        
            yield return new WaitForSeconds(3);      }   } 


