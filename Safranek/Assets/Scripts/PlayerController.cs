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
    private float gravity=-19.62f;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        velocity=new Vector3(x,0f,z).normalized;
        if(velocity.magnitude>=0.1f){
            float targetAngle=Mathf.Atan2(velocity.x,velocity.z)*Mathf.Rad2Deg;
            float angle =Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity,turnSmoothing);
            transform.rotation=Quaternion.Euler(0f,angle,0f);
            characterController.Move(velocity*movementSpeed*Time.deltaTime);
            animator.SetBool("isWalking",true);
        }else{
            animator.SetBool("isWalking",false);
        }
        if (!Physics.CheckSphere(groundCheck.position,0.3f,groundLayer))
            velocity.y+=gravity*Time.deltaTime;
        else
            velocity.y=-0.1f;
        characterController.Move(velocity*Time.deltaTime);
    }
}
