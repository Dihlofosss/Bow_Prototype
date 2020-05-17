using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesctruct : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 5f;
    private void Selfdestruct()
    {
        Object.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (timeToLive <= 0)
        {
            Selfdestruct();
        }
        else
            timeToLive -= Time.deltaTime;
    }
}
