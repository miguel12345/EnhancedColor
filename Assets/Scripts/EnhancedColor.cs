using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class EnhancedColor : MonoBehaviour {

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

	Material mat;

	void Awake() {
		mat = new Material (Shader.Find("Custom/EnhancedColorShader"));
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		
		mat.SetFloat("_InBlack", Shadows);
		mat.SetFloat("_InGamma", Gamma);
		mat.SetFloat("_InWhite", Highlights);
		mat.SetFloat("_OutWhite", OutWhite);
		mat.SetFloat("_OutBlack", OutBlack);

		Graphics.Blit(src, dest, mat);
	}
}
