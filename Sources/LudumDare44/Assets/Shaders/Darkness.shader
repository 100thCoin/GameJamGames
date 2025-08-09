// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Darkness"
{
	Properties
	{

		_MainTex("Texture", 2D) = "white" {}

		_DotCount("Dot Count", float) = 1
		_TColor("Top Color", Color) = (1,1,1,1)
		_BColor("Bottom Color", Color) = (1,1,1,1)

		_Size("Dot Size", Range(0,1)) = 1
		_Blur("Sharpness", Range(0,2)) = 1

		_Slices("Slices", float) = 4


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
		//zWrite off
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


			float4 _TColor;
			float4 _BColor;
			float _DotCount;


			float _OffsetX[1024];
			float _OffsetY[1024];

			float _Size;

			float _Blur;
			int _Slices;




			float4 frag(v2f i) : SV_Target
			{







				float4 FinalDots = float4(
				_TColor.r,
				_TColor.g,
				_TColor.b,
				1
				);

				float4 Dots = float4(0,0,0,0);

				int iter = 0;

				while(iter < _DotCount)
				{
					float offx = _OffsetX[iter];
					float offy = _OffsetY[iter];

					float2 OffsetXY = float2(offx,offy);

					if(abs((i.uv.xy - 0.5 + OffsetXY).x) / _Size < 3.14 && abs((i.uv.xy - 0.5 + OffsetXY).y) / _Size < 3.14)
					{
						Dots = float4((cos((i.uv.xy - 0.5 + OffsetXY) / _Size)+1),0,0);
						Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,1);
						Dots = float4(Dots.r- _Blur*Dots.r,Dots.g- _Blur*Dots.g,Dots.b- _Blur*Dots.b,1);


						//Dots = float4(Dots.r-2 * _Blur,Dots.g -2 * _Blur,Dots.b -2 * _Blur,1);
						//Dots = float4(Dots.r*2 * _Blur,Dots.g*2 * _Blur,Dots.b*2 * _Blur,1);
					
						if(Dots.r >1)
						{
						Dots = float4(1,1,1,0);
						}
						if(Dots.r <0)
						{
						Dots = float4(0,0,0,0);
						}
						Dots = float4(Dots.r,Dots.g,Dots.b,Dots.r);

						FinalDots = float4(
						FinalDots.r,
						FinalDots.g,
						FinalDots.b,
						FinalDots.a  - (Dots.a)
						);

					}
					iter++;
				}

				//float4 test = tex2D(_MainTex,i.uv.xy);

				FinalDots = float4(
						FinalDots.r,
						FinalDots.g,
						FinalDots.b,
						(round(FinalDots.a * _Slices)/(0.0+_Slices)) * _TColor.a
						//FinalDots.a
						);

				return FinalDots;



			}
			ENDCG
		}
	}
}