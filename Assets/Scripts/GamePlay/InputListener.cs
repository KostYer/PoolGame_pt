using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private InputEventHubSO inputEvents = default;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private float step = 111f;

    public float swipeTreshhold =200;



    void Update()
    {
        //float hor = Input.GetAxis("Horizontal");
        //inputEvents.Slide(hor);

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    inputEvents.Fire();
        //}


        foreach (Touch touch in Input.touches)
        {
             
            if (touch.phase == TouchPhase.Began)
            {

                fingerUp = touch.position;
                fingerDown = touch.position;
            }


            //    //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                
                fingerDown = touch.position;
                checkSwipe();

            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                //fingerDown = touch.position;
                //checkSwipe();
                inputEvents.Fire();
            }




            void checkSwipe()
            {
                
                //Check if Horizontal swipe
                if ( horizontalValMove() > swipeTreshhold )
                {
                    float magnutude = fingerDown.x - fingerUp.x;
                 

                    if (magnutude > 0)
                    {
                         
                        inputEvents.Slide(-step);
                    }

                    if (magnutude < 0)
                    {

                        inputEvents.Slide(step);
                    }

                }


            }





            float horizontalValMove()
            {
                return Mathf.Abs(fingerDown.x - fingerUp.x);
            }





        }
    }
}
 