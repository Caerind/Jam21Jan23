using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    private TileObject tileObject = null;
    [SerializeField] private Image image;
    [SerializeField] private GameObject TileSelected;


    private void Update()
    {
    }

    public void SetTileObject(TileObject to)
    {
        tileObject = to;
        //image.sprite = tileObject.Type.
    }


    public void OnClick()
    {
        if (TileButtonManager.Instance.GetSelectedTileButton() != this)
        {
            TileButtonManager.Instance.SelectTileButton(this);
            TileSelected.SetActive(true);
        }
    }

    public void OnUnselect()
    {
        //TileButtonManager.Instance.SelectTileButton(this);
        TileSelected.SetActive(false);
    }

    public TileObject GetTileObject()
    {
        return tileObject;
    }
}
