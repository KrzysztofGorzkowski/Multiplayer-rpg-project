using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.Netcode;

public class tezztMovement : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;
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
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //getting movement from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Debug.Log(movement);
    }
    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {
            if (movement.x != 0 && movement.y != 0)   //limit the speed to 70% when the player is moving diagonally
            {
                _moveLimiter = 0.7f;
            }
            else
            {
                _moveLimiter = 1;
            }

            _rigidbody.MovePosition(_rigidbody.position + movement * _moveLimiter * PlayerDatabase.movementSpeed * Time.fixedDeltaTime);  //change the position
        }


    }
}
