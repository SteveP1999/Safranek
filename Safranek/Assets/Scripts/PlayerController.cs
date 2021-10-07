using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private CharacterController characterController;
    [SerializeField]private Animator animator;
    [SerializeField]private float movementSpeed=3f;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    private float turnSmoothing = 0.1f;
    private float jumpHeight=2f;
    private float gravity=-30f;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private Vector3 move;
    private bool isGrounded=false;
    private void Update()
    {
        isGrounded=Physics.CheckSphere(groundCheck.position,0.1f,groundLayer);
        if(isGrounded&&velocity.y<0){
            velocity.y=-2f;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        move=new Vector3(x,0f,z).normalized;
        if(move.magnitude>=0.1f){
            float targetAngle=Mathf.Atan2(move.x,move.z)*Mathf.Rad2Deg;
            float angle=Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothing);
            transform.rotation=Quaternion.Euler(0f,angle,0f);
            characterController.Move(move*movementSpeed*Time.deltaTime);
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking",false);
        }
        if (isGrounded&&Input.GetButtonDown("Jump"))
        {
            velocity.y=Mathf.Sqrt(jumpHeight*-2f*gravity);
        }
        velocity.y+=gravity*Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);
    }
}
