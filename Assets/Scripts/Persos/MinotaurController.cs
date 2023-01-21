using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MovementController
{
    public Vector2 DestinationTest;

    protected void Awake()
    {
        InitializeAnimator();
    }

    protected void Update()
    {
        UpdateController();
    }

    protected override void UpdatePath()
    {
        if (DestinationList == null || DestinationList.Count == 0)
        {
            DestinationList = LabyrintheManager.Instance.GetPath(transform.position.ToVector2(), DestinationTest);
            IndexDestination = 0;
        }
    }
}