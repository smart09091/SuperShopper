using UnityEngine;

using CMF;
using SimpleInputNamespace;

namespace gotoandplay
{
    public class CharacterMobileInput : CharacterInput
    {
        // Joystick Input
        public Joystick mJoystick;
        public bool useJoystickInput;
        // End Joystick Input

        // Keyboard Input
        public string horizontalInputAxis = "Horizontal";
        public string verticalInputAxis = "Vertical";
        public KeyCode jumpKey = KeyCode.Space;
        public bool useRawInput = true;
        // End Keyboard Input

        void Start()
        {

        }

        public override float GetHorizontalMovementInput()
        {
            if (useJoystickInput && mJoystick != null)
            {
                return mJoystick.xAxis.value;
            }
            else
            {
                if (useRawInput)
                    return Input.GetAxisRaw(horizontalInputAxis);
                else
                    return Input.GetAxis(horizontalInputAxis);
            }
        }

        public override float GetVerticalMovementInput()
        {
            if (useJoystickInput && mJoystick != null)
            {
                return mJoystick.yAxis.value;
            }
            else
            {
                if (useRawInput)
                    return Input.GetAxisRaw(verticalInputAxis);
                else
                    return Input.GetAxis(verticalInputAxis);
            }
        }

        public override bool IsJumpKeyPressed()
        {
            return Input.GetKey(jumpKey);
        }
    }

}