using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 vector3;
    //[SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private int lineToMove = 1;
    public float lineDistanse = 4;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (SwipController.swipeRight)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
            }
        }

        if (SwipController.swipeLeft)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (SwipController.swipeUp)
        {
            if (controller.isGrounded)
            {
                Jump(); 
            }
            
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistanse;
        }
        else if (lineToMove == 2)
        {
            targetPosition += Vector3.right * lineDistanse;
        }

        transform.position = targetPosition;
    }

    private void Jump()
    {
        vector3.y = jumpForce;
    }


    void FixedUpdate()
    {
        //vector3.z = speed;
        //vector3.y += gravity * Time.fixedDeltaTime;
        controller.Move(vector3 * Time.fixedDeltaTime);
    }
}
