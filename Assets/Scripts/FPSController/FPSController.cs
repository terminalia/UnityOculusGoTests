using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    private float xDisplacement = 0.0f;
    private float yDisplacement = 0.0f;
    private float increment = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            yDisplacement += increment;
        }

        if (Input.GetKey(KeyCode.S))
        {
            yDisplacement -= increment;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xDisplacement -= increment;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xDisplacement += increment;
        }


        Vector3 destination = new Vector3(xDisplacement, transform.position.y, yDisplacement);
        transform.position = destination;
    }
}
