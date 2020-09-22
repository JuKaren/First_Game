using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D corpoRigido;
    private Animator animacao;

    private bool estaPulando;
    private bool puloDuplo;

    public float velocidade;
    public float forcaPulo;

    void Start()
    {
        corpoRigido = GetComponent<Rigidbody2D>();
        animacao = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    // Movimentação do personagem
    void Move()
    {
        Vector3 movement = new Vector3 (Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * velocidade;

        if (Input.GetAxis("Horizontal") > 0)
        {
            animacao.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            animacao.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            animacao.SetBool("walk", false);
        }
    }

    // Pulo
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!estaPulando)
            {
                corpoRigido.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
                puloDuplo = true;
                animacao.SetBool("jump", true); // animação
            }
            else
            {
                if (puloDuplo)
                {
                    corpoRigido.AddForce(Vector2.up * (forcaPulo/2), ForceMode2D.Impulse);
                    puloDuplo = false;
                }
            }
            
        }
    }

    // Verifica colisão
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.layer == 8)
        {
            estaPulando = false;
            animacao.SetBool("jump", false);
        }

        if (colisor.gameObject.tag == "Spike" || colisor.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    // Verifica o fim da colisão
    void OnCollisionExit2D(Collision2D colisor)
    {
        if (colisor.gameObject.layer == 8)
        {
            estaPulando = true;
        }
    }
}