using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public void OnClick()
    {
        TileButtonManager.Instance.SelectTileButton(this);
    }
}
