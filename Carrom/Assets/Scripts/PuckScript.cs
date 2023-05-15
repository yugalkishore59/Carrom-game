using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] float maxForce=5f;
    [SerializeField] float minSpeed=1f;
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude < minSpeed){
            rb.velocity = Vector2.zero;
        }
    }
    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)){
            float x = Random.Range(-1,1);
            float y = Random.Range(-1,1);
            Vector2 direction = new Vector2(x,y);
            rb.AddForce(direction.normalized * maxForce);
        }
    }
}
