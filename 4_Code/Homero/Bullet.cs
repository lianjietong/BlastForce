using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public float accelerationPower = 1200f;
    
    void Update(){
        float thrust = accelerationPower * Time.deltaTime;
        thrust = Mathf.Abs(thrust);
        rb.AddForce(transform.right * thrust);
    }

    void OnTriggerEnter(Collider hitInfo){
        //Debug.Log(hitInfo.name);

        if(hitInfo.name != "Player"){   //if bullet hits anything other than player it will be destroyed
            Destroy(gameObject);
        }
    }


}
