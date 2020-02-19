using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TestController : MonoBehaviour
{
    [SerializeField]
    private GameObject controllerObject;
    [SerializeField]
    private GameObject armModel;
    [SerializeField]
    private LineRenderer lineRenderer;

    private GameObject[] pickabledObjects;
    private OVRInput.Controller activeController;
    private Coroutine moveToRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        activeController = OVRInput.GetActiveController();
        pickabledObjects = GameObject.FindGameObjectsWithTag("Pickable");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosRotArm();
        UpdateLine();
        CheckInput();
    }

    private void UpdatePosRotArm()
    {
        armModel.transform.position = controllerObject.transform.position;
        armModel.transform.rotation = controllerObject.transform.rotation;
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, armModel.transform.position);
        lineRenderer.SetPosition(1, armModel.transform.position + armModel.transform.forward * 10);
    }

    private void ShowLine(bool isShown)
    {
        lineRenderer.enabled = isShown;
    }


    private void CheckInput()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            ShowLine(true);
            RayCast();
            Vector3 destination = controllerObject.transform.position + controllerObject.transform.forward * 10;
            destination = new Vector3(destination.x, transform.position.y, destination.z);
            StartMoveTo(destination, 2.0f);
        }
        else
        {
            ShowLine(false);
        }
    }

    private void RayCast()
    {
        Vector3 origin = controllerObject.transform.position;
        Vector3 controllerFwd = controllerObject.transform.forward;
        Ray ray = new Ray(origin, controllerFwd);
        RaycastHit hit;
        GameObject pickedObject = null;

        if (Physics.Raycast(ray, out hit))
        {
            pickedObject = hit.collider.gameObject;
            pickedObject.GetComponent<PickableObject>().ChangeColor(Color.red);

            //Vector3 destination = new Vector3(pickedObject.transform.position.x, transform.position.y, pickedObject.transform.position.z);
            //StartMoveTo(destination, 2.0f);

            foreach (GameObject child in pickabledObjects)
            {
                if (child.name != pickedObject.name)
                {
                    child.GetComponent<PickableObject>().ResetColor();
                }
            }
        }
    }

    private IEnumerator MoveTo(Vector3 target, float duration)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;
        Vector3 start = transform.position;

        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            float perc = Mathf.Clamp01(currentTime / duration);
            transform.position = Vector3.Lerp(start, target, perc);

            yield return null;
        }
    }

    public void StartMoveTo(Vector3 target, float duration)
    {
        if (moveToRoutine == null)
        {
            moveToRoutine = StartCoroutine(MoveTo(target, duration));
        }
        else
        {
            StopCoroutine(moveToRoutine);
            moveToRoutine = StartCoroutine(MoveTo(target, duration));
        }
    }
}
