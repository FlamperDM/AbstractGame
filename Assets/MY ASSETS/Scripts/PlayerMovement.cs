using UnityEngine;

namespace AbstractGame.ThirdPerson
{
    [RequireComponent(typeof(CharacterController2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Third Person")]
        CharacterController2D characterController;
        Joystick joystick;

        public float runSpeed = 40;

        Vector2 targetVelocity;
        Vector3 rotation;
        void Awake()
        {
            characterController = GetComponent<CharacterController2D>();
            joystick = GameObject.Find("Variable Joystick").GetComponent<Joystick>();
        }
        void Update()
        {
            targetVelocity.x = joystick.Horizontal * runSpeed;
            targetVelocity.y = joystick.Vertical * runSpeed;

            rotation = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);
        }
        void FixedUpdate()
        {
            targetVelocity *= Time.deltaTime * 10;
            characterController.Move(targetVelocity, rotation);
        }
    }
}