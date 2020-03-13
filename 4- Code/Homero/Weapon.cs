using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform firePoint;
    public GameObject rocketPrefab;
    public Camera cam;
    public Transform testObject;
    Vector3 mousePos;
 

    // Update is called once per frame

 
    
    void Update()
    {

        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // need to grab distance to player from mouse and incoperate i think
        float xdist = Input.mousePosition.x - firePoint.position.x;
        float ydist = Input.mousePosition.y - firePoint.position.y;
        Vector3 dist = new Vector3(xdist, ydist, 0f);

        //mousePos = getMouseWorldPosition();
        Vector3 aimDir = (mousePos - firePoint.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        firePoint.eulerAngles = new Vector3(0f, 0f, angle);
        Debug.Log(angle);
        if (Input.GetButtonDown("Fire1")){
            Shoot(angle);
        }

    }

  

    void Shoot(float angle){
        // shooting logic
       // firePoint.Rotate(angle, 0f, 0f);
        Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
            
        
    }

    public Vector3 getMouseWorldPosition(){

        Vector3 vec = getMouseWorldPositionz(Input.mousePosition, cam);
        vec.z = 0f;
        return vec;
    }

    public  Vector3 getMouseWorldPositionz(){
        return getMouseWorldPositionz(Input.mousePosition, cam);
    }

    public Vector3 getMouseWorldPositionz(Camera worldCamera){
        return getMouseWorldPositionz(Input.mousePosition, worldCamera);
    }

    public Vector3 getMouseWorldPositionz(Vector3 screenPos, Camera worldCamera){
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }


}
