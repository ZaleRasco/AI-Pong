using UnityEngine;
using System.Collections;

public class MoveRacket : MonoBehaviour
{
    float pspeed = 30;
    public string vaxis = "Vertical";
    int difficulty = 1;
    bool controlled = false;

    void FixedUpdate()
    {
        //Useful stuff
        Rigidbody2D ballobj = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
        Rigidbody2D me = GetComponent<Rigidbody2D>();
        float wallHeight = 16.5f;
        int firstWall = 0;
        float expectedY = 0;
        float myspeed = 0;

        //Player Input
        float vinput = Input.GetAxisRaw(vaxis);
        me.velocity = new Vector2(me.velocity.x, vinput * pspeed);

        //Set Control State
        if (vinput != 0)
        {
            controlled = true;
        }

        //Easy Difficulty
        if (difficulty == 0 && !controlled)
        {
            float difference = me.position.y - ballobj.position.y;

            if (difference < -0.01)
            {
                me.velocity = new Vector2(me.velocity.x, -difference * 50 - 10);
            }
            else if (difference > 0.01)
            {
                me.velocity = new Vector2(me.velocity.x, -difference * 50 + 10);
            }
        }

        //Hard Difficulty
        if (difficulty == 1 && !controlled)
        {
            Vector2 collisionPoint = ballobj.GetComponent<BallMove>().colcoords;

            //Find where the ball would end up if unimpeded
            float dTheory = (ballobj.GetComponent<BallMove>().colvelocity.y / ballobj.GetComponent<BallMove>().colvelocity.x) *
                -(Mathf.Abs(me.position.x - ballobj.GetComponent<BallMove>().colcoords.x)) + collisionPoint.y;
            Debug.Log("*** dTheory: " + dTheory + " ***");

            if (dTheory > wallHeight || dTheory < -wallHeight)
            {
                //Find first impact
                firstWall = dTheory > 0 ? 1 : -1;

                //Find true impact
                float dVertical = dTheory - collisionPoint.y;

                //Distance from the first impact wall to racket
                float dWalltoRacket = (wallHeight * firstWall) - collisionPoint.y;

                //Distance from the last wall the ball hits towards the center
                float dFromLastWall = ((dVertical - dWalltoRacket) % 33) * firstWall;

                //Exact Y coordinates of where the ball will end up
                expectedY = (wallHeight * firstWall) - dFromLastWall * firstWall;

                //Fun info
                Debug.Log("firstWall: " + firstWall);
                Debug.Log("racketPos: " + collisionPoint.y);
                Debug.Log("dWalltoRacket: " + dWalltoRacket);
                Debug.Log("dFromLastWall: " + dFromLastWall);
                Debug.Log("*** expectedY: " + expectedY + " ***");

            }

            else
            {
                expectedY = dTheory;
            }

            float calcDiff = me.position.y - expectedY;
            if (calcDiff < 0.001)
            {
                myspeed = Mathf.Min(30, (-calcDiff * 5 + 3));
                me.velocity = new Vector2(me.velocity.x, myspeed);
            }
            if (calcDiff > 0.001)
            {
                myspeed = Mathf.Max(-30, (-calcDiff * 10 - 3));
                me.velocity = new Vector2(me.velocity.x, myspeed);
            }
        }
    }
}



