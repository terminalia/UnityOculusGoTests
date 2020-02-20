using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroySelf()
    {
        float startTime = Time.time;
        float elapsedTime = 0.0f;

        while (elapsedTime <= lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Object Destroyed!");
        Destroy(gameObject);
    }

}
