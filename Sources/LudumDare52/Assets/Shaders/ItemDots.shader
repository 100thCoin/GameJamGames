// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ItemDots"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DotTex("Dot Texture", 2D) = "white" {}
		_DotCount("Dot Count", float) = 11
		_Color("Dot Color", Color) = (1,1,1,1)
		_Speed("Dot Speed", float) = 10
		_Opacity("Star Opacity", float) =1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}
		Pass
		{
		zTest Always
		zWrite off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _DotTex;
			float4 _Color;
			float _DotCount;
			float _Speed;
			float _Opacity;

			float4 frag(v2f i) : SV_Target
			{
				float4 color1 = tex2D(_MainTex, i.uv);
				float4 color2 = tex2D(_DotTex, float2(i.uv.x*_DotCount+_Time.y*_Speed,i.uv.y*_DotCount+_Time.y*_Speed));


				return color2;
			}
			ENDCG
		}
	}
}