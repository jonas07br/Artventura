using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public bool vazio;
    public bool checkativando;
    public bool ativado;
    private Animator anim;
    private SpriteRenderer sprite;
    void Start()
    {
        vazio=true;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ativando",checkativando);
        anim.SetBool("Ativado",ativado);
        anim.SetBool("Vazio",vazio);
    }
    public void OnTriggerEnter2D (Collider2D col){
        if (col.gameObject.tag == "Player" && vazio==true){
            checkativando=true;
            vazio=false;
        }
    }
    public void ativar(){
        ativado = true;
        checkativando=false;
    }

}
