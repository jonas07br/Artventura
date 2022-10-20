using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Rigidbody2D coxinharb;

    public int vida = 2;

    public float speed;

    public GameObject hitbox;

    private bool ataque;

    public Collider2D areadano;
    public Collider2D visao;

    public GameObject hitboxprefab;

    public Transform mao;

    public Transform groundCheck;

    public float groundDistance;

    private float xPosInicial;

    private float xPosMax;

    private float xPosMin;

    public float areaDeMovimento;

    private Animator anim;

    public bool imortal = false;

    private SpriteRenderer sprite;

    private int h = 2;

    public bool face;

    private bool playerNaVisao = false;

    public LayerMask layerChao;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coxinharb = GetComponent<Rigidbody2D>();

        xPosInicial = transform.position.x;
        xPosMax = xPosInicial + areaDeMovimento;
        xPosMin = xPosInicial - areaDeMovimento;
    }

    void Update()
    {
        Move();
        
        
    }

    void Move()
    {
        if (ataque == false)
        {
            coxinharb.velocity = new Vector2(h * speed, coxinharb.velocity.y);

            RaycastHit2D ground =
                Physics2D
                    .Raycast(groundCheck.position,
                    Vector2.down,
                    groundDistance,
                    layerChao);

            if (
                ground.collider == false ||
                (transform.position.x >= xPosMax && !face) ||
                (transform.position.x <= xPosMin && face)
            )
            {
                Flip();
            }
            else if (ground.collider.tag != "chao")
            {
                Flip();
            }

            if (ataque == false)
            {
                anim.SetBool("andando", true);
            }
            else
            {
                anim.SetBool("andando", false);
            }
        }
        else
        {
            coxinharb.velocity = new Vector3(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            playerNaVisao = true;
            ataque = true;
            anim.SetBool("atacando", true);
        }
        if (col.gameObject.tag == "hitboxcolider" && imortal == false)
        {
            imortal = true;

            print("dano no mob");

            vida -= 1;

            StartCoroutine(DamageAnm());

            if (vida <= 0)
            {
                Destroy (hitbox);
                Debug.Log("vida menor = zero");
                anim.SetTrigger("morreu");
            }
            else
            {
            }
        }
    }
    

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerNaVisao = false;
        }
    }

    void fimataque()
    {
        if (playerNaVisao == false)
        {
            anim.SetBool("atacando", false);
            ataque = false;
        }
    }
    

    private void Flip()
    {
        h = h * -1;
        face = !face;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void morte()
    {
        Destroy(this.gameObject);
    }

    public void dano()
    {
        GameObject hitBoxTemp =
            Instantiate(hitboxprefab,
            mao.position,
            transform.localRotation,
            mao.transform);
        Destroy(hitBoxTemp, 0.5f);
    }

    public IEnumerator DamageAnm()
    {
        print("damage anm");
        anim.SetBool("Damage", true);
        sprite.color = new Color(1f, 0, 0, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = new Color(1f, 1f, 1f, 1f);
        anim.SetBool("Damage", false);

        for (int i = 0; i < 4; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.15f);

            sprite.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }

        imortal = false;
    }
    
}
