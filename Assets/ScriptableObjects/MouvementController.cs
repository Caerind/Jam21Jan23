using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.Rendering.DebugUI;

public class MouvementController : MonoBehaviour
{

    private Vector2 movementInput;
    private Vector3 direction;

    [SerializeField]
    private float Vitesse = 1f;
    [SerializeField]
    private float XVoulu = 1f;
    [SerializeField]
    private float YVoulu = 1f;
    [SerializeField]
    private float EcartVoulu = 0.1f;


    bool HasMoved;

    //animation
    protected Animator animator;
    protected int animIDMvt;


    // Update is called once per frame
    void Update()
    {
        GetMouvementDirection();
    }

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animIDMvt = Animator.StringToHash("mvt");
    }

    public void GetMouvementDirection()
    {
        Vector2 p = new Vector2(XVoulu, YVoulu);
            Vector2 delta = p - transform.position.ToVector2();
        float mvt = delta.magnitude;

        delta.Normalize();

        animator?.SetFloat(animIDMvt, mvt);

        if ((delta.x < -EcartVoulu || delta.x > EcartVoulu) || (delta.y < -EcartVoulu || delta.y > EcartVoulu))
        {
            float s = Vitesse * Time.deltaTime;
            transform.position += new Vector3(s * delta.x, s * delta.y, 0.0f);
            //Animation
        }
    }

}
