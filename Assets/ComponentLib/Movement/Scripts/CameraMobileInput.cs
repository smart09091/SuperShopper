using UnityEngine;

using CMF;
using SimpleInputNamespace;

namespace gotoandplay
{
    public class CameraMobileInput : CameraInput
    {
        // Joystick Input
        public Joystick mJoystick;
        public bool useJoystickInput;
        // End Joystick Input

        //Mouse input axes;
        public string mouseHorizontalAxis = "Mouse X";
        public string mouseVerticalAxis = "Mouse Y";

        //Invert input options;
        public bool invertHorizontalInput = false;
        public bool invertVerticalInput = false;

        //Use this sensitivity setting to fine-tune mouse movement;
        //Mouse input will be multiplied by this value;
        public float mouseSensitivity = 1.0f;

        public override float GetHorizontalCameraInput()
        {
            if (useJoystickInput && mJoystick != null)
            {
                if (invertHorizontalInput)
                    return -mJoystick.xAxis.value * mouseSensitivity;
                else
                    return mJoystick.xAxis.value * mouseSensitivity;
            }
            else
            {
                //Return mouse input based on mouse sensitivity and invert input setting;
                if (invertHorizontalInput)
                    return -Input.GetAxisRaw(mouseHorizontalAxis) * mouseSensitivity;
                else
                    return Input.GetAxisRaw(mouseHorizontalAxis) * mouseSensitivity;
            }
        }

        public override float GetVerticalCameraInput()
        {
            if (useJoystickInput && mJoystick != null)
            {
                if (invertVerticalInput)
                    return -mJoystick.yAxis.value * mouseSensitivity;
                else
                    return mJoystick.yAxis.value * mouseSensitivity;
            }
            else
            {
                //Return mouse input based on mouse sensitivity and invert input setting;
                if (invertVerticalInput)
                    return Input.GetAxisRaw(mouseVerticalAxis) * mouseSensitivity;
                else
                    return -Input.GetAxisRaw(mouseVerticalAxis) * mouseSensitivity;
            }
        }
    }

}