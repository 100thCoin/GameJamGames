// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LightAura"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DotTex("Dot Texture", 2D) = "white" {}
		_DotCount("Dot Count", float) = 11
		_Color("Dot Color", Color) = (1,1,1,1)
		_Speed("Dot Speed", float) = 10
		_Opacity("Star Opacity", float) =1
		_OffX("X", float) = 0
		_OffY("Y", float) = 0

	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent" "DisableBatching" = "true"
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
			float _OffX;
			float _OffY;


			float4 frag(v2f i) : SV_Target
			{
				float2 baseWorldPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) ).xy;
				float2 OffsetPos = float2(_OffX,_OffY);


				float4 color1 = tex2D(_MainTex, i.uv);
				float4 color2 = tex2D(_DotTex, float2(i.uv.x * _DotCount - _DotCount * 0.5f + baseWorldPos.x * _Speed + OffsetPos.x,i.uv.y * _DotCount - _DotCount * 0.5f + baseWorldPos.y * _Speed + OffsetPos.y));
				//float4 color3 = tex2D(_DotTex, i.uv);

				float4 color = float4(
				color1.r - color2.a *(1-color2.r) * _Color.a,
				color1.g - color2.a *(1-color2.g) * _Color.a,
				color1.b - color2.a *(1-color2.b) * _Color.a,
				(color1.a * color2.a) * _Opacity);

				return color;
			}
			ENDCG
		}
	}
}