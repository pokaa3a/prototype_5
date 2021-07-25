using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputDirection
{
    None, North, South, West, East
}

public class InputHandler : MonoBehaviour
{
    public static Vector2 position;
    public Text inputDebugText;
    public GameObject handleObject;
    public GameObject northEdge;
    public GameObject southEdge;
    public GameObject eastEdge;
    public GameObject westEdge;

    private static InputDirection _inputDirection = InputDirection.None;
    public static InputDirection inputDirection 
    { 
        get => _inputDirection; 
        private set => _inputDirection = value; 
    }
    private static InputDirection _prevDirection = InputDirection.None;
    public static InputDirection prevDirection
    {
        get => _prevDirection;
        private set => _prevDirection = value;
    }

    public static float inputAngle = 0f;

    private Vector2 initNorthEdgePosition;
    private Vector2 initJoystickPosition;
    private float joystickRange = 200f;

    //----------------------------------------------------------------------------------------------
    void Awake()
    {
        initJoystickPosition = handleObject.transform.position;
        initNorthEdgePosition = northEdge.transform.position;
    }

    //----------------------------------------------------------------------------------------------
    void Start() 
    {

    }

    //------------------------------------------------------------------------------------------
    void Update()
    {
        prevDirection = inputDirection;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            position = touch.position;

            if (Vector2.Distance(position, initJoystickPosition) < joystickRange)
            {
                // Inside range
                handleObject.transform.position = position;
                _inputDirection = InputDirection.None;
            }
            else
            {
                // Outside range
                handleObject.transform.position = initJoystickPosition + 
                    (position - initJoystickPosition).normalized * joystickRange;
                _inputDirection = ComputeInputDirection(position);
            }

            inputAngle = 
                Vector2.SignedAngle(position - initJoystickPosition, Vector2.up);
        }
        else
        {
            handleObject.transform.position = initJoystickPosition;
            _inputDirection = InputDirection.None;
        
            inputAngle = 0f;
        }

        if (inputDirection == InputDirection.North)
        {
            float angle = Vector2.SignedAngle(position - initJoystickPosition, Vector2.up);
            Vector2 newNorthEdgePosition = new Vector2(
                Mathf.Sin((angle - inputAngle) * Mathf.Deg2Rad),
                Mathf.Cos((angle - inputAngle) * Mathf.Deg2Rad)) * joystickRange;

            northEdge.transform.localPosition = newNorthEdgePosition;
            northEdge.transform.eulerAngles = new Vector3(0, 0, inputAngle - angle);
        }
        else
        {
            northEdge.transform.position = initNorthEdgePosition;
            northEdge.transform.eulerAngles = Vector3.zero;
        }

        inputDebugText.text = $"Input: {inputDirection}";

        SetPressed(inputDirection);
    }

    //------------------------------------------------------------------------------------------
    InputDirection ComputeInputDirection(Vector2 position)
    {
        Vector2 vec = position - initJoystickPosition;
        float angle = Vector2.SignedAngle(vec, Vector2.up);

        // North
        if (angle > -45f && angle <= 45f)
        {
            return InputDirection.North;
        }
        // East
        else if (angle > 45f && angle <= 135f)
        {
            return InputDirection.East;
        }
        // South
        else if (angle > 135f || angle <= -135f)
        {
            return InputDirection.South;
        }
        // West
        else if (angle > -135f && angle <= -45f)
        {
            return InputDirection.West;
        }
        return InputDirection.None;
    }

    //------------------------------------------------------------------------------------------
    void SetPressed(InputDirection direction)
    {
        northEdge.transform.Find("pressed").gameObject.SetActive(false);
        northEdge.transform.Find("unpressed").gameObject.SetActive(true);
        southEdge.transform.Find("pressed").gameObject.SetActive(false);
        southEdge.transform.Find("unpressed").gameObject.SetActive(true);
        eastEdge.transform.Find("pressed").gameObject.SetActive(false);
        eastEdge.transform.Find("unpressed").gameObject.SetActive(true);
        westEdge.transform.Find("pressed").gameObject.SetActive(false);
        westEdge.transform.Find("unpressed").gameObject.SetActive(true);
        
        if (direction == InputDirection.None) return;

        GameObject obj = null;
        if (direction == InputDirection.North)
        {
            obj = northEdge;
        }
        else if (direction == InputDirection.South)
        {
            obj = southEdge;
        }
        else if (direction == InputDirection.East)
        {
            obj = eastEdge;
        }
        else if (direction == InputDirection.West)
        {
            obj = westEdge;
        }

        if (obj == null) return;
        Transform pressed = obj.transform.Find("pressed");
        Transform unpressed = obj.transform.Find("unpressed");
        pressed.gameObject.SetActive(true);
        unpressed.gameObject.SetActive(false);
    }
}
