using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    public PlayerInputActions playerInput;
    public CharacterController cc;
    public Animator animator;
    private Vector2 currentMovementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.magnitude > 0; // Check if there's any movement input
    }

    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void Update()
    {
        HandleAnimation();
        cc.Move(currentMovement * Time.deltaTime);
    }
}
