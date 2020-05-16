using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject arrow;

    [Range(10f, 100f)]
    private float maxStrength = 20f;
    private float strength = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && strength < maxStrength)
        {
            strength += Time.deltaTime * 10;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Shoot(strength);
            strength = 1f;
        }   
    }

    private void Shoot(float impulse)
    {
        GameObject gameObject = Instantiate(arrow,transform.position, transform.rotation);
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * impulse, ForceMode.Impulse);
    }
}
