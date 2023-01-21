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
        if (selectedTileButton != null)
        {
            UnselectTileButton();
        }
        selectedTileButton = tb;
    }

    public void UnselectTileButton()
    {
        if(selectedTileButton != null)
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
