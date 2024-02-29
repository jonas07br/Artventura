using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabaloInimigo : MonoBehaviour
{
    public Inimigo inimigo;

    private bool canDash = true;

    private bool isDashing = false;

    public float dashingPower = 10f;

    public float dashingTime = 0.2f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private TrailRenderer tr;

    void Start()
    {
        inimigo = GetComponent<Inimigo>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inimigo.ataque && !isDashing)
        {
            isDashing = true;
            inimigo.dashing = isDashing;
            Debug.Log("teste");
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity =
            new Vector2(transform.localScale.x * dashingPower * -1, 0f);
        Debug.Log(rb.velocity);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        inimigo.dashing = isDashing;
    }
}
