using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Ball : MonoBehaviour
{
    //Variables para el reinicio de la pelota
    Vector3 initialPos;
    public string hitter;

    //Variables para la puntuacion
    public int playerScore;
    public int botScore;
    public int player2Score;

    [SerializeField] Text playerScoreUI;
    [SerializeField] Text botScoreUI;
    [SerializeField] Text player2ScoreUI;


    //Variable que me permite saber si la pelota esta o no en juego
    public bool playing = true;

    //Booleano encargado de sasber el tipo de partida
    public bool Partida2Player = false;

    private void Start()
    {
        //Reinicio la posicion e igualo todos los puntajes a cero para poder hacer un UI
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        player2Score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            //Baja la velocidad de la pelota a 0 al entrar en contacto con alguna pared
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Llama la funcion de reset para poder ejecutar el saque
            GameObject.Find("Player").GetComponent<Player>().Reset();
            if (Partida2Player == true)
            {
                GameObject.Find("Player2").GetComponent<Player2Script>().Reset();
            }
            
            if (playing)
            {
                if (hitter == "Player") //Ultimo hitter player
                {
                    playerScore++;
                }
                if (hitter == "Bot") //Ultimo hitter Bot
                {
                    botScore++;
                }
                if (hitter == "Player2") //Ultimo hitter Player2
                {
                    player2Score++;
                }
                playing = false; 
            }
        }

        else if (collision.transform.CompareTag("Out"))
        {
            //Bajamos la velocidad de la pelota a 0
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Reiniciamos la posicion
            transform.position = initialPos;

            //Preparamos al jugador para que ejecute el saque
            GameObject.Find("Player").GetComponent<Player>().Reset();
            GameObject.Find("Player2").GetComponent<Player2Script>().Reset();


            //Registramos quien fue el ultimo en pegarle para poder realizar la suma de puntaje
            if (playing)
            {
                if (hitter == "Player")
                {
                    playerScore++;
                }
                if (hitter == "Bot")
                {
                    botScore++;
                }
                if (hitter == "Player2")
                {
                    player2Score++;
                }
                playing = false;
                updateScore();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Revisamos quien fue el ultimo en pegarle y si la pelota esta o no en juego para realizar el puntaje
        if (other.CompareTag("Out") && playing)
        {
            if (hitter == "Player")
            {
                playerScore++;

            }
            if (hitter == "Bot")
            {
                botScore++;
            }
            if (hitter == "Player2")
            {
                player2Score++;
            }
            playing = false;
            updateScore();
        }
    }

    void updateScore()
    {
        if (Partida2Player == false) //Variable que modifica el player2
        {
            playerScoreUI.text = "Player: " + playerScore;
            botScoreUI.text = "Bot: " + botScore;
        }
        else 
        {
            playerScoreUI.text = "Player: " + playerScore;
            player2ScoreUI.text = "Player2 " + player2Score;
        }
    }

    void Update()
    {
        WinCondition();
    }

    void WinCondition() //Checkea si hay algun ganador
    {
        if (playerScore == 4) //Si el player llega a 4 pts 
        {
            if (Partida2Player == false) //Y no es una partida de 1P VS 2P
            {
                SceneManager.LoadScene("Player1Win"); //Gana el jugador
            }
            else
            {
                SceneManager.LoadScene("Player1Win"); 
            }

        }
        if (botScore == 4) //Si el bot llega a 4 puntos
        {
            SceneManager.LoadScene("IAWin"); //Gana la ia
        }
        if (player2Score == 4) // Por ultimo, si el player2 llega a 4 puntos
        {
            SceneManager.LoadScene("Player2WinPVP"); //Gana el player2
        }
    }

    /*
    IEnumerator Winner()
    {
        if (playerScore == 4)
        {
            Debug.Log("PlayerWin");
        }
        else if (botScore == 4)
        {
            Debug.Log("BotWin");
        }
        yield return new WaitForSeconds(5F);
        Winner();
    }
    */
}
