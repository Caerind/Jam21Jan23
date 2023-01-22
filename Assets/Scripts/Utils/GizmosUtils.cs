using UnityEngine;

public static class GizmosUtils
{
    public static void DrawSprite(Sprite sprite, Vector3 position)
    {
        Rect dstRect = new Rect(position.x - sprite.bounds.max.x,
                                  position.y + sprite.bounds.max.y,
                                  sprite.bounds.size.x,
                                  -sprite.bounds.size.y);

        Rect srcRect = new Rect(sprite.rect.x / sprite.texture.width,
                                 sprite.rect.y / sprite.texture.height,
                                 sprite.rect.width / sprite.texture.width,
                                 sprite.rect.height / sprite.texture.height);

        Graphics.DrawTexture(dstRect, sprite.texture, srcRect, 0, 0, 0, 0);
    }

    /*
    public static void DrawString(string text, Vector3 position, Color? color = null)
    {
        UnityEditor.Handles.BeginGUI();

        var restoreColor = GUI.color;
        if (color.HasValue) GUI.color = color.Value;

        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(position);

        if (screenPos.x >= 0 && screenPos.x <= Screen.width && screenPos.y >= 0 && screenPos.y <= Screen.height && screenPos.z >= 0)
        {
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        }

        GUI.color = restoreColor;
        UnityEditor.Handles.EndGUI();
    }
    */
}
