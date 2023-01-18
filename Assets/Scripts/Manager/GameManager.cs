using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float timer = 0.0f;
    private bool isPlaying = false;

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;

            if (UpdateCheckGameFinished())
            {
                Reset();
            }
        }
    }

    private bool UpdateCheckGameFinished()
    {
        /*
        // Has player win ?
        if (playerGeneral != null && playerGeneral.GetSelectableSoldiersCount() == 0)
        {
            GameApplication.Instance.SetPlayerWin(false);
            return true;
        }

        // Has ai win ?
        if (aiGeneral != null && aiGeneral.GetSoldiers() != null && aiGeneral.GetSoldiers().Count == 0)
        {
            GameApplication.Instance.SetPlayerWin(false);
            return true;
        }
        */

        return false;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    /*
    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 25), "ZonePlayer: " + GetPlayerZone().GetEnemyInZoneCounter().ToString());
        GUI.Label(new Rect(5, 35, 100, 25), "ZoneEnemy: " + GetAIZone().GetEnemyInZoneCounter().ToString());
        GUI.Label(new Rect(5, 60, 100, 25), "AINext: " + aiGeneral.GetNextSelectedIndex().ToString());
    }
    */

    public float GetTimer()
    {
        return timer;
    }

    public void Reset()
    {
        isPlaying = false;
        
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
