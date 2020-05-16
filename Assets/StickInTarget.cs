using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickInTarget : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField, Range(0,30)] private float _stickSpeed = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("We hit " + collision.gameObject.name);
        if (((1 << collision.gameObject.layer ) & layer ) != 0)
        {
            if(rb.velocity.magnitude <= _stickSpeed)
            {
                rb.velocity *= -.5f;

                return;
            }
            rb.useGravity = false;
            Debug.Log("We hit the target");
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (rb.velocity.magnitude <= 0.1f)
            return;

        if(((1 << other.gameObject.layer) & layer ) != 0)
        {
           // StopArrowSlowly();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Velocity " + rb.velocity.magnitude);
        if (gameObject.GetComponent<Rigidbody>().isKinematic)
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
        }
    }
}
