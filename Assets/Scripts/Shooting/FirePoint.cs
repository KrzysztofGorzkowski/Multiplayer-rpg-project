using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FirePoint : NetworkBehaviour
{
    public Rigidbody2D rb;
    private Vector2 _mousePos;
    private Vector2 _direction;
    private float _angle;
    public Camera cam;

    private void Start()
    {
        cam = GameManager_OLD.GetCamera().GetComponent<Camera>();  //getting camera !!wazne
    }

    // Update is called once per frame
    void Update()
    {
        
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePos - new Vector2(transform.position.x,transform.position.y);
        _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion target = Quaternion.Euler(0, 0, _angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, 1);
    }
}
