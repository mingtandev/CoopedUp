﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekEnemy : BasicEnemy
{

    Animator _anim;
    const string a_PEEK = "Peek";
    const string a_DASH = "Dash";

    float posXLeft;
    float posXRight;

    float speed = 5f;

    private void Awake()
    {
        posXLeft = GameManager.Instance.camera.MiddleLeftPoint().x;
        posXRight = GameManager.Instance.camera.MiddleRightPoint().x;
        transform.position = new Vector2(posXRight, transform.position.y);
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Vector2 posRight =  new Vector2(posXRight, transform.position.y);
        transform.position = posRight;
        transform.localScale = new Vector2(1.2f, 1.2f);
        StartCoroutine(PeekEnemyAction());
    }

    new void Update()
    {

    }

    IEnumerator PeekEnemyAction()
    {
        //Wait for animation peek.
        _anim.Play(a_PEEK);
        yield return new WaitForSeconds(2.5f);
        _anim.Play(a_DASH);
        Vector2 desLeft = new Vector2(posXLeft, transform.position.y);
        Vector2 desRight = new Vector2(posXRight, transform.position.y);
        while (true)
        {

            if (transform.localScale.x > 0 && Vector2.Distance(transform.position, desLeft) < 0.1f)
                break;
            if (transform.localScale.x < 0 && Vector2.Distance(transform.position, desRight) < 0.1f)
                break;


            Vector2 direc = Vector2.zero;
            if (transform.localScale.x > 0)
            {
                direc = Vector2.left;
            }
            else
            {
                direc = Vector2.right;
            }
            transform.position += (Vector3)direc * speed * Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        StartCoroutine(PeekEnemyAction());
    }
}
