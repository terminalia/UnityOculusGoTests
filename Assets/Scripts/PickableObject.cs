using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void ResetColor()
    {
        GetComponent<MeshRenderer>().material.color = defaultColor;
    }
}
