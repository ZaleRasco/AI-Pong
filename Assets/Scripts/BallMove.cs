using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour
{
    public float speed = 25;
    public Vector2 colcoords = new Vector2(0, 0);
    public Vector2 colvelocity = new Vector2(0, 0);
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, Random.value - 0.5f) * speed;
        colvelocity = GetComponent<Rigidbody2D>().velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Reset after hitting leftWall
        if (col.gameObject.name == "wallLeft")
        {
            GetComponent<Rigidbody2D>().position = new Vector2(0, 0);
            StartCoroutine(WaitAfter(-1));
        }
        //Reset after hitting rightWall
        else if (col.gameObject.name == "wallRight")
        {
            GetComponent<Rigidbody2D>().position = new Vector2(0, 0);
            StartCoroutine(WaitAfter(1));
        }
        //Getcoords after hitting RacketLeft
        if (col.gameObject.name == "racketLeft")
        {
            colcoords = GetComponent<Rigidbody2D>().position;
            Vector2 yforce = col.gameObject.GetComponent<Rigidbody2D>().velocity / 2;
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + yforce;
            colvelocity = GetComponent<Rigidbody2D>().velocity;
        }
        //Getcoords after hitting RacketRight
        if (col.gameObject.name == "racketRight")
        {
            colcoords = GetComponent<Rigidbody2D>().position;
            Vector2 yforce = col.gameObject.GetComponent<Rigidbody2D>().velocity / 2;
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + yforce;
            colvelocity = GetComponent<Rigidbody2D>().velocity;
        }
    }

    IEnumerator WaitAfter(float red)
    {
        Vector2 dir = new Vector2(1, Random.value - 0.5f);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return new WaitForSecondsRealtime(1);
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}