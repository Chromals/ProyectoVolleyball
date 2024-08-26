using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField, Tooltip("Range Interval for attacks")]
    float attackRange;

    [SerializeField, Tooltip("Amount of attacks per Range")]
    int attackRate;

    private CharacterController2D _character;
    private float _attackTime;

    private void Awake()
    {
        _character = GetComponent<CharacterController2D>();
    }

    private void Update()
    {
        _attackTime -= Time.deltaTime;
        if (_attackTime < 0.0F)
            _attackTime = 0.0F;

        if (_attackTime == 0.0F)
        {
            if (Input.GetKeyDown(KeyCode.R))
                _character.Spike();
            
        }


    }

}
