using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espinho : MonoBehaviour
{
    public float shootSpeed;

    public float turnigSpeed;

    public float lerpTime = 3;

    private float currentLerpTime = 0;

    public float angleOffset;

    public bool lookingPlayer;

    private Transform player;

    Rigidbody2D espinhoRB;

    private Vector3 startPosition;

    private bool restarting;
    private bool goingSide;

    private float sideX;
    private float sideY = -6.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        espinhoRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingPlayer)
        {
            lookPlayer();
        }
    }

    void LateUpdate()
    {
        if (restarting)
        {
            currentLerpTime += Time.deltaTime;

            transform.position =
                Vector3
                    .Lerp(transform.position,
                    startPosition,
                    currentLerpTime / lerpTime);

            transform.rotation =
                Quaternion
                    .Slerp(transform.rotation,
                    Quaternion.Euler(new Vector3(0, 0, 0)),
                    Time.deltaTime * turnigSpeed);
            if (transform.position == startPosition && transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))
            {
                restarting = false;
            }
        }
        if (goingSide)
        {
            currentLerpTime += Time.deltaTime;

            transform.position =
                Vector3
                    .Lerp(transform.position,
                    new Vector3(sideX, sideY, 10),
                    currentLerpTime / lerpTime);

            if (transform.position == new Vector3(sideX, sideY, transform.position.z))
            {
                goingSide = false;
            }
        }
    }

    public void shootPlayer()
    {   
        restarting = false;
        goingSide = false;
        lookingPlayer = false;        
        espinhoRB.simulated = true;
        Vector2 moveDir =
            (player.transform.position - transform.position).normalized *
            shootSpeed;
        espinhoRB.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    public void startLookingPlayer()
    {
        currentLerpTime = 0;
        lookingPlayer = true;
    }

    void lookPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        transform.rotation =
            Quaternion
                .Slerp(transform.rotation,
                Quaternion.Euler(new Vector3(0, 0, angle + angleOffset)),
                Time.deltaTime * turnigSpeed);

        //espinhoRB.rotation = angle + angleOffset;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "chao" || col.gameObject.tag == "parede")
        {
            espinhoRB.velocity = new Vector2(0, 0);
            espinhoRB.simulated = false;
        }
    }

    public void startPos()
    {
        restarting = true;
        goingSide = false;
        lookingPlayer = false;
        currentLerpTime = 0;
        // espinhoRB.simulated = true;
        //Vector2 moveDir =
        //     (startPosition - transform.position).normalized * shootSpeed;
        // espinhoRB.velocity = new Vector2(moveDir.x, moveDir.y);
        // espinhoRB.rotation = 0;
    }

    public void sidePos()
    {
        goingSide = true;
        currentLerpTime = 0;
        int rand = Random.Range(1, 3);
        if (rand == 1) {
            sideX = -15;
        } else {
            sideX = 15;
        }
        lookingPlayer = true;
    }
}
