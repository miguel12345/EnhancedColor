Shader "Custom/EnhancedColorCurvesShader"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LUTex ("LUTexture",2D) = "white" {}
		_ShowBeforeXThreshold ("Show Before X Threshold", Float) = 0
	}
	SubShader
	{
		Pass
		{
			Cull Off
		 	ZWrite Off
		 	ZTest Always

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _LUTex;
			fixed _ShowBeforeXThreshold;

			fixed4 frag (v2f_img i) : COLOR
			{
				//TESTING - BEGIN
				//return tex2D(_LUTex, i.uv);
				//TESTING - END



				// sample the texture
				fixed4 inPixel = tex2D(_MainTex, i.uv);
				if(i.uv.x < _ShowBeforeXThreshold) return inPixel;

				//TESTING - RED CHANNEL
//				float r = tex2D(_LUTex, float2(inPixel.r,0.0)).r;
////				return fixed4(inPixel.r,0.0,0.0,1.0);
//				return fixed4(tex2D(_LUTex, float2(0.4/256.0,0.0)));
				//TESTING

				fixed4 outPixel;
				outPixel.r = tex2D(_LUTex, float2(inPixel.r,0.0)).r;
				outPixel.g = tex2D(_LUTex, float2(inPixel.g,0.0)).g;
				outPixel.b = tex2D(_LUTex, float2(inPixel.b,0.0)).b;
				outPixel.a = 1.0;

				return outPixel;
			}
			ENDCG
		}
	}
}
