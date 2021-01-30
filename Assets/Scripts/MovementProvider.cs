using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : LocomotionProvider
{
    public float speed;
    public float sideSpeedMultiplier;
    public float gravityBelowSeaLevel;
    public float gravityAboveSeaLevel;
    public List<XRController> controllers = null;
    private CharacterController characterController = null;
    private GameObject head = null;

    protected override void Awake()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionController();
    }

    // Update is called once per frame
    void Update()
    {
        PositionController();
        CheckForInput();
        ApplyGravity();
    }

    private void PositionController()
    {
        //get the head height
        float headHeight = Mathf.Clamp(head.transform.localPosition.y,1,2);
        characterController.height = headHeight;

        //cut in half, add skin
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        //Move capsule in local space
        newCenter.x = head.transform.localPosition.x;
        newCenter.y = head.transform.localPosition.y;

        //apply
        characterController.center = newCenter;
    }

    private void CheckForInput(){
        foreach(XRController controller in controllers)
        {
            if(controller.enableInputActions){
                CheckForMovement(controller.inputDevice);
            }
        }
    }

    private void CheckForMovement(InputDevice device)
    {
        if(device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
        {
            StartMove(position);
        }
    }

    private void StartMove(Vector2 position)
    {
        
        if(Camera.main.transform.position.y < 0.5)
        {
            // below sea level
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward *= position.y;
            right *= position.x * sideSpeedMultiplier;

            Vector3 movement = (forward + right) * speed;

            characterController.Move(movement * Time.deltaTime);
        } else 
        {
            // Apply the touch position to the head's forward Vector
            Vector3 direction = new Vector3(position.x, 0, position.y);
            Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

            // Rotate the input direction by the horizontal head rotation
            direction = Quaternion.Euler(headRotation) * direction;

            // Apply speed and move
            Vector3 movement = direction * speed;
            characterController.Move(movement * Time.deltaTime);
        }
    }

    private void ApplyGravity(){

        Vector3 gravityVector;

        if(Camera.main.transform.position.y < 0.5)
        {
            gravityVector = new Vector3(0, Physics.gravity.y * gravityBelowSeaLevel, 0);
        } else {
            gravityVector = new Vector3(0, Physics.gravity.y * gravityAboveSeaLevel, 0);
        }
        
        gravityVector.y *= Time.deltaTime;

        characterController.Move(gravityVector * Time.deltaTime);
    }
}
