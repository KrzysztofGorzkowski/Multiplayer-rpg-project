using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public Transform spawnObject;
    public GameObject firePoint;

    private Rigidbody2D _rigidbody;
    private static GameObject _camera;

    public enum Direction
    {
        UP = 0, DOWN, LEFT, RIGHT
    }

    private float _moveLimiter;

    [SerializeField]
    private Vector2 movement;

    //possible directions
    private Vector2 upDirection = new Vector2(0.0f, 1.0f);
    private Vector2 downDirection = new Vector2(0.0f, -1.0f);
    private Vector2 rightDirection = new Vector2(1.0f, 0.0f);
    private Vector2 leftDirection = new Vector2(-1.0f, 0.0f);
    private Vector2 upRightDirection = new Vector2(1.0f, 1.0f);
    private Vector2 downRightDirection = new Vector2(1.0f, -1.0f);
    private Vector2 upLeftDirection = new Vector2(-1.0f, 1.0f);
    private Vector2 downLefttDirection = new Vector2(-1.0f, -1.0f);

    //firepoint positions
    private Vector3 upFirePoint = new Vector3(0.13f, 0.166f, 0);
    private Vector3 downFirePoint = new Vector3(-0.113f, 0.011f, 0);
    private Vector3 rightFirePoint = new Vector3(0.183f, 0.093f, 0);
    private Vector3 leftFirePoint = new Vector3(-0.183f, 0.093f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        _rigidbody = GetComponent<Rigidbody2D>();

        transform.position = new Vector3(-3, -3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!IsOwner) return;
            Transform var = Instantiate(spawnObject);

            var.transform.SetPositionAndRotation(new Vector3(0, 0, 0), new Quaternion());
            var.GetComponent<NetworkObject>().Spawn(true);
        }

        //getting movement from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if (!IsOwner) return;
        if (movement.x!=0 && movement.y != 0)   //limit the speed to 70% when the player is moving diagonally
        {
            _moveLimiter = 0.7f;
        }
        else
        {
            _moveLimiter = 1;
        }
        
        _rigidbody.MovePosition(_rigidbody.position + movement * _moveLimiter * PlayerDatabase.movementSpeed * Time.fixedDeltaTime);  //change the position
        
        if (movement == upDirection || movement == upRightDirection || movement == upLeftDirection)
        {
            firePoint.transform.position = transform.position + upFirePoint;
        }
        else if (movement == leftDirection)
        {
            firePoint.transform.position = transform.position + leftFirePoint;
        }
        else if (movement == rightDirection)
        {
            firePoint.transform.position = transform.position + rightFirePoint;
        }
        else if (movement == downDirection || movement == downRightDirection || movement == downLefttDirection)
        {
            firePoint.transform.position = transform.position + downFirePoint;
        }

    }
   
}
