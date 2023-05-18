using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]  GameObject gameManager;
    GameManagerScript gameManagerScript;

    [SerializeField] float minSpeed=1f; //minimun stopping speed
    [SerializeField] int score = 1;
    [SerializeField] int id = 1; //1 player ie. white, -1 cpu ie. black, 0 queen
    bool isMoving = false;

    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if(rb.velocity.magnitude < minSpeed){ //stop if speed in less than minSpeed
            rb.velocity = Vector2.zero;
            if(isMoving){
                gameManagerScript.movingPucks -= 1; //Update movingPucks in gameManager
                isMoving = false;
            } 
        }

        if(!isMoving && rb.velocity.magnitude != 0){ //if it is moving then update movingPucks in gameManager
            isMoving = true;
            gameManagerScript.movingPucks += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) { //updating score if potted
        if(other.tag == "Hole"){
            rb.velocity = Vector2.zero;
            transform.position = other.transform.position;
            gameObject.GetComponent<CircleCollider2D>().enabled= false;
            if(id == 1){ //player's puck ie. white
                gameManagerScript.UpdatePlayerScore(score);
            }else if(id == -1){ //cpu's puck ie. black
                gameManagerScript.UpdateCPUScore(score);
            }else{ // queen
                    if(gameManagerScript.turn == 1){ //player potted queen
                        gameManagerScript.UpdatePlayerScore(score);
                    }else{ //cpu potted queen
                        gameManagerScript.UpdateCPUScore(score);
                    }
            }
            StartCoroutine(DestroyAfter(1));
        }
    }

    IEnumerator DestroyAfter(int s){ //destroying and updating variables when potted
        yield return new WaitForSeconds(s);
        if(isMoving){
            gameManagerScript.movingPucks -= 1;
            isMoving = false;
        }
        if(id == gameManagerScript.turn || id == 0){ //for playing again
            gameManagerScript.isPotted = true;
        }
        if(id == 1) gameManagerScript.whitePucksCount -= 1;
        else if(id == -1) gameManagerScript.blackPucksCount -= 1;
        else gameManagerScript.queenPucksCount -= 1;
        gameObject.SetActive(false);
    }
}
