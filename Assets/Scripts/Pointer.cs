using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 10.0f;
    private LineRenderer lineRenderer = null;

    private Transform currentOrigin = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 UpdateLine()
    {
        OVRInput.Controller controller = OVRInput.GetActiveController();
    
        Vector3 endPos = currentOrigin.position + currentOrigin.forward * maxDistance;
        lineRenderer.SetPosition(0, currentOrigin.position);
        lineRenderer.SetPosition(1, endPos);

        return endPos;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {

    }
}
