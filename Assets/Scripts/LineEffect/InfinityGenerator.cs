﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> listRope = new List<Transform>();
    private float lastPosY;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        lastPosY = listRope[listRope.Count - 1].transform.position.y;

    }

    void Update()
    {
        for (int i = 0; i < listRope.Count; i++)
        {
            if (listRope[i].transform.position.y < cam.BottomMiddlePoint().y)
            {
                listRope[i].transform.position = new Vector3(listRope[i].transform.position.x, lastPosY + 2.5f);
                lastPosY += 2.5f;
            }
        }
    }
}
