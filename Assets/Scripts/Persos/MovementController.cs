using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float Vitesse = 1f;
    [SerializeField]
    private float EcartVoulu = 0.1f;

    [SerializeField] private int pathIteration = 5;

    private List<Vector2> DestinationList = null;
    private int IndexDestination = 0;
    private PathfindAStar astar = null;

    //animation
    private Animator animator;
    private int animIDMvt;
    private int animDIR;

    protected void Init()
    {
        animator = GetComponentInChildren<Animator>();
        animIDMvt = Animator.StringToHash("mvt");
        animIDMvt = Animator.StringToHash("dir");

        astar = new PathfindAStar();
        astar.Start(null, 0, 0); // Hack just to be considered as finished from the begininng
    }

    protected void UpdateController()
    {
        UpdatePath();
        UpdateAstar();
        UpdateMovement();
    }

    protected virtual void UpdatePath()
    {
    }

    protected bool HasPath() => DestinationList != null && DestinationList.Count > 0 && IndexDestination < DestinationList.Count;
    protected bool IsComputingPath() => !astar.IsFinished();

    protected void RequestPath(Vector2 from, Vector2 to)
    {
        LabyrintheManager.Instance.RequestPath(astar, from, to);
    }

    private void UpdateAstar()
    {
        if (IsComputingPath())
        {
            for (int i = 0; i < pathIteration && !astar.IsFinished(); ++i)
            {
                astar.Iteration();
            }

            // Just finished
            if (astar.IsFinished())
            {
                DestinationList = LabyrintheManager.Instance.GetPathFromAstar(astar);
                IndexDestination = 0;
            }
        }
    }

    private void UpdateMovement()
    {
        if (DestinationList != null && DestinationList.Count > 0 && IndexDestination < DestinationList.Count)
        {
            Vector2 delta = DestinationList[IndexDestination] - transform.position.ToVector2();
            float mvtMagnitude = delta.magnitude;

            delta.Normalize();

            // Gestion de la direction pour les animations
            int dir = 0;
            if (delta.x < 0f)
            {
                dir = 1;
            }
            else if (delta.x > 0f)
            {
                dir = 2;
            }
            else
            {
                dir = 0;
            }

            // Animation
            animator?.SetFloat(animIDMvt, mvtMagnitude);
            animator?.SetInteger(animDIR, dir);

            if (mvtMagnitude > EcartVoulu)
            {
                float s = Vitesse * Time.deltaTime;
                transform.position += new Vector3(s * delta.x, s * delta.y, 0.0f);
            }
            else
            {
                IndexDestination++;
            }
        }
    }

    protected void DrawGizmoPath()
    {
        if (DestinationList != null && DestinationList.Count > 0)
        {
            for (int i = 0; i < DestinationList.Count; i++)
            {
                if (IndexDestination == i)
                    Gizmos.color = Color.green;
                else if (i > IndexDestination)
                    Gizmos.color = Color.white;
                else
                    Gizmos.color = Color.black;

                Gizmos.DrawWireSphere(DestinationList[i], 0.3f);
            }
        }
    }
}
