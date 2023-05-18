using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerScript : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] GameObject forceCircle;
    [SerializeField] GameObject sliderObject;
    [SerializeField] GameObject arrow;
    [SerializeField]  GameObject gameManager;
    GameManagerScript gameManagerScript;
    [SerializeField] GameObject CPU;
    CPUScript cPUScript;
    Slider slider;

    [SerializeField] float maxForce = 500f; //max allowed force
    [SerializeField] float curForce;
    [SerializeField] float forceFactor = 5;
    [SerializeField] float minSpeed=1f; //min stopping speed
    bool isMoving = false;
    int mode = 1; //1 player, -1 cpu, 0 none

    Touch touch;
    bool isTouching = false; //is touched by finger ie. dragging

    Vector2 strikerPosition;
    Vector2 direction; //force direction
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        slider = sliderObject.GetComponent<Slider>();
        cPUScript = CPU.GetComponentInParent<CPUScript>();
    }

    void Update(){

        DetectTouch();
        if(mode == 1 || mode == -1){ //if it can be controlled by slider
            SlideControls();
        }

        if(isTouching){
            StrikerDragging();
        }

        if(rb.velocity.magnitude < minSpeed){ //stop if speed in less than minSpeed
            rb.velocity = Vector2.zero;
            if(isMoving){
                gameManagerScript.movingPucks -= 1; //Update movingPucks in gameManager
                isMoving = false;
            }
        }

        if(!isMoving && rb.velocity.magnitude != 0){ //if it is moving then update movingPucks in gameManager
            mode = 0; //making sure for cpu ai so that ai doesn't shoot while striker is moving
            isMoving = true;
            gameManagerScript.movingPucks += 1;
            gameManagerScript.strikerThrown = true;
        }

    }

    void DetectTouch(){
        if (Input.touchCount > 0 && !isMoving ){
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D collider = Physics2D.OverlapPoint(touchPosition); //checking overlapping collider on touch position
                if (collider != null && collider.gameObject == gameObject) //checking if striker is touched by compairing it with overlapping collider
                {
                    mode = 0; //ie. now it cannot be controlled by slider or cpu
                    arrow.SetActive(true);
                    forceCircle.SetActive(true);
                    isTouching = true;
                }
            }else if(touch.phase == TouchPhase.Ended){
                isTouching = false;
                arrow.SetActive(false);
                forceCircle.SetActive(false);
                ThrowStriker(); //throwing striker
            }
        }
    }

    void StrikerDragging(){ //while striker is being charged
        strikerPosition = new Vector2(transform.position.x,transform.position.y);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        curForce = (strikerPosition - touchPosition).magnitude; //calculating force with drag distance
        curForce = Mathf.Clamp(curForce,0f,maxForce);

        direction = touchPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //getting angle by tan-1
        arrow.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward); //pointing arrow at angle (opposite)

        //setting scale according to current force ie. drag distance
        float circleScale = 4.8f + (curForce * 7);
        float arrowScale = 4.5f + (curForce * 2);
        forceCircle.transform.localScale = new Vector3(1,1,1)* circleScale;
        arrow.transform.localScale = new Vector3(1,1,1)* arrowScale;
    }

    void ThrowStriker(){
        rb.AddForce(-direction.normalized * curForce * forceFactor);
        curForce = 0;
    }

    void SlideControls(){
        transform.position = new Vector3 (4.5f * slider.value,transform.position.y,0);
    }

    public void ChangeSide(){ //setting sriker initial position according to "turn"
        if(gameManagerScript.turn == 1){ //player's turn
            transform.position = new Vector3(0,-5.5f,0);
            sliderObject.transform.localPosition = new Vector3(0,-700,0);
            mode = 1;
        }
        else{ //cpu's turn
            transform.position = new Vector3(0,5.5f,0);
            sliderObject.transform.localPosition = new Vector3(0,700,0);
            mode = -1;
            cPUScript.isCPUTurn = true;
        }
    }
}
