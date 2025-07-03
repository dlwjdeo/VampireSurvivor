using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    private Rigidbody2D _rigidbody;
    public float speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
