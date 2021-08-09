using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //public Ball ball;
    void Start()
    {
        Debug.Log("XD");
        StartCoroutine(Winner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Winner()
    {
        if (GetComponent<Ball>().playerScore==4)
        {
            Debug.Log("PlayerWin");
            //SceneManager.LoadScene();
        }
        else if (GetComponent<Ball>().botScore == 4)
        {
            Debug.Log("BotWin");
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(Winner());
    }

}
