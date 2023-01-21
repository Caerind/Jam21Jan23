using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{
    private TileObject tileObject = null;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

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
        TileButtonManager.Instance.SelectTileButton(this);
    }
}
