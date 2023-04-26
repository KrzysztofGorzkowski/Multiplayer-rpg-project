using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCam : NetworkBehaviour
{

    [SerializeField] Transform target;
    private void Start()
    {
        if (!IsOwner) GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Test")
        {
            //change the position of the camera
            this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.9f);
            this.transform.position = this.transform.position + new Vector3(0f, 0f, -10f);
        }
        
    }

    public void SetTarget(Transform target)
    {
        this.target = target;   //get the player object as target
    }
}
