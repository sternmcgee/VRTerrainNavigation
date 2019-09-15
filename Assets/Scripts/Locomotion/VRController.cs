using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float gravity = 30.0f;
    public float sensitivity = 0.1f;
    public float maxSpeed = 1.0f;
    public float rotateIncrement = 45.0f;

    public SteamVR_Action_Boolean rotatePress = null;
    public SteamVR_Action_Boolean movePress = null;
    public SteamVR_Action_Vector2 moveValue = null;

    [SerializeField] private float speed = 0.0f;

    private CharacterController characterController = null;
    private Transform cameraRig = null;
    private Transform head = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapRotation();
    }

    private void HandleHeight()
    {
        // get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        // calculate capsule height
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;
        newCenter.y += cameraRig.localPosition.y;

        // move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        // apply
        characterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        //return if move actions are null
        if (movePress == null || moveValue == null)
            return;

        // determine movement orientation
        Vector3 orientationEuler = new Vector3(0.0f, head.eulerAngles.y, 0.0f);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // if not moving
        if (movePress.GetStateUp(SteamVR_Input_Sources.Any))
            speed = 0;
               
        // if button pressed
        if (movePress.GetState(SteamVR_Input_Sources.Any))
        {
            // add and clamp
            speed += moveValue.GetAxis(SteamVR_Input_Sources.Any).y * sensitivity;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            // orientation
            movement += orientation * (speed * Vector3.forward);
        }

        // gravity
        movement.y -= gravity * Time.deltaTime;

        // apply
        characterController.Move(movement * Time.deltaTime);
    }    

    private void SnapRotation()
    {
        //return if snap action is null
        if (rotatePress == null)
            return;

        float snapValue = 0.0f;

        // handle left hand
        if (rotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            snapValue = -Mathf.Abs(rotateIncrement);
        }
        // handle right hand
        if (rotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            snapValue = Mathf.Abs(rotateIncrement);
        }

        //calculate
        transform.RotateAround(head.position, Vector3.up, snapValue);
    }
}
