using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUScript : MonoBehaviour
{
    [SerializeField] List<GameObject> holes = new List<GameObject>();
    [SerializeField] GameObject striker;
    [SerializeField] Slider slider;
    Rigidbody2D strikerRb;
    [SerializeField] GameObject gameManager;
    GameManagerScript gameManagerScript;

    [SerializeField] float minForce = 500;
    [SerializeField] float maxForce = 1200;
    
    public bool isCPUTurn = false;

    private void Awake() {
        strikerRb = striker.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    private void Update(){
        if(isCPUTurn){
            ScanShots();
            isCPUTurn = false;
        }        
    }

    void ScanShots(){ //scanning black pucks which are in line of sight between striker and hole
        transform.position = new Vector3(-4.5f,5.5f,0);
        int flag = 0; //to check if any black puck found (or queen)

        while(transform.position.x<=4.5f){ //for every initial position of striker
            for(int i=0; i<holes.Count;i++){
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, holes[i].transform.position - transform.position);
                Debug.DrawRay(transform.position, holes[i].transform.position - transform.position,Color.green);

                foreach(RaycastHit2D other in hit){
                    if(other.collider.gameObject.tag == "Black Puck"){
                    slider.value = transform.position.x / 4.5f; //setting slider value for striker position
                    striker.transform.position = transform.position; //just to confirm the striker position
                    Vector2 direction=holes[i].transform.position - transform.position;
                    float force = Random.Range(minForce,maxForce);
                    strikerRb.AddForce(direction.normalized * force); //adding force to striker in direction for puck
                    flag =1;
                    break;
                    }
                }
                if(flag==1){
                break;
            }
            }
            if(flag==1){
                break;
            }
            transform.position += new Vector3(0.5f,0,0); //next position
        }
        if(flag==0){ //if no black puck or queen found
            strikerRb.AddForce(Vector2.down.normalized * minForce);
        }
    }
}
