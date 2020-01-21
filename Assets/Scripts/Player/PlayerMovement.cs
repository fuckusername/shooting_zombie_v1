using UnityEngine;
[RequireComponent(typeof(Animator), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 1000f;

    Vector3 movement;
    Animator animator;
    Rigidbody playerRigidbody;
    int floorMark;
    float camRayLength = 100f;
	
    void Awake()
    {
        floorMark = LayerMask.GetMask("Floor");
        animator = GetComponent<Animator> ();
        playerRigidbody = GetComponent<Rigidbody> ();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Turning();
        Animating(h,v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMark))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            var rotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(rotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }
}