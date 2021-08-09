using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    public Transform aimTarget;
    public Transform ball;
    public float speed;
    public float targetSpeed = 7f;
    //float force = 13f;

    bool hitting;

    Animator animator;

    Vector3 aimTargetInitialPosition;

    ShotManager shotManager;
    Shot currentShot;

    [SerializeField] Transform serveRight;
    [SerializeField] Transform serveLeft;

    bool servedRight = true;

    public TimeManager timeManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
        ball.GetComponent<Ball>().Partida2Player = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Input utilizado para el aimTarget
        float h = Input.GetAxisRaw("Horizontal");

        //Accion que mueve nuestro player 
        MovePlayer2();

        //Nos permite manipular el aimtarget y los diferentes tiros que hacemos 
        //R Flat
        if (Input.GetKeyDown(KeyCode.I))
        {
            hitting = true;
            currentShot = shotManager.Flat;
            timeManager.DoSlowMotion();

        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            hitting = false;
        }

        //Space TopSpin
        if (Input.GetKeyDown(KeyCode.O))
        {
            hitting = true;
            currentShot = shotManager.topSpin;
            timeManager.DoSlowMotion();
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            hitting = false;
        }

        //T flatServe
        if (Input.GetKeyDown(KeyCode.K))
        {
            hitting = true;
            currentShot = shotManager.flatServe;
            GetComponent<BoxCollider>().enabled = false;
            animator.Play("Serve-prepare");
        }

        //Y kickServe
        if (Input.GetKeyDown(KeyCode.L))
        {
            hitting = true;
            currentShot = shotManager.kickServe;
            animator.Play("Serve-prepare");
        }

        // T e Y service ejecucion 
        if (Input.GetKeyUp(KeyCode.K) || Input.GetKeyUp(KeyCode.L))
        {
            hitting = false;
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.2f, 1, 0);
            Vector3 dir = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            animator.Play("Serve");
            ball.GetComponent<Ball>().hitter = "Player2";
            ball.GetComponent<Ball>().playing = true;
        }


        
        //Mueve el aimtarget
        if (hitting)
        {
            aimTarget.Translate(new Vector3(-h, 0, 0) * targetSpeed * Time.deltaTime);
        }

    }

    void MovePlayer2()
    {
        //Encargado del movimiento del personaje y de cambiar entre movernos o mover el aimtarget

        if (Input.GetKey(KeyCode.UpArrow) && !hitting)
        {
            transform.Translate(new Vector3(0, 0, Vector3.forward.z) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !hitting)
        {
            transform.Translate(new Vector3(0, 0, -Vector3.forward.z) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !hitting)
        {
            transform.Translate(new Vector3(Vector3.left.x, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !hitting)
        {
            transform.Translate(new Vector3(Vector3.right.x, 0, 0) * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //Calcula la direccion en la que se encuentra el aimtarget y envia la pelota hacia ese lugar
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            /*Codigo utilizado para calcular la direccion en la que se encuentra la pelota y asi 
            saber cual animacion vamosa a ejecutar*/
            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x >= 0)
            {
                animator.Play("ForeHand");
            }
            else
            {
                animator.Play("BackHand");
            }

            ball.GetComponent<Ball>().hitter = "Player2";

            aimTarget.position = aimTargetInitialPosition; //Resetea la posicion del target
        }
    }

    public void Reset()
    {
        //Resetea la posicion para poder sacar

        if (servedRight)
        {
            transform.position = serveLeft.position;
        }
        else
        {
            transform.position = serveRight.position;
        }

        servedRight = !servedRight;
    }
}
