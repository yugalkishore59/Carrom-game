using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrikerScript : MonoBehaviour
{
    bool isTouching = false;
    [SerializeField] GameObject forceCircle;
    [SerializeField] GameObject sliderObject;
    [SerializeField] GameObject arrow;
    [SerializeField] float maxForce = 500f;
    [SerializeField] float curForce;
    [SerializeField] float forceFactor = 5;
    [SerializeField] float minSpeed=1f;
    Touch touch;
    Vector2 strikerPosition;
    Vector2 direction;
    Rigidbody2D rb;
    Slider slider;
    int mode = 1; //1 player, -1 cpu, 0 none
    GameManagerScript gameManagerScript;
    [SerializeField]  GameObject gameManager;
    bool isMoving = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        slider = sliderObject.GetComponent<Slider>();
    }

    void Update(){

        DetectTouch();
        if(mode == 1 || mode == -1){ //two player mode -- will be changed
            SlideControls();
        }

        if(isTouching){
            StrikerDragging();
        }
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
            gameManagerScript.strikerThrown = true;
        }

    }

    void DetectTouch(){
        if (Input.touchCount > 0 && !isMoving ){  //bug here -- as soon as striker stops, it can be dragged to throw in middle of board
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began){
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Collider2D collider = Physics2D.OverlapPoint(touchPosition);
                if (collider != null && collider.gameObject == gameObject)
                {
                    mode = 0;
                    arrow.SetActive(true);
                    forceCircle.SetActive(true);
                    isTouching = true;
                }
            }else if(touch.phase == TouchPhase.Ended){
                isTouching = false;
                arrow.SetActive(false);
                forceCircle.SetActive(false);
                ThrowStriker();
                //gameManagerScript.strikerThrown = true;
            }
        }
    }

    void StrikerDragging(){
        strikerPosition = new Vector2(transform.position.x,transform.position.y);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        curForce = (strikerPosition - touchPosition).magnitude;
        curForce = Mathf.Clamp(curForce,0f,maxForce);

        direction = touchPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
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

    public void ChangeSide(){
        if(gameManagerScript.turn == 1){ //player's turn
            transform.position = new Vector3(0,-5.5f,0);
            sliderObject.transform.localPosition = new Vector3(0,-700,0);
            mode = 1;
        }
        else{ //cpu's turn
            transform.position = new Vector3(0,5.5f,0);
            sliderObject.transform.localPosition = new Vector3(0,700,0);
            mode = -1;
        }
    }
}
