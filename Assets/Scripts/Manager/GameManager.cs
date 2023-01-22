using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float IntensiteDegatShake = 6f;
    [SerializeField] private float TimerDegatShake = 3f;

    public List<GameObject> peons = new List<GameObject>();

    private void Update()
    {
        HandleTilePlacement();
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
                selectedButton.OnUnselect();
            }
        }

    }

    public void CameraShake()
    {
        CinemachineCameraShake.Instance.Shake(IntensiteDegatShake, TimerDegatShake);
    }
}
