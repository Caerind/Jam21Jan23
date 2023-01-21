using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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

    [SerializeField]
    private float XVouluTemp = 1f;
    [SerializeField]
    private float YVouluTemp = 1f;
    private List<Vector2> DestinationList= null;
    private int IndexDestination=0;


    bool HasMoved;

    //animation
    protected Animator animator;
    protected int animIDMvt;
    protected int animDIR;
    [SerializeField] private float IntensiteDegatShake = 6f;
    [SerializeField] private float TimerDegatShake = 3f;


    // Update is called once per frame
    void Update()
    {
        Vector2 Destination=new Vector2 (XVoulu, YVoulu);
        if (DestinationList == null || DestinationList.Count == 0)
        {
            IndexDestination = 0;
            DestinationList = LabyrintheManager.Instance.GetPath(transform.position.ToVector2(), Destination);
        }
        GetMouvementDirection();
    }

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animIDMvt = Animator.StringToHash("mvt");
        animIDMvt = Animator.StringToHash("dir");

    }

    private void Start()
    {
        CinemachineCameraShake.Instance.Shake(IntensiteDegatShake, TimerDegatShake);

    }

    public void GetMouvementDirection()
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
