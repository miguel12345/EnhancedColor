using UnityEngine;
using System.Collections;

public class LUTextureGenerator
{

	public static Texture2D GenerateLUTexture (AnimationCurve RedChannelCurve,
	                                 AnimationCurve GreenChannelCurve,
	                                 AnimationCurve BlueChannelCurve)
	{
		Texture2D texture = new Texture2D (256,1,TextureFormat.RGB24,false);

		ModifyLUTexture (texture, RedChannelCurve, GreenChannelCurve, BlueChannelCurve);

		//Setting the wrap mode to clamp is extremly important
		//otherwise you'd get wrong values on the edges of the texture due
		//to bilinear filtering
		texture.wrapMode = TextureWrapMode.Clamp;

		return texture;
	}

	public static void ModifyLUTexture(Texture2D texture,
		AnimationCurve RedChannelCurve,
		AnimationCurve GreenChannelCurve,
		AnimationCurve BlueChannelCurve) {

		float step = 1.0f / 256.0f;
		int pixelX = 0;
		for (pixelX = 0; pixelX < 256; pixelX += 1) {
			texture.SetPixel(pixelX,0,SampleColorFromChannelCurves(RedChannelCurve,
				GreenChannelCurve,
				BlueChannelCurve,pixelX*step));
		}
	}

	static Color SampleColorFromChannelCurves(AnimationCurve RedChannelCurve,
		AnimationCurve GreenChannelCurve,
		AnimationCurve BlueChannelCurve, float t) {
		return new Color(RedChannelCurve.Evaluate(t),
			GreenChannelCurve.Evaluate(t),
			BlueChannelCurve.Evaluate(t));
	}
} 