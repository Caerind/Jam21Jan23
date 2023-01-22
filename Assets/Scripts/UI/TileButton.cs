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

        int w = 64;
        int h = 80;

        var lab = LabyrintheManager.Instance;

        var newTexture = new Texture2D(w, h);
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Color color = new Color(1, 1, 1, 0);

                // TODO : GroundTile first ?

                for (int i = 0; i < 3; ++i)
                {

                    Sprite s = lab.walls[i].sprite;
                    Color c = s.texture.GetPixel(x + (int)s.rect.x, y + (int)s.rect.y);
                    if (c.a != 0 && to.wallDirection[i])
                    {
                        color = c;
                    }

                    int other = (i == 0) ? 5 : (i == 1) ? 4 : 3;
                    Sprite s2 = lab.walls[i].sprite;
                    Color c2 = s.texture.GetPixel(x + (int)s2.rect.x, y + (int)s2.rect.y);
                    if (c2.a != 0 && to.wallDirection[other])
                    {
                        color = c2;
                    }
                }

                newTexture.SetPixel(x, y, color);
            }
        }

        newTexture.Apply();
        image.sprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
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
