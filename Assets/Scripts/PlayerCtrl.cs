using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private Text debugText;
    [SerializeField]
    private GameObject headAnchor;
    [SerializeField]
    private GameObject handAnchor;
    [SerializeField]
    private float projectileSpeed = 20.0f;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float lineLength = 10.0f;
   
    private GameObject projectilePrefab;
    private GameObject pickedObject;
    private bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        projectilePrefab = Resources.Load("projectile") as GameObject;    
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        UpdateLine();
        PointObject();
    }

    private void Shoot()
    {
        //Shoot sphere projectiles
        if (!isGrabbing)
        {
            GameObject projectile = Instantiate(projectilePrefab) as GameObject;
            projectile.transform.position = handAnchor.transform.position + handAnchor.transform.forward;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = handAnchor.transform.forward * projectileSpeed;
        }
        //Shoot current grabbed object
        else
        {
            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.WakeUp();
            rb.velocity = handAnchor.transform.forward * projectileSpeed;
            pickedObject.transform.parent = null;
            pickedObject = null;
            isGrabbing = false;

        }
    }

    private void CheckInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Shoot();
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GrabObject();
        }
    }

    private void PointObject()
    {
        Ray ray = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            pickedObject = hit.collider.gameObject;
            //debugText.text = "Picked: " + pickedObject.name;
            if (pickedObject.tag == "Pickable")
            {
                pickedObject.GetComponent<PickableObject>().ChangeColor(Color.red);
                GameObject[] pickableObjects = GameObject.FindGameObjectsWithTag("Pickable");
                foreach (GameObject child in pickableObjects)
                {
                    if (child.name != pickedObject.name)
                    {
                        child.GetComponent<PickableObject>().ResetColor();
                    }
                }
            }
            else
            {
                pickedObject = null;
            }

        }
    }

    private void GrabObject()
    {
        if (pickedObject != null)
        {
            debugText.text = "Picked: " + pickedObject.name;
            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            pickedObject.GetComponent<PickableObject>().StartMoveTo(handAnchor.transform.position + handAnchor.transform.forward, .2f, ParentPickedObject);

        }
        else
        {
            debugText.text = "No object ready!";
        }
    }

    private void ParentPickedObject()
    {
        pickedObject.transform.parent = handAnchor.transform;
        isGrabbing = true;
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, handAnchor.transform.position);
        lineRenderer.SetPosition(1, handAnchor.transform.position + handAnchor.transform.forward * lineLength);
    }

}
