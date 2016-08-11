using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class EnhancedColorLevels : MonoBehaviour {

	[Header("Input Levels")]
	[Range(0,255)]
	public int Shadows;
	[Range(0.1f,10.0f)]
	public float Gamma = 1.0f;
	[Range(0,255)]
	public int Highlights = 255;
	[Header("Output Levels")]
	[Range(0,255)]
	public int OutBlack;
	[Range(0,255)]
	public int OutWhite = 255;

	public bool ShowBeforeAndAfterEffect;

	Material mat;

	void Awake() {
		mat = new Material (Shader.Find("Custom/EnhancedColorLevelsShader"));
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		
		mat.SetFloat("_InBlack", Shadows);
		mat.SetFloat("_InGamma", Gamma);
		mat.SetFloat("_InWhite", Highlights);
		mat.SetFloat("_OutWhite", OutWhite);
		mat.SetFloat("_OutBlack", OutBlack);
		mat.SetFloat("_ShowBeforeXThreshold", ShowBeforeAndAfterEffect?0.5f:0.0f);

		Graphics.Blit(src, dest, mat);
	}
}
