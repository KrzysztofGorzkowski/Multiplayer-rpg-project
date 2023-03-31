using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 _mousePos;
    private Vector2 _direction;
    private float _angle;
    public Camera cam;

    private void Start()
    {
        cam = GameManager.GetCamera().GetComponent<Camera>();  //getting camera
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
