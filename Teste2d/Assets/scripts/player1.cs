using UnityEngine;

public class player1 : MonoBehaviour
{
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float HP = 100f;
    [SerializeField] private LayerMask HarmLayer;
    
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private int andandoHash = Animator.StringToHash("andando");
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Apenas declaramos as variáveis aqui
    private GameObject cameraPrincipal;
    private CameraController cameraScript;
    private bool isDead = false; 

    float horizontal;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Buscamos a câmera e o script no Awake de forma segura
        cameraPrincipal = GameObject.FindWithTag("MainCamera");
        if (cameraPrincipal != null)
        {
            cameraScript = cameraPrincipal.GetComponent<CameraController>();
        }
    }

    private void Update()
    {
        // Se já morreu, impede de andar ou pular
        if (isDead) return;

        horizontal = Input.GetAxis("Horizontal");
        
        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetBool(andandoHash, horizontal != 0);
        spriteRenderer.flipX = horizontal > 0;

        // Checa a morte
        if (HP <= 0)
        {
            Morrer();
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // Move só no eixo X
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private void Morrer()
    {
        isDead = true;
        
        // Desativa o colisor com o cenário para ele cair no vácuo
        boxCollider.enabled = false;

        // Para a movimentação para os lados na hora da morte
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        // Desativa o script de seguir da câmera (fazendo ela parar)
        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }
    }

    // Detecta quando ENCOSTA em algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
        if (((1 << collision.gameObject.layer) & HarmLayer) != 0)
        {
            HP -= 10f;
        }
    }

    // Detecta quando SAI de cima de algo
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isDead) return;

        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}