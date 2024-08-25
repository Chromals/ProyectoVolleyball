using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    [Header("Tracking")]
    [SerializeField]
    Transform ballTracking;
    [Header("Move")]
    [SerializeField]
    float jumpForce;

    private void Update()
    {
        AiController();
    }
    void AiController()
    {
        Vector3 tmp = ballTracking.position;

        if (tmp.x > 0 && Mathf.Abs(tmp.x - transform.position.x) > 0.3f)
        {
            if (tmp.x > transform.position.x)
            {
               // MoveRight();
            }
            else if (tmp.x < transform.position.x)
            {
               // MoveLeft();
            }

            if (Mathf.Abs(tmp.y - transform.position.y) > 1.5f &&
                Mathf.Abs(tmp.y - transform.position.y) < 4f && tmp.x < 1.75f)
            {
               // Jump();
            }
        }
        else if ((tmp.x > 0 && Mathf.Abs(tmp.x - transform.position.x) <= 0.3f) || (tmp.x <= 0 && tmp.x >= -0.6f))
        {
            //Stay();
        }
        else if (tmp.x < -0.6f)
        {
            if (transform.position.x < 3.75f)
            {
              //  MoveRight();
            }
            else if (transform.position.x > 4.1f)
            {
               // MoveLeft();
            }
            else
            {
              //  Stay();
            }
        }
        else
        {
           // Stay();
        }
    }

}
