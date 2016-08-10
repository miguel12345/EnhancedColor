Shader "Custom/EnhancedColorShader"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_InBlack ("Shadows", Float) = 0
		_InWhite ("Highlights", Float) = 255
		_InGamma ("Gamma", Float) = 1.0
		_OutWhite ("Out White", Float) = 255
		_OutBlack ("Out Black", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
			fixed _InBlack;
			fixed _InWhite;
			fixed _InGamma;
			fixed _OutWhite;
			fixed _OutBlack;

			fixed4 frag (v2f_img i) : COLOR
			{
				// sample the texture
				fixed4 inPixel = tex2D(_MainTex, i.uv);
				fixed4 outPixel = (pow(((inPixel * 255.0) - _InBlack) / (_InWhite - _InBlack),
                _InGamma) * (_OutWhite - _OutBlack) + _OutBlack) / 255.0;
                outPixel.a = 1.0f;
				return outPixel;
			}
			ENDCG
		}
	}
}
