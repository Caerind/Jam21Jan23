using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonController : MovementController
{
    protected void Awake()
    {
        Init();
    }

    protected void Update()
    {
        UpdateController();
    }

    protected override void UpdatePath()
    {
        if (!HasPath() && !IsComputingPath())
        {
            RequestPath(transform.position.ToVector2(), LabyrintheManager.Instance.endPoint.transform.position);
        }
    }

    protected void OnDrawGizmosSelected()
    {
        DrawGizmoPath();
    }

    public void Kill()
    {
        GameManager.Instance.peons.Remove(gameObject);
        GameObject.Destroy(gameObject);
    }
}