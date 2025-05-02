using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private Transform tankHead;
    [SerializeField] Transform tankTreads;
    [SerializeField] Rigidbody2D rb;
    private Camera mainCamera;
    private Vector2 mouseWorldPos;

    [Tooltip("Settings")]
    [SerializeField] private float sensitivity = 10f;
    [SerializeField] float turningRate = 4f;
    [SerializeField] private float normalSpeed = 0.05f;
    [SerializeField] private float sprintSpeed = 0.1f;
    private Vector2 moveInput;

    private bool isSprinting = false;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            gameObject.GetComponent<PlayerInput>().enabled = false;
            return;
        }
    }
    public override void OnNetworkDespawn()
    {
        if (!IsOwner) {return;}

    }
    void Update()
    {
        if (!IsOwner) {return;}
        LookAtMouse();
        Rotate();
    }
    private void FixedUpdate()
    {
        if (!IsOwner) { return; }
        Move();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!IsOwner) { return; }
    }
    private void LookAtMouse()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsOwner) { return; }
        moveInput = context.ReadValue<Vector2>();
    }
    private void Move()
    {
        float currentSpeed;
        if (!isSprinting)
        {
            currentSpeed = normalSpeed;
        }
        else
        {
            currentSpeed = sprintSpeed;
        }
        rb.linearVelocity = tankTreads.up * moveInput.y * currentSpeed;
    }
    private void Rotate()
    {
        tankTreads.Rotate(0f, 0f, moveInput.x * -turningRate * Time.deltaTime);
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!IsOwner) { return; }
        if (context.performed) {isSprinting = true;}
        else {isSprinting = false;}
    }
    private void Sprinting()
    {
        
    }
    public void OnFire(InputAction.CallbackContext context)
    {

    }
}
