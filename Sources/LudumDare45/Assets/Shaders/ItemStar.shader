Shader "Custom/ItemStar" {
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BColor("Bottom Color", Color) = (1,1,1,1)

		_WaveSize("Size", float) = 0
		//_WaveThick("Thickness", float) = 0

		_DotSize("PolkaDot Size",float) =0
		_DotCount2("Dot Count",float) = 10

		_RGB("Rainbow",float) = 0
		_Speed("Rainbow Speed",float) = 10

		_SpeedX("Dot Speed X",float) = 0
		_SpeedY("Dot Speed Y",float) = 0

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
			float4 _BColor;
			float _WaveSize;
			//float _WaveThick;

			float _RGB;

			float _Speed;
			float _SpeedY;
			float _SpeedX;

			float _DotSize;
			float _DotCount2;

			float4 frag(v2f i) : SV_Target
			{

				float4 Rainbow = float4((cos(_RGB)+0.5),(cos(_RGB + (2.0/3.0)*3.141592)+0.5),(cos(_RGB + (4.0/3.0)*3.141592)+0.5),1);


				float _DotCount = 6.3 / 8;
				float _Size = _WaveSize;

				float4 Dots = float4((cos(i.uv.xy * _DotCount - 0.395) - (_Size - 0.01)*1.05 - 2.1626),0,1);

				Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,1);
				//Dots = float4(Dots.r-8,Dots.g -8,Dots.b -8,1);
				//Dots = float4(Dots.r*80,Dots.g*80,Dots.b*80,1);

				//Dots = Dots * _Size;

				//if(Dots.r >1)
				//{
				//Dots = float4(1,1,1,0);
				//}
				if(Dots.r <0)
				{
				Dots = float4(0,0,0,0);
				}
				Dots = float4(Dots.r,Dots.g,Dots.b,Dots.r);


				Rainbow = float4(cos((Dots.r * _RGB + _Time.x * _Speed))/2.0 +0.5 + 0.1,(cos((Dots.r * _RGB + _Time.x * _Speed) + (2.0/3.0)*3.141592)/2.0+0.5)+0.1,(cos((Dots.r * _RGB + _Time.x * _Speed)+ (4.0/3.0)*3.141592)/2.0+0.5) + 0.1,1);

				//The rainbow part is complete, now to draw polka dots

				float FixDistanceX = (-_DotSize * 4 + 5.2); 
				float FixDistanceY = (-_DotSize * 0.5 + 5.2);

				//FixDistanceX = 0; FixDistanceY = 0; //  MATHY STUFF IF UNCOMMENTED  //


				Dots = float4(((cos(i.uv.x * _DotCount2 * 1 + (i.uv.y * _DotCount2) + (_Time.x * -_SpeedY - _DotCount2 * -0.48) * 10))  + (cos(i.uv.x * _DotCount2 * 1 - (i.uv.y * _DotCount2) + _Time.x * -_SpeedX * 10 - _DotCount2 * 0))) - FixDistanceX,( 1 ) - FixDistanceY,0,1);

				//return Dots; //  MATHY STUFF IS UNCOMMENTED  //


				Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,1);
				Dots = float4(Dots.r-8,Dots.g -8,Dots.b -8,1);
				Dots = float4(Dots.r*80,Dots.g*80,Dots.b*80,1);



				if(Dots.r >1)
				{
				Dots = float4(1,1,1,0);
				}
				if(Dots.r <0)
				{
				Dots = float4(0,0,0,0);
				}
				Dots = float4(Dots.r,Dots.g,Dots.b,Dots.r);



				//float4 Result = Rainbow;


				float4 Starshape = tex2D(_MainTex, i.uv);


				float4 Result = float4(
				Rainbow.r - (Dots.a * Rainbow.r) + _BColor.r * Dots.a,
				Rainbow.g - (Dots.a * Rainbow.g) + _BColor.g * Dots.a,
				Rainbow.b - (Dots.a * Rainbow.b) + _BColor.b * Dots.a,
				(Rainbow.a - (Dots.a * Rainbow.a) + _BColor.a * Dots.a) * Starshape.a
				);

				//Result = float4(Result.r * i.uv.x,Result.g * i.uv.y,0,Result.a);

				//Result = Rainbow;

				return Result;
			}
			ENDCG
		}
	}
}