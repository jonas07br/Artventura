 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private gamecontroler _gamecontroler;
    public float moveSpeed = 4f;

    private float horizontalMove = 0;

    public bool chao;

    private bool pulando = false;

    public float jumpForce = 10f;

    public float jumpTime = 1f;

    private float jumpTimeCounter;

    public static bool face = true;

    private Rigidbody2D playerRb;

    private Animator anim;

    public bool andando;
    public int cargas;

    private bool atacando;
    public bool ataquedistancia;
    public Transform Mao;
    public Transform ponta;
    public Vector3 spawn; 
    public GameObject hitboxprefab;
    public GameObject projetil;
    public float vidas;
    public static bool info;
    
    

    private SpriteRenderer sprite;

    private bool imortal = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        _gamecontroler = FindObjectOfType(typeof(gamecontroler)) as gamecontroler;
        _gamecontroler.playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {   
        Move();
        Jump();
        Atack();
        AnmChanger();
        ataquelongo();
        isalive();
    }
    

    private void Move()
    {
        if (Input.GetButton("Horizontal") && atacando==false && ataquedistancia==false && info==false)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalMove, 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

            if (horizontalMove > 0 && !face)
            {
                Flip();
            }
            else if (horizontalMove < 0 && face)
            {
                Flip();
            }
            andando = true;
        }
        else
        {
            andando = false;
        }
    }
    private void Flip()
    {
        face = !face;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && chao && !atacando)
        {
            playerRb.velocity = Vector2.up * jumpForce;
            pulando = true;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKey(KeyCode.Space) && pulando)
        {
            if (jumpTimeCounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                pulando = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            pulando = false;
        }
    }

    private void Atack()
    {
        if (Input.GetButtonDown("Fire1") && !atacando && !ataquedistancia && chao && (anim.GetBool("Damage")==false))
        {
            atacando = true;
        }
    }
    private void ataquelongo()
    {
        if(Input.GetButtonDown("Fire2") && !ataquedistancia && chao && cargas > 0 && !atacando)
        {
            ataquedistancia = true;
            anim.SetBool("ataquedistancia", true);
            cargas -= 1;
            _gamecontroler.CargaAtt(cargas);
        }
    }

    private void AtackEnd(){
        atacando = false;
        ataquedistancia = false;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "coletavel")
        {
            Destroy(col.gameObject);
            cargas++;
            _gamecontroler.CargaAtt(cargas);
        }
        if ((col.gameObject.tag == "dano" || col.gameObject.tag == "inimigo" || col.gameObject.tag == "espinhos") && imortal == false)
        {
            StartCoroutine(DamagePlayer());
            _gamecontroler.hit();
            imortal = true;
            vidas--;
        }
        if (col.gameObject.tag == "checkpoint"){
            spawn = col.gameObject.transform.position;
        }
        if (col.gameObject.tag == "buraco"){
            vidas=vidas-5;
        }
        
        
    }

    public IEnumerator DamagePlayer(){
        anim.SetBool("Damage", true);
        sprite.color = new Color(1f, 0, 0, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        anim.SetBool("Damage", false);

        for (int i = 0; i<6; i++){
            sprite.enabled=false;
            yield return new WaitForSeconds(0.15f);
            
            sprite.enabled=true;
            yield return new WaitForSeconds(0.15f);
        }

        imortal = false;
    }
    private void AnmChanger()
    {
        anim.SetFloat("VelocidadeY", playerRb.velocity.y);
        anim.SetBool("Andando", andando);
        anim.SetBool("Atacando", atacando);
        anim.SetBool("Pulando", pulando);
        anim.SetBool("Chao", chao);
        anim.SetBool("ataquedistancia", ataquedistancia);
        
    }
     public void hitboxataque()
    {
        GameObject hitBoxTemp = Instantiate(hitboxprefab, Mao.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
        
    }
    public void areatiro()
    {
        GameObject tiro = Instantiate(projetil, ponta.position, transform.localRotation);


    }
    public void isalive(){
        if (vidas<=0){
            transform.position=spawn;
            vidas=5;
            _gamecontroler.respawn();
        }
    }
}