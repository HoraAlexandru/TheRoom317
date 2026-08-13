using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class AutoAssignTextures
{
    [MenuItem("Tools/Auto Assign Textures to Materials")]
    public static void AutoAssign()
    {
        // Cauta materiale (oriunde)
        string[] materials = AssetDatabase.FindAssets("t:Material");
        
        // Cauta texturi in Assets/Textures (și subfoldere)
        string[] textures = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Textures" });

        foreach (string matGuid in materials)
        {
            string matPath = AssetDatabase.GUIDToAssetPath(matGuid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
            string matName = mat.name.ToLower();

            foreach (string texGuid in textures)
            {
                string texPath = AssetDatabase.GUIDToAssetPath(texGuid);
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
                string texName = tex.name.ToLower();

                // Potriveste texturile cu materialele
                if (texName.Contains(matName) || matName.Contains(texName))
                {
                    mat.SetTexture("_BaseMap", tex);
                    Debug.Log($"✓ Assigned {tex.name} to {mat.name}");
                    break;
                }
            }
        }
        
        AssetDatabase.SaveAssets();
        Debug.Log("Done!");
    }
}