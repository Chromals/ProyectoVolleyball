using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{


    private CharacterController2D _character;
    private float _attackTime;

    private void Awake()
    {
        _character = GetComponent<CharacterController2D>();
    }

    private void Update()
    {

        if (!_character._isGrounded && Input.GetKeyDown(KeyCode.R))
            _character.Spike();




    }

}
