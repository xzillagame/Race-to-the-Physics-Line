using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour{

// y = a * Sin (b (x - c)) + d

    [SerializeField] private bool xSine = false;
    [SerializeField] private bool ySine = false;
    [SerializeField] private bool zSine = false;

    //[SerializeField] private float frequency = 1; // Frequency of the sine wave
    //[SerializeField] private float amplitude = 1; // Amplitude of the sine wave
    //[SerializeField] private float speed = 1; // Speed of the sine wave
    //[SerializeField] private float offset = 0; // Offset of the sine wave
    [SerializeField] Rigidbody rb;


    [SerializeField] private float a_Amplitude = 1; // Amplitude of the sine wave

    [SerializeField] private float b_frequency = 1;
    private const float b_Period = 2 * (float)Math.PI;// Speed of the sine wave
    [SerializeField] private float c_PhaseShift = 1;
    [SerializeField] private float d_VerticalShift = 1;

    // Update is called once per frame
    void Update(){
        if(xSine == true){
            float _sinPos =  a_Amplitude * (float)Math.Sin(((b_Period/b_frequency) * (Time.fixedDeltaTime - c_PhaseShift)) + d_VerticalShift);
            transform.position= new Vector3(transform.position.x - _sinPos,transform.position.y,transform.position.z);
            //transform.position= new Vector3(transform.position.x - (float)Math.Sin(Time.deltaTime),transform.position.y,transform.position.z);

            //rb.AddForce(_sinPos, 0, 0, ForceMode.Acceleration);
        }
    }
}
