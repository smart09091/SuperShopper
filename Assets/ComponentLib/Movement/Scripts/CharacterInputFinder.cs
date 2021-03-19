using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleInputNamespace;

namespace gotoandplay
{
    public class CharacterInputFinder : MonoBehaviour
    {
        public bool mapCharacterInput;
        public bool mapCameraInput;

        JoystickInput joystickInput;
        void Start()
        {
            Init();
        }

        void Init()
        {
            joystickInput = FindObjectOfType<JoystickInput>();

            // map character input to movement joystick
            if (mapCharacterInput)
            {
                var characterInput = GetComponent<CharacterMobileInput>();
                if (joystickInput && joystickInput.leftJoystick)
                {
                    characterInput.mJoystick = joystickInput.leftJoystick.GetComponent<Joystick>();
                }
            }

            // map camera input to lookaround joystick
            if(mapCameraInput)
            {
                if(joystickInput && joystickInput.rightJoystick)
                {
                    var cameraInput = GetComponentInChildren<CameraMobileInput>();
                    cameraInput.mJoystick = joystickInput.rightJoystick.GetComponent<Joystick>();
                }
            }
        }
    }
}
