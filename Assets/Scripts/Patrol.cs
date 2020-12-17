using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float distance;
    private float speed=1;
   PlayerController player;
    private bool MovingLeft = true;
    PolygonCollider2D polygon;
    public Transform groundDetection;
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<PlayerController>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Dead) 
        {
           polygon.enabled = false;
        }
        transform.Translate(Vector2.left*speed*Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position,Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (MovingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                MovingLeft = false;

            }
            else 
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                MovingLeft = true;
            }
             
        }

       

    }
  
}
