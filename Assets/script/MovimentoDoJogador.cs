using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoJogador : MonoBehaviour
{
    [Header("Referencias")]
    private Rigidbody2D oRigidbody2D;
    private Animator oAnimator; 

    [Header("Movimento Horizontal")]
    public float velocidadeJogador;
    public bool indoParaDireita;

    [Header("Pulo")]
    public bool estaNoChao;
    public float alturaDoPulo;
    public float tamanhoDoRaioDeVerificacao;
    public Transform verificadorDeChao;
    public LayerMask layerDoChao;

    void Awake()
    {
        oRigidbody2D = GetComponent<Rigidbody2D>();
        oAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        MovimentarJogador();
        Pular();
    }

    private void MovimentarJogador()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");

        oRigidbody2D.velocity = new Vector2(movimentoHorizontal * velocidadeJogador, oRigidbody2D.velocity.y);

        if(movimentoHorizontal > 0)
        {
            transform.localScale = new Vector3(1f,1f,1f);
            indoParaDireita = true;
        }
        else if(movimentoHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f,1f,1f); 
            indoParaDireita = false;
        }

        if(movimentoHorizontal == 0 && estaNoChao == true)
        {
            oAnimator.Play("jogador-idle");
        }
        
        else if(movimentoHorizontal != 0 && estaNoChao == true)
        {
            oAnimator.Play("jogador-andando");
        }
    }

    private void Pular()
    {
        estaNoChao = Physics2D.OverlapCircle(verificadorDeChao.position, tamanhoDoRaioDeVerificacao, layerDoChao); //Cria um circulo para ver se sobrepoe o chap
        
        if(Input.GetButtonDown("Jump") && estaNoChao == true)
        {
            oRigidbody2D.AddForce(new Vector2(0f, alturaDoPulo), ForceMode2D.Impulse);
        }
        if(estaNoChao == false)
        {
            oAnimator.Play("jogador-pulando");
        }
    }
}
