using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detector : MonoBehaviour
{
    public static bool danore =false;
    // Start is called before the first frame update
    void Start()
    {
        danore=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "hitboxcolider"){
            danore=true;
            print("deu cerrto");
        }
    }
    
}
