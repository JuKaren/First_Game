using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    public GameObject coletado;
    public int pontos;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.tag == "Player")
        {
            spriteRenderer.enabled = false;
            circleCollider.enabled = false;
            coletado.SetActive(true);

            GameController.instance.pontuacao += pontos;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.3f);
        }
    }
}
