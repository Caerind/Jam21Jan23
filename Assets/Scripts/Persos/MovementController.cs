using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float Vitesse = 1f;
    [SerializeField]
    private float EcartVoulu = 0.1f;

    protected List<Vector2> DestinationList = null;
    protected int IndexDestination = 0;

    //animation
    private Animator animator;
    private int animIDMvt;
    private int animDIR;

    protected void InitializeAnimator()
    {
        animator = GetComponentInChildren<Animator>();
        animIDMvt = Animator.StringToHash("mvt");
        animIDMvt = Animator.StringToHash("dir");
    }

    protected void UpdateController()
    {
        UpdatePath();
        GetMouvementDirection();
    }

    protected virtual void UpdatePath()
    {
    }

    protected void GetMouvementDirection()
    {
        Vector2 delta = DestinationList[IndexDestination] - transform.position.ToVector2();
        float mvt = delta.magnitude;

        delta.Normalize();

        //gestion de la direction pour les animations
        int dir = 0;
        if(delta.x < 0f)
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
        //Animation
        animator?.SetFloat(animIDMvt, mvt);
        animator?.SetInteger(animDIR, dir);
        

        if ((delta.x < -EcartVoulu || delta.x > EcartVoulu) || (delta.y < -EcartVoulu || delta.y > EcartVoulu))
        {
            float s = Vitesse * Time.deltaTime;
            transform.position += new Vector3(s * delta.x, s * delta.y, 0.0f);
        }
        else
        {
            IndexDestination ++;
        }
    }
}
