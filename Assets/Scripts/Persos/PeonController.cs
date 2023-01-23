using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonController : MovementController
{

    //animation
    private Animator animator;
    private int Death;
    [SerializeField] private GameObject deadBodyPlayerPrefab;

    protected void Awake()
    {
        Init();
    }

    protected void Update()
    {
        UpdateController();

        Vector2 delta = LabyrintheManager.Instance.endPoint.transform.position.ToVector2() - transform.position.ToVector2();
        if (delta.sqrMagnitude < 1)
        {
            GameManager.Instance.PeloSauve++;
            Debug.Log("Un peon de sauvé en plus");

            Remove();
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

        // Spawn new entity
        Instantiate(deadBodyPlayerPrefab, transform.position, Quaternion.Euler(new Vector3(0.0f, 0.0f, 0f)));

        // Blood
       // GameObject part = Instantiate(bloodDeath, transform.position, Quaternion.identity);

        Remove();
    }

    private void Remove()
    {
        GameManager.Instance.peons.Remove(gameObject);
        GameObject.Destroy(gameObject);
    }
}