using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtonManager : Singleton<TileButtonManager>
{
    private List<TileButton> tileButtons = new List<TileButton>();
    private TileButton selectedTileButton = null;

    private void Awake()
    {
        TileButton[] tbs = GetComponentsInChildren<TileButton>();
        foreach (TileButton tb in tbs)
        {
            tileButtons.Add(tb);
        }
    }

    public void SelectTileButton(TileButton tb)
    {
        selectedTileButton = tb;
    }

    public void UnselectTileButton()
    {
        selectedTileButton = null;
    }

    public TileButton GetSelectedTileButton()
    {
        return selectedTileButton;
    }
}
