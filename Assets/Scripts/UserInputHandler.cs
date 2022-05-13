using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMobileGalaxyShooter
{
    public class UserInputHandler : MonoBehaviour
    {
        public delegate void TapAction(Touch touch);
        public static event TapAction OnTouchAction;           


        #region PUBLIC VARIABLES
        public float tapMaxMovement = 50f;  // Maximum pixel a tap can be move.
        #endregion

        #region PRIVATE VARIABLES 
        private Vector2 movement;   // To trach how far we move.
        private bool tapGestureFailed = false;  // Will become true, if tap moves to far.
        #endregion

        #region  MONOBEHAVIOUR METHODS 
        #endregion        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.touchCount > 0)    // To find out how many touches > 0 or not, if no touches do nothing.
            {
                Touch touch = Input.touches[0];   // Need to find out number of touches on screen. If there are more number touches, need to call
                if (touch.phase == TouchPhase.Began)   // We have a several touch phases began enters the first frame of the touch.
                {
                    movement = Vector2.zero;    // We made our movement to zero.
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    movement += touch.deltaPosition;    // The positon delta since last change in pixel coordinates.
                    if (movement.magnitude > tapMaxMovement)
                    {
                        tapGestureFailed = true;
                    }
                }
                else   // If finger is removed, then we are calling tap 
                {
                    if (!tapGestureFailed)
                    {
                        if (OnTouchAction != null)
                        {
                            OnTouchAction(touch);
                        }
                    }
                    tapGestureFailed = false;    // Making ready for the next tap.
                }
            }
        }

        #region PUBLIC METHODS

        #endregion
        #region PRIVATE METHODS

        #endregion
    }

}
