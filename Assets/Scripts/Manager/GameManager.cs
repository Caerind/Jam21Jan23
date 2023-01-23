using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float IntensiteDegatShake = 6f;
    [SerializeField] private float TimerDegatShake = 3f;

    [HideInInspector] public List<GameObject> peons = new List<GameObject>();
    [HideInInspector] public GameObject minotaur = null;
    
    [SerializeField] public int PeloaSauver=10;
    public int PeloSauve = 0;

    private bool isPlaying = true;

    private void Update()
    {
        if (isPlaying)
        {
            HandleTilePlacement();

            UpdateCheckGameFinished();
        }
    }

    private void HandleTilePlacement()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 Worldpos2D = new Vector2(Worldpos.x, Worldpos.y);

            TileButton selectedButton = TileButtonManager.Instance.GetSelectedTileButton();
            if (selectedButton != null && selectedButton.GetTileObject() != null)
            {
                LabyrintheManager.Instance.SetTile(Worldpos2D, selectedButton.GetTileObject());
                TileButtonManager.Instance.ConsumeSelectedTile();
            }
        }

    }

    private bool UpdateCheckGameFinished()
    {
        // Has player win ?
        if (PeloSauve > PeloaSauver)
        {
            Debug.Log("Victoire");
            return true;
        }
        else 
        { 
            return false;
        }
    }

    public void CameraShake()
    {
        CinemachineCameraShake.Instance.Shake(IntensiteDegatShake, TimerDegatShake);
    }

    //public bool IsPlaying()
    //{
    //    return isPlaying;
    //}

    /*
    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 25), "ZonePlayer: " + GetPlayerZone().GetEnemyInZoneCounter().ToString());
        GUI.Label(new Rect(5, 35, 100, 25), "ZoneEnemy: " + GetAIZone().GetEnemyInZoneCounter().ToString());
        GUI.Label(new Rect(5, 60, 100, 25), "AINext: " + aiGeneral.GetNextSelectedIndex().ToString());
    }
    */

    //public float GetTimer()
    //{
    //    return timer;
    //}

    public void Reset()
    {
      //  isPlaying = false;
        
        /*
        interestPoints.Clear();
        playerGeneral = null;
        aiGeneral = null;
        playerZone = null;
        aiZone = null;
        playerZonePoints.Clear();
        aiZonePoints.Clear();
        */
    }
}
