using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    private Animator anim;

    public bool isLive;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    WaitForFixedUpdate wait;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;
        Vector2 dirVec = target.position - _rigidbody.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + nextVec);
        _rigidbody.velocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (!isLive) return;
        _spriteRenderer.flipX = target.position.x < _rigidbody.position.x;
    }

    private void OnEnable()//스크립트가 활성화 될 때 불러오는 함수
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        _collider.enabled = true;
        _rigidbody.simulated = true;
        _spriteRenderer.sortingOrder = 100;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData spawnData)
    {
        anim.runtimeAnimatorController = animCon[spawnData.spriteType];
        speed = spawnData.speed;
        maxHealth = spawnData.health;
        health = spawnData.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if(health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            _collider.enabled = false;
            _rigidbody.simulated = false;
            _spriteRenderer.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            StartCoroutine(DeadTime());
        }
        
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        _rigidbody.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
    IEnumerator DeadTime()
    {
        yield return new WaitForSeconds(1);
        Dead();
    }
}
