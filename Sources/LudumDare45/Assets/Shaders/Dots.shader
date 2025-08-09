Shader "Custom/Dots" 
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Top Color", Color) = (1,1,1,1)
		_Color2("Bottom Color", Color) = (1,1,1,1)

		_SpeedX("Speed X",float)=0.5
		_SpeedY("Speed Y",float)=0.5

		_Height("Height",float)=1

		_Size("Size",float)=0.5

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
			float4 _Color;
			float4 _Color2;
			float _Size;
			float _SpeedX;
			float _SpeedY;
			float _Height;

			float4 frag(v2f i) : SV_Target
			{

				float _Scale = i.uv.y / _Height;
				float4 color1 = float4(cos(i.uv.x*2*3.1415926*_Size + _SpeedX*_Time.y)*0.5-0.5+(_Scale+1),cos(i.uv.y*2*3.1415926*_Size + _SpeedY*_Time.y)*0.5-0.5+(_Scale+1),0,1);
				color1 = clamp(color1,0,2);
				float4 color2 = float4(color1.r*color1.g,0,0,1);
				float4 color3 = float4((color2.r-0.999)*1000,0,0,1);
				color3 = clamp(color3,0,1);
				float4 colorCompile = float4(
				color3.r*_Color.r + (1-color3.r)*_Color2.r,
				color3.r*_Color.g + (1-color3.r)*_Color2.g,
				color3.r*_Color.b + (1-color3.r)*_Color2.b,
				color3.r*_Color.a + (1-color3.r)*_Color2.a
				);

				return colorCompile;
			}
			ENDCG
		}
	}
}