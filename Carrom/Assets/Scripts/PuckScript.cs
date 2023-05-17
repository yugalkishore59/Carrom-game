using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] float minSpeed=1f;
    [SerializeField]  GameObject gameManager;
    GameManagerScript gameManagerScript;
    [SerializeField] int score = 1;
    bool isMoving = false;
    [SerializeField] int id = 1; //1 player ie. white, -1 cpu ie. black, 0 queen

    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if(rb.velocity.magnitude < minSpeed){
            rb.velocity = Vector2.zero;
            if(isMoving){
                gameManagerScript.movingPucks -= 1;
                isMoving = false;
            } 
        }

        if(!isMoving && rb.velocity.magnitude != 0){
            isMoving = true;
            gameManagerScript.movingPucks += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Hole"){
            rb.velocity = Vector2.zero;
            transform.position = other.transform.position;
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

    IEnumerator DestroyAfter(int s){
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
