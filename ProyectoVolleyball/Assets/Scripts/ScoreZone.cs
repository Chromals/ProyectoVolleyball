using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour
{
    public bool isPlayer1Zone;
    public Transform startPointP1;
    public Transform startPointP2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayer1Zone)
            {
                ScoreManager.instance.AddPointsP2();
                RespawnBall(collision.gameObject, startPointP2);
            }
            else
            {
                ScoreManager.instance.AddPointsP1();
                RespawnBall(collision.gameObject, startPointP1);
            }
        }
    }

    private void RespawnBall(GameObject ball, Transform startPoint)
    {
        
        ball.transform.position = startPoint.position;
        Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();
        if (ballRigidbody != null)
        {
            ballRigidbody.velocity = Vector2.zero; 
            ballRigidbody.angularVelocity = 0f; 
            ballRigidbody.gravityScale = 0; 
        }

        
        StartCoroutine(AllowBallToFall(ballRigidbody, 2f));
    }

    private IEnumerator AllowBallToFall(Rigidbody2D ballRigidbody, float delay)
    {
       
        yield return new WaitForSeconds(delay);

       
        if (ballRigidbody != null)
        {
            ballRigidbody.gravityScale = 1; 
        }
    }
}