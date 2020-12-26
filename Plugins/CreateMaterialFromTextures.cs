// CreateMaterialsForTextures.cs
// C#
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
     
public class CreateMaterialFromTextures : ScriptableWizard
{
    public Shader shader;

    [MenuItem("Tools/CreateMaterialsForTextures")]
    static void CreateWizard ()
    {
        ScriptableWizard.DisplayWizard<CreateMaterialFromTextures>("Create Materials", "Create");

    }

    void OnEnable()
    {
        shader = Shader.Find("Diffuse");
    }

    void OnWizardCreate ()
    {
        try
        {
            AssetDatabase.StartAssetEditing();
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            var mat = new Material(shader);
            mat.EnableKeyword("_NORMALMAP");
            mat.EnableKeyword("_DETAIL_MULX2");
            mat.EnableKeyword ("_METALLICGLOSSMAP");
            string path = null;

            foreach(var tex in textures)
            {
                path = AssetDatabase.GetAssetPath(tex);
                path = path.Substring(0,path.LastIndexOf("."))+".mat";
                if (AssetDatabase.LoadAssetAtPath(path,typeof(Material)) != null)
                {
                    Debug.LogWarning("Can't create material, it already exists: " + path);
                    continue;
                }
                if (tex.name.Contains("basecolor"))     {
                    mat.mainTexture = tex;
                }
                else if (tex.name.Contains("metallic"))  {
                    mat.SetTexture("_MetallicGlossMap", tex);
                }
                else if (tex.name.Contains("normal"))  {
                    mat.SetTexture("_BumpMap", tex);
                }
                
            }
            if (path != null)
                AssetDatabase.CreateAsset(mat,path);
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
        }
    }
}
#endif