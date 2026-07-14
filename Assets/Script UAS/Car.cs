using UnityEngine;
using SoftTouchUIKit;

public class Car : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBrakeForce;
    private bool isBraking;

    [Header("Car Settings")]
    [SerializeField] private float motorForce = 1000f;
    [SerializeField] private float brakeForce = 3000f;
    [SerializeField] private float maxSteerAngle = 30f;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [Header("Wheel Meshes")]
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [Header("Mobile Controls")]
    public VehiclePedal gasPedal;
    public VehiclePedal brakePedal;
    public VehiclePedal leftPedal;
    public VehiclePedal rightPedal;

    [Header("Engine Audio")]
    [SerializeField] private AudioSource idleAudio;
    [SerializeField] private AudioSource accelerateAudio;

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleEngineAudio();
    }

    void GetInput()
    {
        // ===========================
        // KEYBOARD (PC)
        // ===========================
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetKey(KeyCode.Space);

        // ===========================
        // MOBILE BUTTON
        // ===========================
        if (leftPedal != null && leftPedal.isPressed)
            horizontalInput = -1f;

        if (rightPedal != null && rightPedal.isPressed)
            horizontalInput = 1f;

        if (gasPedal != null && gasPedal.isPressed)
            verticalInput = 1f;

        if (brakePedal != null && brakePedal.isPressed)
        {
            float speed = GetComponent<Rigidbody>().linearVelocity.magnitude;

            if (speed > 1f)
            {
                isBraking = true;
                verticalInput = 0f;
            }
            else
            {
                isBraking = false;
                verticalInput = -1f;
            }
        }
    }

    void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = isBraking ? brakeForce : 0f;

        ApplyBrake();
    }

    void ApplyBrake()
    {
        frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
    void Start()
    {
        if (idleAudio != null)
        {
            idleAudio.loop = true;
            idleAudio.Play();
        }

        if (accelerateAudio != null)
        {
            accelerateAudio.loop = true;
            accelerateAudio.Play();

            // Awalnya tidak terdengar
            accelerateAudio.volume = 0f;
        }
    }
    void HandleEngineAudio()
    {
        bool gasPressed =
            (gasPedal != null && gasPedal.isPressed) ||
            Input.GetKey(KeyCode.W);

        if (gasPressed)
        {
            idleAudio.volume = Mathf.Lerp(idleAudio.volume, 0.2f, Time.deltaTime * 5f);
            accelerateAudio.volume = Mathf.Lerp(accelerateAudio.volume, 1f, Time.deltaTime * 5f);
        }
        else
        {
            idleAudio.volume = Mathf.Lerp(idleAudio.volume, 0.6f, Time.deltaTime * 5f);
            accelerateAudio.volume = Mathf.Lerp(accelerateAudio.volume, 0f, Time.deltaTime * 5f);
        }
    }
    public void StopEngineAudio()
    {
        if (idleAudio != null)
            idleAudio.Stop();

        if (accelerateAudio != null)
            accelerateAudio.Stop();
    }
}