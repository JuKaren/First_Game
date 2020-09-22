using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int pontuacao;
    public Text textoPontos;
    public GameObject fimDeJogo;
    public GameObject proximoNivel;

    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        textoPontos.text = pontuacao.ToString();
    }

    public void ShowGameOver()
    {
        fimDeJogo.SetActive(true);
    }

    public void RestarGame(string nomelvl)
    {
        SceneManager.LoadScene(nomelvl);
    }
}
