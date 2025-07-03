using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    private Rigidbody2D _rigidbody;
    public float speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        
    }
}
