using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Color defaultColor;
    private Coroutine moveToRoutine;

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

    private IEnumerator MoveTo(Vector3 target, float duration, System.Action callback)
    {
        float currentTime = 0.0f;
        float startTime = Time.time;
        Vector3 start = transform.position;

        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            float perc = Mathf.Clamp01(currentTime / duration);
            transform.position = Vector3.Lerp(start, target, perc);
            transform.rotation *= Quaternion.Euler(new Vector3(5.0f, 5.0f, 5.0f));

            yield return null;
        }

        if (callback != null)
            callback();
    }

    public void StartMoveTo(Vector3 target, float duration, System.Action callback)
    {
        if (moveToRoutine == null)
        {
            moveToRoutine = StartCoroutine(MoveTo(target, duration, callback));
        }
        else
        {
            StopCoroutine(moveToRoutine);
            moveToRoutine = StartCoroutine(MoveTo(target, duration, callback));
        }
    }

    public void FollowHand(GameObject hand)
    {
        transform.position = hand.transform.position;
        transform.rotation = hand.transform.rotation;
    }
}
