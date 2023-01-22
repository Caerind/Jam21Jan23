using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MovementController
{
    public int forceRecomputeEveryXFrame = 30;
    public float eatDistance = 1.0f;



    private int forceRecomputeCounter = 0;


    protected void Awake()
    {
        Init();
    }

    protected void Update()
    {
        forceRecomputeCounter++;
        if (forceRecomputeCounter >= forceRecomputeEveryXFrame)
        {
            ResetPath();
        }

        UpdateController();

        var gm = GameManager.Instance;
        for (int i = 0; i < gm.peons.Count; ++i)
        {
            Vector2 pos = gm.peons[i].transform.position;
            float sqrD = (transform.position.ToVector2() - pos).sqrMagnitude;
            if (sqrD < eatDistance)
            {
                gm.peons[i].GetComponent<PeonController>().Kill();
            }
        }
    }

    protected override void UpdatePath()
    {
        if (!HasPath() && !IsComputingPath())
        {
            Vector2 bestPeonPos = Vector2.zero;
            float bestPeonSqrDistance = 999999f;
            bool found = false;
            var gm = GameManager.Instance;
            for (int i = 0; i < gm.peons.Count; ++i)
            {
                Vector2 pos = gm.peons[i].transform.position;
                float sqrD = (transform.position.ToVector2() - pos).sqrMagnitude;
                if (sqrD < bestPeonSqrDistance)
                {
                    found = true;
                    bestPeonPos = pos;
                    bestPeonSqrDistance = sqrD;
                }
            }

            if (found)
            {
                RequestPath(transform.position.ToVector2(), bestPeonPos);
            }
        }
    }

    protected void OnDrawGizmosSelected()
    {
        DrawGizmoPath();
    }
}