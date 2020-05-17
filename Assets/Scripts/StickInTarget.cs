using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class StickInTarget : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField, Range(0,30)] private float _stickSpeed = 15f;

    [SerializeField] bool _selfdestructCountdown = false;
    //[SerializeField, ] private float timeToLive = 5f;

    private bool _inCollision = false;
    private bool _arrowIsStuck = false;
    private bool _doNotStopTheArrow = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _inCollision = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _inCollision = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("We hit " + collision.gameObject.name);
        if (((1 << collision.gameObject.layer ) & layer ) != 0)
        {
            if(rb.velocity.magnitude <= _stickSpeed)
            {
                _doNotStopTheArrow = true;
                rb.velocity *= -.5f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!_arrowIsStuck && !_doNotStopTheArrow)
        {           
           StopArrowSlowly();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Velocity " + rb.velocity.magnitude);
        if (gameObject.GetComponent<Rigidbody>().isKinematic || _inCollision)
            return;
        transform.forward = Vector3.Slerp(transform.forward, gameObject.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);
    }

    private void StopArrowSlowly()
    {
        rb.velocity *= 0.01f;
        if (rb.velocity.magnitude <= 0.1f)
        {
            //rb.velocity *= 0f;
            rb.isKinematic = true;
            rb.Sleep();
            _arrowIsStuck = true;
        }
    }
}
