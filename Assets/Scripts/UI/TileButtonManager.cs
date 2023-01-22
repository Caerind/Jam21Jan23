using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtonManager : Singleton<TileButtonManager>
{
    private List<TileButton> tileButtons = new List<TileButton>();
    private TileButton selectedTileButton = null;

    public float randomPercentWalls = 0.5f;

    private void Awake()
    {
        TileButton[] tbs = GetComponentsInChildren<TileButton>();
        foreach (TileButton tb in tbs)
        {
            tileButtons.Add(tb);
            tb.SetTileObject(GenerateTileObject());
        }
    }

    private TileObject GenerateTileObject()
    {
        TileObject to = new TileObject();
        for (int i = 0; i < 6; ++i)
        {
            to.wallDirection[i] = Random.Range(0.0f, 1.0f) < randomPercentWalls;
        }
        return to;
    }

    public void ConsumeSelectedTile()
    {
        if (selectedTileButton != null)
        {
            selectedTileButton.SetTileObject(GenerateTileObject());
            UnselectTileButton();
        }
    }

    public void SelectTileButton(TileButton tb)
    {
        if (selectedTileButton != null)
        {
            UnselectTileButton();
        }
        selectedTileButton = tb;
    }

    public void UnselectTileButton()
    {
        if (selectedTileButton != null)
        {
            selectedTileButton.OnUnselect();
        }
        selectedTileButton = null;
    }

    public TileButton GetSelectedTileButton()
    {
        return selectedTileButton;
    }
}
