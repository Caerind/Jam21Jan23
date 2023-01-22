using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonController : MovementController
{

    //animation
    private Animator animator;
    private int Death;

    protected void Awake()
    {
        Init();
    }

    protected void Update()
    {
        UpdateController();
        //Verification de la distance avec le dernier point
        Vector2 delta = LabyrintheManager.Instance.endPoint.transform.position.ToVector2()- transform.position.ToVector2();
        
        if (delta.x < 1 && delta.y<1)
        {
            GameManager.Instance.PeloSauve++;
            Debug.Log("Un peon de sauvé en plus");
            Destroy(gameObject);
        }
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
        animator?.SetTrigger(Death);
        GameManager.Instance.peons.Remove(gameObject);
        GameObject.Destroy(gameObject);
    }
}