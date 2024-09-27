using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jump = 8f;
    [SerializeField] float speed = 3f;

    private Animator Animator;
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;

    private bool ataqueAbotao = false;
    private bool ataqueCbotao = false;
    private bool ataqueDbotao = false;
    private bool Pulobotao = false;
    private bool deitarbotao = false;
    private bool boladfogobotao = false;
    private bool Chutebotao = false;
    private bool flechabotao = false;
    private bool dançarinobotao = false;
    private float andarbotao = 0f;
    private bool isGrounded = false;

    enum State { IdleA, AtaqueA, AtaqueC, AtaqueD, PuloA, PuloB, Deitar, Boladfogo, Chute, Flecha, Dançarino, Andar}

    State state = State.IdleA;

    void Start()
    {
        Animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    
    }

    // Update is called once per frame
    void Update()
    {
        ataqueAbotao = Input.GetKey(KeyCode.Z);
        ataqueCbotao = Input.GetKey(KeyCode.X);
        ataqueDbotao = Input.GetKey(KeyCode.C);
        Pulobotao = Input.GetKey(KeyCode.Space);
        deitarbotao = Input.GetKey(KeyCode.M);
        boladfogobotao = Input.GetKey(KeyCode.Q);
        Chutebotao = Input.GetKey(KeyCode.F);
        flechabotao = Input.GetKey(KeyCode.S);
        dançarinobotao = Input.GetKey(KeyCode.B);
        andarbotao = Input.GetAxisRaw("Horizontal");


        //enum State { IdleA, AtaqueA, AtaqueC, AtaqueD, PuloA, PuloB, Deitar, Boladfogo, Chute, Flecha, Dançarino, Andar}

            switch (state)
             {
                case State.IdleA: IdleA(); break;
                case State.AtaqueA: AtaqueA(); break;
                case State.AtaqueC: AtaqueC(); break;
                case State.AtaqueD: AtaqueD(); break;
                case State.PuloA: PuloA(); break;
                case State.PuloB: PuloB(); break;
                case State.Deitar: Deitar(); break;
                case State.Boladfogo: Boladfogo(); break;
                case State.Chute: Chute(); break;
                case State.Flecha: Flecha(); break;
                case State.Dançarino: Dançarino(); break;
                case State.Andar: Andar(); break;
                
             }   

    }

    private bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
     private void CloseState(string animationName)
    {
        if(IsAnimationFinished(animationName))
        {
            rigid.gravityScale = 1;

            if (isGrounded)
            {
                state = State.IdleA;
            }
            else state = State.PuloB;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("piso"))
        {
            isGrounded = true;
        }
    }


    //enum State { IdleA, AtaqueA, AtaqueC, AtaqueD, PuloA, PuloB, Deitar, Boladfogo, Chute, Flecha, Dançarino, Andar}
private void IdleA()
    {
        Animator.Play("idleA");
        if (ataqueAbotao)
        {
            state = State.AtaqueA;
        }
         if (ataqueCbotao)
        {
            state = State.AtaqueC;
        }
         if (ataqueDbotao)
        {
            state = State.AtaqueD;
        }
         if (deitarbotao)
        {
            state = State.Deitar;
        }
         if (Chutebotao)
        {
            state = State.Flecha;
        }
         if (dançarinobotao)
        {
            state = State.Dançarino;
        }
        //CADA ANIMACAO É UM ELSE IF
        else if (boladfogobotao)
        {
            state = State.Boladfogo;
        }
        else if (andarbotao != 0) //DEIXA ESSA IGUAL
        {
            state = State.Andar;
        }
        else if (Pulobotao && isGrounded) //ESSE IGUAL TAMBEM
        {
            isGrounded = false;
            rigid.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            state = State.PuloA;
        }
        else if (!isGrounded) //E ESSE TAMBEM E TEM QUE SER O ULTIMO
        {
            state = State.PuloB;
        }
    }

    //enum State { IdleA, AtaqueA, AtaqueC, AtaqueD, PuloA, PuloB, Deitar, Boladfogo, Chute, Flecha, Dançarino, Andar}
    private void AtaqueA()
    {
        Animator.Play("ataqueA");

        CloseState("ataqueA");
    }

    private void AtaqueC()
    {
        Animator.Play("AtaqueC");

        CloseState("AtaqueC");
    }

    private void AtaqueD()
    {
        Animator.Play("ataqueD");

        CloseState("ataqueD");
    }

    private void PuloA()
    {
        Animator.Play("puloA");
            if (andarbotao < 0f && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (andarbotao > 0f && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rigid.velocity = new Vector2(andarbotao * speed, rigid.velocity.y);

        if (rigid.velocity.y < 0) 
        {
            state = State.PuloB;
        }

    }

    private void PuloB()
    {
        Animator.Play("puloB");
        if (andarbotao < 0f && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (andarbotao > 0f && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rigid.velocity = new Vector2(andarbotao * speed, rigid.velocity.y);

        CloseState("puloB");
    }

    private void Deitar()
    {
        Animator.Play("deitar");

        CloseState("deitar");
    }
    private void Boladfogo()
    {
        Animator.Play("boladfogo");

        CloseState("boladfogo");
    }
    private void Chute()
    {
        Animator.Play("Chute");

        CloseState("Chute");
    }
    private void Flecha()
    {
        Animator.Play("flecha");

        CloseState("flecha");
    }
    private void Dançarino()
    {
        Animator.Play("dançarino");

        CloseState("dançarino");
    }
    private void Andar()
    {
        Animator.Play("andar");
            if (andarbotao < 0f && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (andarbotao > 0f && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rigid.velocity = new Vector2(andarbotao * speed, rigid.velocity.y);
        
        if (Pulobotao && isGrounded)
        {
            isGrounded = false;
            rigid.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
            state = State.PuloA;
        }

        else CloseState("andar");
    }



}
