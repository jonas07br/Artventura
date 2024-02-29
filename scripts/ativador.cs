using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ativador : MonoBehaviour
{
    public GameObject infoprefab;
    public Transform infopos;
    public string textotutor;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag=="Player"){
            print("pegou");
            GameObject info = Instantiate(infoprefab, infopos.position, transform.localRotation);
            infos.fala=textotutor;
        }
    }
    
}
