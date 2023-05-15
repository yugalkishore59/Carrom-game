using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool touchPhase = false;
    [SerializeField] GameObject forceCircle;
    [SerializeField] GameObject arrow;
    [SerializeField] float maxForce = 500f;
    Touch strikerTouch;
    [SerializeField] float curForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(touchPhase && Input.GetMouseButtonUp(0)){
            touchPhase = false;
            //Debug.Log(strikerTouch.position.x);
        }

        if(touchPhase){
            strikerTouch = Input.GetTouch(0);
            onStrikerDragging();
        }
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)){
            touchPhase = true;
        }
    }

    void onStrikerDragging(){
        Vector3 touchPos = Camera.main.ScreenToViewportPoint(strikerTouch.position);
        touchPos.z=0;
        curForce = (transform.position - touchPos).magnitude;
        curForce = Mathf.Clamp(curForce,0f,maxForce);

        arrow.transform.LookAt((transform.position - touchPos).normalized);
    }
}
