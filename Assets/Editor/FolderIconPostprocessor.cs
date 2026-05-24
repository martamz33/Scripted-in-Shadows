using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace CrazyBirdLady.FolderIcons
{
    [InitializeOnLoad]
    public static class FolderIconPostprocessor
    {
        private static Dictionary<string, Texture2D> folderIcons = new Dictionary<string, Texture2D>();
        private static readonly string iconKeyPrefix = "FolderColor_";

        static FolderIconPostprocessor()
        {
            LoadIcons();
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        static void LoadIcons()
        {
            folderIcons.Clear();

            folderIcons["CharcoalGray"] = LoadIcon("charcoalGray");
            folderIcons["LightGray"] = LoadIcon("lightGray");
            folderIcons["Snow"] = LoadIcon("snow");
            folderIcons["Orange"] = LoadIcon("orange");
            folderIcons["Red"] = LoadIcon("red");
            folderIcons["Brown"] = LoadIcon("brown");
            folderIcons["Yellow"] = LoadIcon("yellow");
            folderIcons["Lemon"] = LoadIcon("lemon");
            folderIcons["Olive"] = LoadIcon("olive");
            folderIcons["GrassGreen"] = LoadIcon("grassGreen");
            folderIcons["Lime"] = LoadIcon("lime");
            folderIcons["Teal"] = LoadIcon("teal");
            folderIcons["Turquoise"] = LoadIcon("turquoise");
            folderIcons["NavyBlue"] = LoadIcon("navyBlue");
            folderIcons["SkyBlue"] = LoadIcon("skyBlue");
            folderIcons["Purple"] = LoadIcon("purple");
            folderIcons["Lavender"] = LoadIcon("lavender");
            folderIcons["Rose"] = LoadIcon("rose");
            folderIcons["Coral"] = LoadIcon("coral");
            folderIcons["Blush"] = LoadIcon("blush");
            folderIcons["Bubblegum"] = LoadIcon("bubblegum");
            folderIcons["Apricot"] = LoadIcon("apricot");
            folderIcons["Sand"] = LoadIcon("sand");

        }

        static Texture2D LoadIcon(string name)
        {
            // Loads from: Assets/Editor Default Resources/FolderIcons/name.png
            return EditorGUIUtility.Load($"FolderIcons/{name}.png") as Texture2D;
        }

        static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(path))
                return;

            string iconName = EditorPrefs.GetString(iconKeyPrefix + path, "");
            if (!folderIcons.TryGetValue(iconName, out Texture2D baseIcon) || baseIcon == null)
                return;

            Texture2D iconToUse = baseIcon;
            bool isListView = selectionRect.height < 25f;

            if (isListView)
            {
                Texture2D listIcon = LoadIcon(iconName + "_list");
                if (listIcon != null)
                    iconToUse = listIcon;
            }

            Rect iconRect = isListView
                ? new Rect(selectionRect.x, selectionRect.y + 1f, 16f, 16f)
                : new Rect(
                    selectionRect.x + (selectionRect.width - Mathf.Min(selectionRect.width, selectionRect.height - 12f)) / 2f,
                    selectionRect.y,
                    Mathf.Min(selectionRect.width, selectionRect.height - 12f),
                    Mathf.Min(selectionRect.width, selectionRect.height - 12f)
                  );

            GUI.DrawTexture(iconRect, iconToUse, ScaleMode.ScaleToFit);
        }

        // --- Menu Items ---
        [MenuItem("Assets/Set Folder Color/CharcoalGray", true)]
        [MenuItem("Assets/Set Folder Color/LightGray", true)]
        [MenuItem("Assets/Set Folder Color/Snow", true)]
        [MenuItem("Assets/Set Folder Color/Orange", true)]
        [MenuItem("Assets/Set Folder Color/Red", true)]
        [MenuItem("Assets/Set Folder Color/Brown", true)]
        [MenuItem("Assets/Set Folder Color/Yellow", true)]
        [MenuItem("Assets/Set Folder Color/Lemon", true)]
        [MenuItem("Assets/Set Folder Color/Olive", true)]
        [MenuItem("Assets/Set Folder Color/GrassGreen", true)]
        [MenuItem("Assets/Set Folder Color/Lime", true)]
        [MenuItem("Assets/Set Folder Color/Teal", true)]
        [MenuItem("Assets/Set Folder Color/Turquoise", true)]
        [MenuItem("Assets/Set Folder Color/NavyBlue", true)]
        [MenuItem("Assets/Set Folder Color/SkyBlue", true)]
        [MenuItem("Assets/Set Folder Color/Purple", true)]
        [MenuItem("Assets/Set Folder Color/Lavender", true)]
        [MenuItem("Assets/Set Folder Color/Rose", true)]
        [MenuItem("Assets/Set Folder Color/Coral", true)]
        [MenuItem("Assets/Set Folder Color/Blush", true)]
        [MenuItem("Assets/Set Folder Color/Bubblegum", true)]
        [MenuItem("Assets/Set Folder Color/Apricot", true)]
        [MenuItem("Assets/Set Folder Color/Sand", true)]

        private static bool ValidateSetColor() =>
            Selection.activeObject != null &&
            AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(Selection.activeObject));

        // Color Setters
        [MenuItem("Assets/Set Folder Color/CharcoalGray")] private static void SetCharcoalGray() => SetColor("CharcoalGray");
        [MenuItem("Assets/Set Folder Color/LightGray")] private static void SetLightGray() => SetColor("LightGray");
        [MenuItem("Assets/Set Folder Color/Snow")] private static void SetSnow() => SetColor("Snow");
        [MenuItem("Assets/Set Folder Color/Orange")] private static void SetOrange() => SetColor("Orange");
        [MenuItem("Assets/Set Folder Color/Red")] private static void SetRed() => SetColor("Red");
        [MenuItem("Assets/Set Folder Color/Brown")] private static void SetBrown() => SetColor("Brown");
        [MenuItem("Assets/Set Folder Color/Yellow")] private static void SetYellow() => SetColor("Yellow");
        [MenuItem("Assets/Set Folder Color/Lemon")] private static void SetLemon() => SetColor("Lemon");
        [MenuItem("Assets/Set Folder Color/Olive")] private static void SetOlive() => SetColor("Olive");
        [MenuItem("Assets/Set Folder Color/GrassGreen")] private static void SetGrassGreen() => SetColor("GrassGreen");
        [MenuItem("Assets/Set Folder Color/Lime")] private static void SetLime() => SetColor("Lime");
        [MenuItem("Assets/Set Folder Color/Teal")] private static void SetTeal() => SetColor("Teal");
        [MenuItem("Assets/Set Folder Color/Turquoise")] private static void SetTurquoise() => SetColor("Turquoise");
        [MenuItem("Assets/Set Folder Color/NavyBlue")] private static void SetNavyBlue() => SetColor("NavyBlue");
        [MenuItem("Assets/Set Folder Color/SkyBlue")] private static void SetSkyBlue() => SetColor("SkyBlue");
        [MenuItem("Assets/Set Folder Color/Purple")] private static void SetPurple() => SetColor("Purple");
        [MenuItem("Assets/Set Folder Color/Lavender")] private static void SetLavender() => SetColor("Lavender");
        [MenuItem("Assets/Set Folder Color/Rose")] private static void SetRose() => SetColor("Rose");
        [MenuItem("Assets/Set Folder Color/Coral")] private static void SetCoral() => SetColor("Coral");
        [MenuItem("Assets/Set Folder Color/Blush")] private static void SetBlush() => SetColor("Blush");
        [MenuItem("Assets/Set Folder Color/Bubblegum")] private static void SetBubblegum() => SetColor("Bubblegum");
        [MenuItem("Assets/Set Folder Color/Apricot")] private static void SetApricot() => SetColor("Apricot");
        [MenuItem("Assets/Set Folder Color/Sand")] private static void SetSand() => SetColor("Sand");


        private static void SetColor(string color)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (AssetDatabase.IsValidFolder(path))
            {
                EditorPrefs.SetString(iconKeyPrefix + path, color);
                EditorApplication.RepaintProjectWindow();
            }
        }

        [MenuItem("Assets/Set Folder Color/Reset")]
        private static void ResetFolderColor()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (AssetDatabase.IsValidFolder(path))
            {
                EditorPrefs.DeleteKey(iconKeyPrefix + path);
                EditorApplication.RepaintProjectWindow();
            }
        }

        [MenuItem("Assets/Set Folder Color/Reset", true)]
        private static bool ValidateResetFolderColor()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return AssetDatabase.IsValidFolder(path) && EditorPrefs.HasKey(iconKeyPrefix + path);
        }
    }
}
