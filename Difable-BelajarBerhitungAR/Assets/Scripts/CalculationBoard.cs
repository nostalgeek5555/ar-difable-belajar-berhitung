using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Lean.Pool;
using System;

public class CalculationBoard : MonoBehaviour
{
    [Header("Grid Properties")]
    public Transform board;
    public GameObject calculationObject;
    public int rows;
    public int cols;
    public int tileSize;
    [ReadOnly] public Vector3[] vertices;
    

    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        vertices = new Vector3[rows * cols];
        int initPosY = (int)Math.Round(board.transform.position.y);
        int initPosX = (int)Math.Round(board.transform.position.x);

        for (int i = 0, y = initPosY; y <= cols; y++)
        {
            for (int x = initPosX; x <= rows; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                GameObject levelObject = LeanPool.Spawn(calculationObject, new Vector3(vertices[i].x, vertices[i].y, vertices[i].z), Quaternion.identity);
                levelObject.transform.parent = board.transform;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        Gizmos.color = Color.blue;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
