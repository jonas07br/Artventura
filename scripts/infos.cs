using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infos : MonoBehaviour
{
    private bool showinfo;
    private bool info;
    private bool closeinfo;
    private Animator anim;
    private SpriteRenderer sprite;
    public Text texto;
    public static string fala;
    char[] ctr;
    private bool mostrou;
    void Start()
    {

        anim = GetComponent<Animator>();
        showinfo=true;
        ctr = fala.ToCharArray();
        mostrou=true;
        PlayerMove.info=true;
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Ativando",showinfo);
        anim.SetBool("Ativado",info);
        anim.SetBool("sumiu",closeinfo);
        fecharinfo();
    }
    public void ativado(){
        showinfo=false;
        info=true;
        print("teste");
    }
    public void show(){
        while(mostrou){
        StartCoroutine(Showtext());
        mostrou=false;
        }
        
    }
    IEnumerator Showtext(){
        int count = 0;
        while (count<ctr.Length){
            yield return new WaitForSeconds(0.01f);
            texto.text += ctr[count];
            count++;
        }
        
    }
    public void fecharinfo(){
        if (Input.GetButtonDown("Fire1")){
            info=false;
            closeinfo= true;

    }
    }
    public void destroytext(){
        
    }
    public void destroyinfo(){
        texto.text = " ";
        Destroy(this.gameObject);
        PlayerMove.info = false;
    }

    }
    
    

