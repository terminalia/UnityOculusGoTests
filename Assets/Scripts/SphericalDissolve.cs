using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalDissolve : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float scaling = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PassPlayerPosToShader();
        AnimateDissolveBurn();
    }

    private void PassPlayerPosToShader()
    {
        Vector4 playerPos = new Vector4(player.transform.position.x, player.transform.position.y, player.transform.position.z, 0);
        GetComponent<Renderer>().material.SetVector("_ActorPosition", playerPos);
    }

    private void AnimateDissolveBurn()
    {
        scaling += 0.001f;
        if (scaling > 1)
            scaling = 0.0f;
        GetComponent<Renderer>().material.SetTextureOffset("_DissolveTex", new Vector2(scaling, scaling));
    }
}
