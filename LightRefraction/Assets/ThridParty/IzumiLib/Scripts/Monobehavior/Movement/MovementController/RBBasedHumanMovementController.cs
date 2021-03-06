using UnityEngine;

namespace IzumiLib
{
    /// <summary>
    /// Rigidbody based FPS/TPS human movement controller.<br/>
    /// - Accepts physical interaction.<br/>
    /// - Requires curved surface on bottom collider (like capsule) to climbs up stairs. 
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class RBBasedHumanMovementController : MonoBehaviour
    {
        //inspector
        [Header("Movement")]
        [Min(0)]
        public float Speed = 5f;
        [Min(0)]
        public float JumpHeight = 2f;
        [SerializeField]
        Cooldown JumpCD = new Cooldown(0.1F);
        public bool controllableInAir = true;
        [SerializeField]
        [Min(0)]
        float modelRotateLerpFactor = 20F;

        [Header("SelfReference")]
        [SerializeField]
        CollisionChecker _groundChecker;
        [SerializeField]
        Transform _modelYAxis;
        [Tooltip("Warn: CameraYAxis should only do Y rotation, or moving vector could towards sky/ground.")]
        [SerializeField]
        protected Transform _cameraYAxis;

        //data
        public bool IsGrounded => _groundChecker.CollidingAny;
        public Rigidbody Rigidbody { get; private set; }
        private Vector3 _inputs = Vector3.zero;
        public Quaternion ModelTargetRotation { get; private set; }

        void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        void Update()
        {
            if (IsGrounded || controllableInAir)
            {
                _inputs = Vector3.zero;
                _inputs.x = Input.GetAxis("Horizontal");
                _inputs.z = Input.GetAxis("Vertical");
                if (_inputs != Vector3.zero)
                {
                    _inputs = _cameraYAxis.TransformVector(_inputs).normalized;
                    ModelTargetRotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(_inputs.x, _inputs.z), 0);
                }
                else
                {
                    // standing still behavior
                }
            }
            JumpCD.AddDeltaTime();
            if (Input.GetButtonDown("Jump") && JumpCD.IsReady) //prevents multiple jumps invoked before leaving ground
            {
                if (IsGrounded)
                {
                    JumpCD.Reset();
                    Rigidbody.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                }
                // else -> mid-air jump?
            }
        }
        void FixedUpdate()
        {
            if (_inputs != Vector3.zero)
            {
                Rigidbody.MovePosition(Rigidbody.position + _inputs * Speed * Time.fixedDeltaTime);
            }
            _modelYAxis.rotation = Quaternion.LerpUnclamped(_modelYAxis.rotation, ModelTargetRotation, modelRotateLerpFactor * Time.fixedDeltaTime);
        }
    }

}