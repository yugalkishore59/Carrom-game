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

    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    void Update()
    {
        if(rb.velocity.magnitude < minSpeed){
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Hole"){
            rb.velocity = Vector2.zero;
            transform.position = other.transform.position;
            if(gameManagerScript.turn == 1){ //player's turn
                gameManagerScript.UpdatePlayerScore(score);
            }else{
                gameManagerScript.UpdateCPUScore(score);
            }
            StartCoroutine(DestroyAfter(1));
        }
    }

    IEnumerator DestroyAfter(int s){
        yield return new WaitForSeconds(s);
        gameObject.SetActive(false);
    }
}
