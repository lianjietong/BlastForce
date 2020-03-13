using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        // transform.forward
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter(Collider hitInfo){
        Debug.Log(hitInfo.name);

        Destroy(gameObject);
    }

}
