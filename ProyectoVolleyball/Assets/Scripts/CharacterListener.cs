using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListener : MonoBehaviour
{
    private CharacterController2D _character;


    private void Awake()
    {
        _character = GetComponentInParent<CharacterController2D>();
    }


    public void OnSpike()
    {
        _character.Spike();
    }
    public void OnReception() 
    { 
    
    }
    

}
