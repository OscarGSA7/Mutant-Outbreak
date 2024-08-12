using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Wilberforce
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
	[HelpURL("https://projectwilberforce.github.io/colorblind/")]
    [AddComponentMenu("Image Effects/Color Adjustments/Colorblind")]
    public class Colorblind : MonoBehaviour
    { 
		public int Type = 0;

        
		public Shader colorblindShader;
        private bool isSupported;
        private Material ColorblindMaterial;

		
        private void ReportError(string error)
        {
            if (Debug.isDebugBuild) Debug.Log("Colorblind Effect Error: " + error);
        }

        [System.Obsolete]
        void Start()
        {
			
            if (colorblindShader == null) colorblindShader = Shader.Find("Hidden/Wilberforce/Colorblind");

			
            if (colorblindShader == null)
            {
                ReportError("Could not locate Colorblind Shader. Make sure there is 'Colorblind.shader' file added to the project.");
                isSupported = false;
                enabled = false;
                return;
            }

			
            if (!SystemInfo.supportsImageEffects || SystemInfo.graphicsShaderLevel < 30)
            {
                if (!SystemInfo.supportsImageEffects) ReportError("System does not support image effects.");
                if (SystemInfo.graphicsShaderLevel < 30) ReportError("This effect needs at least Shader Model 3.0.");

                isSupported = false;
                enabled = false;
                return;
            }

			
            EnsureMaterials();

			
            if (!ColorblindMaterial || ColorblindMaterial.passCount != 1)
            {
                ReportError("Could not create shader.");
                isSupported = false;
                enabled = false;
                return;
            }

            isSupported = true;
        }

        private static Material CreateMaterial(Shader shader)
        {
            if (!shader) return null;
            Material m = new Material(shader);
            m.hideFlags = HideFlags.HideAndDontSave;
            return m;
        }

        private static void DestroyMaterial(Material mat)
        {
            if (mat)
            {
                DestroyImmediate(mat);
                mat = null;
            }
        }

        private void EnsureMaterials()
        {
            if (!ColorblindMaterial && colorblindShader.isSupported)
            {
				
                ColorblindMaterial = CreateMaterial(colorblindShader);
            }

            if (!colorblindShader.isSupported)
            {
                ReportError("Could not create shader (Shader not supported).");
            }
        }
			
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!isSupported || !colorblindShader.isSupported)
            {	
				
                enabled = false;
                return;
            }

            EnsureMaterials();

            
			
			ColorblindMaterial.SetInt ("type", Type);
			
			Graphics.Blit (
				source, 
				destination, 
				ColorblindMaterial,
				0 
			);
        }
    }

	
	#if UNITY_EDITOR 

	
	[CustomEditor(typeof(Colorblind))]
	public class ColorblindEditor : Editor
	{	
		
		private readonly GUIContent[] typeTexts = new GUIContent[4] {
			new GUIContent("Normal Vision"),
			new GUIContent("Protanopia"),
			new GUIContent("Deuteranopia"),
			new GUIContent("Tritanopia")
		};
		
		private readonly GUIContent typeLabelContent = new GUIContent("Type:", "Type of color blindness");

		
		private readonly int[] typeInts = new int[4] { 0, 1, 2, 3 };

		
		override public void OnInspectorGUI()
		{
			
			var colorblindScript = target as Colorblind;

            
            colorblindScript.Type = EditorGUILayout.IntPopup(typeLabelContent, colorblindScript.Type, typeTexts, typeInts);

			
			if (GUI.changed)
			{
				
				EditorUtility.SetDirty(target);
			}
		}
	}
	#endif
}