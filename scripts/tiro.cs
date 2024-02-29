using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiro : MonoBehaviour
{
    [SerializeField]private float speed =0.5f;
    
    
    
    private Vector3 move = Vector3.zero;
   
    void Start()
    {
        direcao();
        Destroy(gameObject, 2f);
    }

    
    void Update()
    {
        Move();
        
        
    }
    private void Move()
    {
        move.x = speed + Time.deltaTime;
        transform.position +=  move;



    }
    public void direcao()
    {
        if (PlayerMove.face!=true){
        speed=-0.1f;
        Vector3 lado = transform.localScale;
        lado.x *= -1;
        transform.localScale = lado;
        }   
    }
    public void OnTriggerEnter2D(Collider2D coll){
            Destroy(gameObject);
            
    }
    
}   

