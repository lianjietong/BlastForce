using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public Camera cam;

    public float damage = 10f;
    public float range = 100f;


    // Start is called before the first frame update
    void Start()
    {

        // searches for Camera object
        GameObject temp = GameObject.Find("First Person Camera");
        if (temp != null)
        {
            // gets Camera's info
            cam = temp.GetComponent<Camera>();
        }
        else
        {
            Debug.Log("Camera not found");
        }

        // searches for firePoint object
        GameObject temp2 = GameObject.Find("FirePoint");
        if (temp2 != null)
        {
            // gets firePoint's transform info
            firePoint = temp2.GetComponent<Transform>();
        }
        else
        {
            Debug.Log("FirePoint not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // --------------------------------------- Still Not Finished ---------------------------------------
        // a bad attempt to implement 360-degree aiming function (still not finished)
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Ray ray = new Ray(firePoint.transform.position, mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, range);

        Debug.DrawRay(firePoint.transform.position, mousePosition, Color.red);
        if (hits.Length > 0)
        {
            Debug.DrawLine(hits[0].point, hits[0].point + Vector3.left * 25f, Color.green);
        }
        // --------------------------------------- Still Not Finished ---------------------------------------

        // shoot if mouse left button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        // get mouse position
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);

        // shoot and get object info
        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, mousePosition, out hit, range))
        {
            // These lines of code are for debugging purposes only
            // Debug.DrawLine(firePoint.transform.position, firePoint.transform.position + mousePosition * range, Color.red); // Draw a red line of aiming
            // Debug.DrawLine(hit.point, hit.point - Vector3.forward * range, Color.green); // Draw a green line from hit point
            Debug.Log(hit.transform.name); // print out the name of object being hit

            // apply damage to target
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.takeDamage(damage);
            }
        }
    }
}
