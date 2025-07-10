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
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public Scanner scanner;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }
    

    private void FixedUpdate()
    {
        if(!GameManager.instance.isLive)
            return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        _animator.SetFloat("Speed",inputVec.magnitude);
        if (inputVec.x != 0)
        {
            _spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!GameManager.instance.isLive)
            return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            _animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
