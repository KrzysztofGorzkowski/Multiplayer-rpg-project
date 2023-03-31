using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        //change the position of the camera
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.9f);
        this.transform.position = this.transform.position + new Vector3(0f, 0f, -10f);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target.transform;   //get the player object as target
    }
}
