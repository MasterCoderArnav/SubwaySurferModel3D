using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    public float forwardSpeed;
    public float maxForwardSpeed;
    private Vector3 direction;
    private int desiredLane = 1; //0 left 1 mid 2 right
    [SerializeField]
    private int laneDistance = 4;
    public float jumpForce = 10;
    public float gravity = -20;
    public Animator animator;
    private bool isSliding = false;
    void Start(){
        controller = GetComponent<CharacterController>();
    }
    private IEnumerator slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.height = 1;
        controller.center = new Vector3(0, -0.5f, 0);
        yield return new WaitForSeconds(1.5f);
        controller.height = 2;
        controller.center = new Vector3(0, 0, 0);
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        animator.SetBool("isGameStarted", true);
        animator.SetBool("New Bool", !controller.isGrounded);
        if (SwipeManager.swipeDown&&!isSliding)
        {
            StartCoroutine(slide());
        }
        direction.z = forwardSpeed;
        forwardSpeed += 0.1f * Time.deltaTime;
        if (forwardSpeed > maxForwardSpeed)
        {
            forwardSpeed = maxForwardSpeed;
        }
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)){
                direction.y = jumpForce;
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;

        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        controller.Move(direction * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10*Time.fixedDeltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().playSound("gameOver");
        }
    }
}