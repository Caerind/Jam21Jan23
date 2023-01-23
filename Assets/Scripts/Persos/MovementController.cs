using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static IGraph;

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

    public void ResetPath()
    {
        DestinationList = null;
    }

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

            if (astar.IsFinished() && !HasPath())
            {
                var tileInfos = LabyrintheManager.Instance.GetTileInfos();
                Vector2Int currentCell = LabyrintheManager.Instance.GetCellFromPos(transform.position);
                Vector2Int[] neighbors = PathfindingHexGrid2D.GetNeighborCells(currentCell);

                List<Vector2Int> possibles = new List<Vector2Int>();
                for (int dir = 0; dir < 6; ++dir)
                {
                    if (tileInfos[currentCell].wallDirection[dir] == false)
                    {
                        Vector2Int otherCoords = neighbors[dir];
                        TileInfo otherTile = tileInfos.ContainsKey(otherCoords) ? tileInfos[otherCoords] : null;

                        if (otherTile != null && otherTile.wallDirection[(dir + 3) % 6] == false)
                        {
                            possibles.Add(otherCoords);
                        }
                    }
                }

                if (possibles.Count > 0)
                {
                    int index = Random.Range(0, possibles.Count);
                    Vector2Int cell = possibles[index];
                    Vector2 pos = LabyrintheManager.Instance.GetPosFromCell(cell);
                    DestinationList = new List<Vector2> { pos };
                    IndexDestination = 0;
                }
            }
        }
    }

    private void UpdateMovement()
    {
        if (HasPath())
        {
            Vector2 delta = DestinationList[IndexDestination] - transform.position.ToVector2();
            float mvtMagnitude = delta.magnitude;

            delta.Normalize();

            // Gestion de la direction pour les animations
            int dir = 0;

            if (delta.x < 0f && delta.y<0f)
            {
                dir = 1;
            }
            else if (delta.x < 0f && delta.y > 0f)
            {
                dir = 2;
            }
            else if (delta.x > 0f && delta.y > 0f)
            {
                dir = 3;
            }
            else if (delta.x > 0f && delta.y < 0f)
            {
                dir = 4;
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
