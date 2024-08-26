using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController2D player1;
    [SerializeField] private CharacterController2D ally;

    private void Start()
    {
        player1._isActive = true;
        ally._isActive = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchActivePlayer();
        }
    }

    private void SwitchActivePlayer()
    {
        player1._isActive = !player1._isActive;
        ally._isActive = !ally._isActive;
    }
}
