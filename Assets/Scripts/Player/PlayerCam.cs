using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerCam : NetworkBehaviour
{

    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        //change the position of the camera
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.9f);
        this.transform.position = this.transform.position + new Vector3(0f, 0f, -10f);
    }

    public void SetTarget(NetworkObject target)
    {
        this.target = target.transform;   //get the player object as target
    }
}
