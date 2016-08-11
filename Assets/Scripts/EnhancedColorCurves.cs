using UnityEngine;
using System.Collections;
using System.IO;

[ExecuteInEditMode]
public class EnhancedColorCurves : MonoBehaviour
{
	public AnimationCurve RedChannelCurve;
	public AnimationCurve GreenChannelCurve;
	public AnimationCurve BlueChannelCurve;
	public Texture2D BakedLUTexture;
	public bool UseBakedTexture;
	public bool ShowBeforeAndAfterEffect;

	void Reset() {
		RedChannelCurve = DefaultAnimationCurve;
		GreenChannelCurve = DefaultAnimationCurve;
		BlueChannelCurve = DefaultAnimationCurve;
		_luTexture = null;
	}

	#if UNITY_EDITOR
	public void SaveLUTexture() {

		if (_luTexture == null) {
			Debug.LogWarning ("LUTexture doesn't exist");
			return;
		}

		//Write png bytes and refresh database for the assset file to be created
		var fileName =  string.Format("{0} lut.png", gameObject.name);
		var textureAssetPath = Path.Combine(Application.dataPath,fileName);
		File.WriteAllBytes(textureAssetPath,_luTexture.EncodeToPNG());
		UnityEditor.AssetDatabase.Refresh ();

		//Get texture importer and fix import settings
		var textureImporter = (UnityEditor.TextureImporter) UnityEditor.TextureImporter.GetAtPath("Assets/"+fileName);
		textureImporter.wrapMode = TextureWrapMode.Clamp;
		textureImporter.textureFormat = UnityEditor.TextureImporterFormat.RGB24;
		textureImporter.isReadable = true;
		textureImporter.SaveAndReimport ();
	}
	#endif

	AnimationCurve DefaultAnimationCurve {
		get {
			return AnimationCurve.Linear (0.0f, 0.0f, 1.0f, 1.0f);
		}
	}

	Material _mat;
	Texture2D _luTexture;

	void RegenerateLUTexture() {

		if (UseBakedTexture) {
			_luTexture = BakedLUTexture;
		} else {
			if (_luTexture == null) {
				_luTexture = LUTextureGenerator.GenerateLUTexture (RedChannelCurve, GreenChannelCurve, BlueChannelCurve);
			} else {
				LUTextureGenerator.ModifyLUTexture (_luTexture,RedChannelCurve, GreenChannelCurve, BlueChannelCurve);
			}
			_luTexture.Apply ();
		}
	}

	void OnValidate() {

		if (!UseBakedTexture)
			BakedLUTexture = null;
		
		RegenerateLUTexture ();
	}

	void Awake() {
		_mat = new Material (Shader.Find("Custom/EnhancedColorCurvesShader"));
		if (_luTexture == null) {
			RegenerateLUTexture ();
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {

		if (_luTexture == null) {
			Graphics.Blit (src, dest);
			return;
		}
		
		_mat.SetTexture("_LUTex", _luTexture);
		_mat.SetFloat("_ShowBeforeXThreshold", ShowBeforeAndAfterEffect?0.5f:0.0f);

		Graphics.Blit(src, dest, _mat);
	}
}

