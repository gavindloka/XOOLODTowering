using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        public Rigidbody rb;
        public PlayerInputActions InputActions;
        public GameObject playerCamera;

        [Header("Walking and Running")]
        public static float BaseSpeed = 15f;
        public float WalkSpeed = 0.4f;
        public float RunSpeed = 1.25f;
        public float RotationSpeed = 0.1f;
        public float CombatRunSpeed = 1.4f;

        [Header("Dashing")]
        public float DashForce;
        public float DashUpwardForce;
        public float DashDuration = 1;
        public float DashCd;
        private int dashCount = 0;
        private bool isDashing;
        private bool isDashingAllowed = true;
        private float nextDashTime = 0f;

        [Header("Jumping")]
        public float JumpHeight = 10f;
        private bool isGrounded;
        private float lastGroundedTime;
        private bool isFalling = false;
        private float stepOffset;
        public float HardLandingThreshold = 3f;


        [Header("UI")]
        private bool isShopCanvasOpen = false;

        public Vector3 direction;
        private float rotateVelocity;
        public Animator PlayerAnimator;
        public bool WalkToggle = true;
        public static bool CombatToggle = false;
        public bool IsOnCircle;
        [SerializeField] UIController ui;
        private bool isDrawingOrSheathing = false;
        public static bool IsCanvasActive;
        public GameObject ThighSword;
        public GameObject HandSword;

        [Header("MazeTeleporter")]
        public bool IsOnTeleporter;
        public bool IsOnSecondFloor = false;
        public bool IsTeleporterCooldown = false;
        private float teleporterCooldownDuration = 5f;
        private float lastTeleportTime = 0f;

        [Header("EndRoom")]
        public bool IsOnEndTeleporter;

        private void Start()
        {
            ui = FindObjectOfType<UIController>();
        }
        //sebelum start, dijalanin dlu method Awake
        private void Awake()//bawaan monobehavior
        {
            InputActions = new PlayerInputActions();
        }


        private void OnEnable()//bawaan monobehavior
        {

            //walk idle run
            InputActions.Player.Move.Enable();
            InputActions.Player.WalkToggle.Enable();
            InputActions.Player.Dash.performed += OnDashPerformed;
            InputActions.Player.Dash.Enable();
            InputActions.Player.Jump.Enable();

            AddInputCallback();
        }
        private void OnDisable()
        {
            InputActions.Player.Move.Disable();
            InputActions.Player.Dash.performed -= OnDashPerformed;
            InputActions.Player.Jump.Disable();

            RemoveInputCallBack();
        }

        private void OnDashPerformed(InputAction.CallbackContext context)
        {

            if (isDashingAllowed && !isDashing && !IsCanvasActive)
            {
                StartCoroutine(Dash());
                Debug.Log(dashCount);
            }
        }
        IEnumerator Dash()
        {
            isDashing = true;
            PlayerAnimator.SetBool("isDashing", true);
            rb.AddForce(transform.forward * DashForce + Vector3.up * DashUpwardForce, ForceMode.Impulse);

            yield return new WaitForSeconds(DashDuration);

            rb.velocity = Vector3.zero;

            if (direction.magnitude > 0)
            {
                PlayerAnimator.SetBool("isDashing", true);
                WalkToggle = false;
            }

            PlayerAnimator.SetBool("isDashing", false);
            isDashing = false;
            dashCount++;

            if (dashCount >= 2)
            {
                isDashingAllowed = false;
                StartCoroutine(DashCooldown());
            }
        }

        IEnumerator DashCooldown()
        {
            nextDashTime = Time.time + DashCd;
            yield return new WaitForSeconds(DashCd);
            dashCount = 0;
            isDashingAllowed = true;
        }
        private void AddInputCallback()
        {
            InputActions.Player.WalkToggle.performed += OnWalkTogglePerformed;
            InputActions.Player.Jump.performed += OnJumpPerformed;
        }

        private void RemoveInputCallBack()
        {
            InputActions.Player.WalkToggle.performed -= OnWalkTogglePerformed;
            InputActions.Player.Jump.performed -= OnJumpPerformed;
        }
        //private IEnumerator WaitForFalling()
        //{
        //    float fallStartY = transform.position.y;
        //    yield return new WaitUntil(() => !isGrounded);

        //    float fallDistance = fallStartY - transform.position.y;


        //    if (fallDistance >= 0.5f)
        //    {
        //        PlayerAnimator.SetBool("isFalling", true);
        //    }
        //}

        void Update()
        {
            IsCanvasActive = ui.isShopCanvasActive || ui.isChangeCanvasActive || ui.isRebindCanvasActive || ui.isEnterMazeCanvasActive || ui.isFightBossCanvasActive || ui.isEndTeleporterCanvasActive;

            if (!IsCanvasActive && !isDrawingOrSheathing && !PlayerGetDamage.IsDead)
            {
                playerCamera.SetActive(true);
                float horizontalInput = InputActions.Player.Move.ReadValue<Vector2>().x;
                float verticalInput = InputActions.Player.Move.ReadValue<Vector2>().y;
                //Debug.Log(horizontalInput + ", " + verticalInput);
                direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

                isGrounded = IsGrounded();

                //isOnCircle
                if (transform.position.x >= 918 && transform.position.x <= 929 &&
            transform.position.z >= 506 && transform.position.z <= 520)
                {
                    IsOnCircle = true;
                }
                else
                {
                    IsOnCircle = false;
                }
                Debug.Log("IsGrounded: " + isGrounded);
                Debug.Log("IsOnCircle: " + IsOnCircle);
                Debug.Log(lastGroundedTime);

                if (rb.velocity.y < -0.5f && !isGrounded)
                {
                    isFalling = true;
                    Debug.Log("Player is falling");
                    //PlayerAnimator.SetBool("isFalling", true);
                }
                else
                {
                    isFalling = false;
                }

                if (Input.GetKeyDown(KeyCode.R) && CombatToggle == false)
                {
                    isDrawingOrSheathing = true;
                    CombatToggle = !CombatToggle;
                    PlayerAnimator.Play("DrawSword1");
                    StartCoroutine(EnableActionsAfterAnimation());
                }
                else if (Input.GetKeyDown(KeyCode.R) && CombatToggle == true)
                {
                    isDrawingOrSheathing = true;
                    CombatToggle = !CombatToggle;
                    PlayerAnimator.Play("SheathSword");
                    StartCoroutine(EnableActionsAfterAnimation());
                }
                if (IsOnTeleporter && Input.GetKeyDown(KeyCode.E) && !IsTeleporterCooldown)
                {
                    if (IsOnSecondFloor == false)
                    {
                        transform.position += Vector3.up * 100f;
                        IsOnSecondFloor = true;
                        IsTeleporterCooldown = true;
                    }
                    else if (IsOnSecondFloor == true)
                    {
                        transform.position += Vector3.down * 100f;
                        IsOnSecondFloor = false;
                        IsTeleporterCooldown = true;
                    }
                    StartCoroutine(TeleporterCooldown());
                }


            }
            else if (PlayerGetDamage.IsDead)
            {
                direction = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
            else
            {
                direction = Vector3.zero;
                rb.velocity = Vector3.zero;
                playerCamera.SetActive(false);
            }

        }
        private IEnumerator EnableActionsAfterAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            isDrawingOrSheathing = false;
        }

        private void Move()
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraForwardFlat = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;

            Vector3 worldSpaceDirection = Quaternion.LookRotation(cameraForwardFlat) * direction;

            float targetAngle = Mathf.Atan2(worldSpaceDirection.x, worldSpaceDirection.z) * Mathf.Rad2Deg;

            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotateVelocity, RotationSpeed);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            float currentModifier = BaseSpeed;
            //if (WalkToggle)
            //{
            //    currentModifier *= WalkSpeed;
            //    PlayerAnimator.SetBool("isWalking", true);
            //    PlayerAnimator.SetBool("isRunning", false);
            //}
            //else if(!WalkToggle)
            //{
            //    currentModifier *= RunSpeed;
            //    PlayerAnimator.SetBool("isWalking", false);
            //    PlayerAnimator.SetBool("isRunning", true);
            //}
            //if (CombatToggle==true)
            //{
            //    currentModifier *= CombatRunSpeed;
            //    PlayerAnimator.SetBool("isCombatRunning",true);
            //    PlayerAnimator.SetBool("isRunning",false);
            //    PlayerAnimator.SetBool("isWalking",false);
            //}
            if (isDashing)
            {
                currentModifier *= DashForce;

            }
            else if (CombatToggle)
            {
                currentModifier *= CombatRunSpeed;
                PlayerAnimator.SetBool("isCombatRunning", true);
                PlayerAnimator.SetBool("isRunning", false);
                PlayerAnimator.SetBool("isWalking", false);
            }
            else if (WalkToggle)
            {
                currentModifier *= WalkSpeed;
                PlayerAnimator.SetBool("isWalking", true);
                PlayerAnimator.SetBool("isRunning", false);
            }
            else
            {
                currentModifier *= RunSpeed;
                PlayerAnimator.SetBool("isWalking", false);
                PlayerAnimator.SetBool("isRunning", true);
            }

            rb.velocity = new(worldSpaceDirection.x * currentModifier, rb.velocity.y, worldSpaceDirection.z * currentModifier);

        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            if (isGrounded && !IsCanvasActive)
            {
                rb.AddForce(Vector3.up * JumpHeight * BaseSpeed, ForceMode.Impulse);
                PlayerAnimator.SetTrigger("jump");
                isGrounded = false;
                isFalling = false;
                StartCoroutine(WaitForLanding());
            }
        }


        private void OnWalkTogglePerformed(InputAction.CallbackContext context)
        {
            WalkToggle = !WalkToggle;
        }



        private void LateUpdate()
        {
            if (direction != Vector3.zero)
            {
                Move();
            }
            else if (isDashing)
            {
                DashMove();
            }
            else
            {
                rb.velocity = new(0, rb.velocity.y, 0);
                PlayerAnimator.SetBool("isWalking", false);
                PlayerAnimator.SetBool("isRunning", false);
                //PlayerAnimator.SetBool("isJumping", false);
                PlayerAnimator.SetBool("isCombatRunning", false);
            }
        }

        void DashMove()
        {
            Vector3 worldSpaceDirection = rb.transform.forward; //return hadep kmn
            float currentModifier = BaseSpeed * DashForce;
            rb.velocity = new(worldSpaceDirection.x * currentModifier, rb.velocity.y, worldSpaceDirection.z * currentModifier);
        }
        //private IEnumerator WaitForFallingToLanding()
        //{
        //    yield return new WaitUntil(() => !IsGrounded());
        //    float fallStartY = transform.position.y;
        //    yield return new WaitUntil(() => rb.velocity.y < -2f);
        //    PlayerAnimator.SetBool("isFalling", true);
        //    float fallDistance = fallStartY - transform.position.y;
        //    if (fallDistance > HardLandingThreshold)
        //    {
        //        PlayerAnimator.SetTrigger("hardLand");
        //    }
        //    else
        //    {
        //        PlayerAnimator.SetTrigger("land");
        //    }
        //}
        private IEnumerator WaitForLanding()
        {
            yield return new WaitUntil(() => !IsGrounded());
            float fallStartY = transform.position.y;
            yield return new WaitUntil(() => isGrounded && rb.velocity.y <= 0.001f);
            float fallDistance = fallStartY - transform.position.y;
            if (fallDistance > HardLandingThreshold)
            {
                PlayerAnimator.SetTrigger("hardLand");
            }
            else
            {
                PlayerAnimator.SetTrigger("land");
            }
        }
        private bool IsGrounded()
        {
            float raycastdistance = 1.5f;
            return Physics.Raycast(transform.position, Vector3.down, raycastdistance, Physics.AllLayers);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Teleporter"))
            {
                IsOnTeleporter = true;
                Debug.Log("Player is on teleporter!");
            }
            else
            {
                IsOnTeleporter = false;
            }
            if (other.CompareTag("EndTeleporter"))
            {
                IsOnEndTeleporter = true;
                Debug.Log("Player is on end teleporter");
            }
            else
            {
                IsOnEndTeleporter = false;
            }
        }
        IEnumerator TeleporterCooldown()
        {
            yield return new WaitForSeconds(teleporterCooldownDuration);
            IsTeleporterCooldown = false;
        }
    }
}