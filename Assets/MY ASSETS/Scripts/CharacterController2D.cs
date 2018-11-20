using UnityEngine;
namespace AbstractGame.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        [Header("Third Person")]
        [Range(0, .3f)] [SerializeField] float movementSmoothing = .05f;

        public float fallMultiplier = 2.5f;
        public float lowMultiplier = 2f;

        Rigidbody2D body;
        Vector3 velocity = Vector3.zero;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 targetVelocity, Vector3 rotate)
        {
            if (rotate != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, rotate);
            }

            body.velocity = Vector3.SmoothDamp(body.velocity, targetVelocity, ref velocity, movementSmoothing);
        }
    }
}