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
    [SerializeField] float minForce = 500;
    [SerializeField] float maxForce = 1200;
    [SerializeField] GameObject gameManager;
    GameManagerScript gameManagerScript;
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

    void ScanShots(){
        transform.position = new Vector3(-4.5f,5.5f,0);
        int flag = 0;
        while(transform.position.x<=4.5f){
            for(int i=0; i<holes.Count;i++){
                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, holes[i].transform.position - transform.position);
                Debug.DrawRay(transform.position, holes[i].transform.position - transform.position,Color.green);
                foreach(RaycastHit2D other in hit){
                    if(other.collider.gameObject.tag == "Black Puck"){
                    slider.value = transform.position.x / 4.5f;
                    striker.transform.position = transform.position;
                    Vector2 direction=holes[i].transform.position - transform.position;
                    float force = Random.Range(minForce,maxForce);
                    strikerRb.AddForce(direction.normalized * force);
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
            transform.position += new Vector3(0.5f,0,0);
        }
        if(flag==0){
            strikerRb.AddForce(Vector2.down.normalized * minForce);
        }
    }
}
