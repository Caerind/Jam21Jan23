using UnityEngine;

public class MouseCursor : Singleton<MouseCursor>
{
    [SerializeField] private Texture2D mouseCursorTexture;
    [SerializeField] private Vector2 mouseCursorHotspot;

    private bool showCursor = false; // Start as false because there is a check

    private void Start()
    {
        ShowCursor(true);
    }

    private void OnMouseEnter()
    {
        ShowCursor(true);
    }

    private void OnMouseExit()
    {
        ShowCursor(false);
    }

    public void ShowCursor(bool visible)
    {
        if (showCursor != visible)
        {
            if (visible)
            {
                Cursor.SetCursor(mouseCursorTexture, mouseCursorHotspot, CursorMode.ForceSoftware);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            showCursor = visible;
        }
    }
}
