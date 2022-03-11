using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPS_Player : MonoBehaviour
{
    
    InputAction move, attack, jump, mouse;
    Vector3 velocity;
    public Vector3 cameraPos;
    Vector2 vector2;
    CharacterController characterController;
    public GameObject mazule; 
    public GameObject bullet;
    public GameObject cameraPivot;
    public float xLimit, yLimit;
    public int xSet, ySet;
    public float speed = 2.0f, jumpPower = 5.0f;



    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
        InputSetting();

        vector2 = mouse.ReadValue<Vector2>();

        

        Screen.lockCursor = true;
    }


    private void Update()
    {
        Action(move.ReadValue<Vector2>(), jump.triggered, attack.triggered);

        cameraPivot.gameObject.transform.position = this.gameObject.transform.position + cameraPos;
        Camera(mouse.ReadValue<Vector2>());
    }



    void InputSetting()
    {
        move = GetComponent<PlayerInput>().currentActionMap["Move"];
        attack = GetComponent<PlayerInput>().currentActionMap["Attack"];
        jump = GetComponent<PlayerInput>().currentActionMap["Jump"];
        mouse = GetComponent<PlayerInput>().currentActionMap["Mouse"];
    }



    void Action(Vector2 vector2, bool isJump, bool isAttack)
    {
        // à⁄ìÆ
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;

            var input = new Vector3(vector2.x, 0, vector2.y);

            if (input.magnitude > 0f)
            {
                //transform.LookAt(transform.position + input);
                velocity = input * speed;
                velocity = transform.TransformDirection(velocity);
                
            }
            else
            {
                
            }

            if (isJump == true)
            {
                velocity.y += jumpPower;
            }

        }

        // ÉWÉÉÉìÉv
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // ìÆÇ©Ç∑
        characterController.Move(velocity * Time.fixedDeltaTime * speed);

        // çUåÇ
        if (isAttack == true)
        {
            Debug.Log("çUåÇ");
            GameObject newBullet = Instantiate(bullet, mazule.transform.position, transform.rotation);
            Vector3 direction = mazule.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(direction * 10, ForceMode.Impulse);
            newBullet.name = bullet.name;

            Destroy(newBullet, 10);
        }
    }

    // ÉJÉÅÉâëÄçÏ
    void Camera(Vector2 mouseVelocity)
    {
        cameraPivot.transform.eulerAngles += new Vector3(Mathf.Clamp((mouseVelocity.y + vector2.y), -yLimit, yLimit) / ySet, Mathf.Clamp((mouseVelocity.x + vector2.x), -xLimit, xLimit) / xSet, 0);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cameraPivot.transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
