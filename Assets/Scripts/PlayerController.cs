using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float MoveSpeed = 1f;

   private PlayerControls PlayerControls;
   private Vector2 MoveInput;
   private Rigidbody2D rb;
   private Animator anim;
   private SpriteRenderer spriteRenderer;

   private void Awake()
    {
        PlayerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerDirection();
    }

    private void PlayerInput()
    {
        MoveInput = PlayerControls.Movement.Move.ReadValue<Vector2>();
        anim.SetFloat("MoveX", MoveInput.x);
        anim.SetFloat("MoveY", MoveInput.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + MoveInput * (MoveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerDirection()
{
    Vector2 mouseScreen = Mouse.current.position.ReadValue();
    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

    if (mouseWorld.x < transform.position.x)
    {
        spriteRenderer.flipX = true;
    }
    else
    {
        spriteRenderer.flipX = false;
    }
}
}