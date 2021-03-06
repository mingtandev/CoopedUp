﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : BasicEnemy
{

    BasicEnemy.EnemyState curState;
    const string a_turn = "Turn";
    float originY;
    new void Awake()
    {
        curState = state;
        base.Awake();
        originY = transform.localPosition.y;
    }
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Movement();
        base.ApplyConstraint();

        transform.localPosition = new Vector3(transform.localPosition.x, originY);
    }

    private void LateUpdate()
    {
        CheckTurn();
    }

    void CheckTurn()
    {
        if(state!=curState)
        {
            StartCoroutine(TurnAction());
            curState = state;

        }

    }

    IEnumerator TurnAction()
    {
        float t = 0;
        float duration = 1f;
        Vector3 curPos = transform.localPosition;
        anim.SetTrigger(a_turn);

        while (t < duration)
        {
            transform.localPosition = curPos;
            t += Time.deltaTime;
            yield return null;
        }
        anim.ResetTrigger(a_turn);

    }

}
