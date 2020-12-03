﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour, CollisionWithRope
{
    [System.Serializable]
    public struct DictLine
    {
        public int index;
        public Transform trans;
    }

    // Start is called before the first frame update
    public GameObject LinePrefab;
    public GameObject parentLeft;
    public GameObject parentRight;

    [SerializeField] int pieceOfLine;
    [SerializeField] DictLine[] dictPieceOFLine;

    Vector2 pos;
    public int currentRopeID;

    void GenerateLine()
    {
        pos = Vector2.zero;
        dictPieceOFLine = new DictLine[pieceOfLine];
        for (int i = 0; i < pieceOfLine; i++)
        {

            GameObject line = Instantiate(LinePrefab, parentLeft.transform);
            line.transform.localPosition = pos;
            pos = new Vector2(pos.x + 0.1f, pos.y);
            if (i > pieceOfLine / 2)
            {
                line.transform.SetParent(parentRight.transform);
            }
            dictPieceOFLine[i].index = i;
            dictPieceOFLine[i].trans = line.transform;
            line.GetComponent<LineElement>().id = i;
        }
    }

    private void Awake()
    {
        GenerateLine();
    }

    void ResetParent(int temp)
    {
        for (int i = 0; i < temp; i++)
        {
            Transform line = dictPieceOFLine[i].trans;
            line.SetParent(parentLeft.transform);
        }
        for (int i = temp; i < pieceOfLine; i++)
        {
            Transform line = dictPieceOFLine[i].trans;
            line.SetParent(parentRight.transform);
        }

    }

    public void SetupRopeDisplay()
    {

        Debug.Log(currentRopeID);
        for (int i = 0; i < dictPieceOFLine.Length; i++)
        {
            if (dictPieceOFLine[i].index == currentRopeID)
            {
                ResetParent(dictPieceOFLine[i].index);
                return;
            }
        }
    }


    float CalcAngleOfRightRope()
    {
        float angleRight;

        //Tim vector direc tu node root cho den diem giao
        Vector2 direc = dictPieceOFLine[currentRopeID - 1].trans.position - parentRight.transform.position;
        angleRight = Vector2.Angle(Vector2.left, direc);
        return angleRight;
    }

    public void RopeUpdate(float distance)
    {
        float angleLeft = distance * 2;
        float angleRight = CalcAngleOfRightRope();
        if (distance <= 0.1f)
        {
            angleLeft = 0;
        }
        parentLeft.transform.rotation = Quaternion.Lerp(parentLeft.transform.rotation, Quaternion.Euler(parentLeft.transform.rotation.x, parentLeft.transform.rotation.y, -angleLeft), 0.15f);
        parentRight.transform.rotation = Quaternion.Lerp(parentRight.transform.rotation, Quaternion.Euler(parentRight.transform.rotation.x, parentRight.transform.rotation.y, angleRight), 1f);

    }

    public IEnumerator RopeReset()
    {
        yield return new WaitForSeconds(0.05f);
        parentLeft.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        parentRight.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

    }




}
