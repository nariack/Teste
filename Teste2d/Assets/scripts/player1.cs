using UnityEngine;

public class player1 : MonoBehaviour
{

    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 10f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    float horizontal;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        {
            // Pulo
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }


    }

    private void FixedUpdate()
    {
        // Move só no eixo X
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }


    // Detecta quando ENCOSTA em algo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    // Detecta quando SAI de cima de algo
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}
