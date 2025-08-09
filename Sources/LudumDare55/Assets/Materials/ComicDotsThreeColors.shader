// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ComicDotsThreeColors"
{
	Properties
	{
		_DotCount("Dot Count", float) = 11
		_TColor("Top Color", Color) = (1,1,1,1)
		_MColor("Mid Color", Color) = (1,1,1,1)
		_BColor("Bottom Color", Color) = (1,1,1,1)
		_Speed("Dot Speed", float) = 10
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
			cull off

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
			float4 _TColor;
			float4 _MColor;
			float4 _BColor;
			float _DotCount;
			float _Speed;



			float4 frag(v2f i) : SV_Target
			{
				//Documentation of my terrible spaghetti shader code

				//I used math instead of textures. At the same time, i was really just guessing and checking every step of the way.

				//Here's what i think is going on.
				// The Cos let's the pattern repeat.
				//It's making horizontal (red) and vertical (green) stripes. The dots are made from where the two stripes meet.


				//i.uv.xy * _DotCount draws more stripes the higher _DotCount is.
				// This draws a single stripe once for each _DotCount.
				// -_Time.x * _Speed makes it move diagonally based on the value of speed (multiplied by ten)
				// + i.uv.y * 2 makes the dots shrink depending on the Y value.
				// + 2.16 lines the dot sizes with the top and bottom of the plane. The smaller the value, the higher the dots will be

				float yVal = i.uv.g*2;


				float4 Dots = float4((cos(i.uv.xy * _DotCount - _Time.x * _Speed*10)+(yVal*2) + 2.16),0,0);

				//The following three lines fixes an opacity issue with the dots.
				//I multiplied the red and green values together to make the dots have a circular shape, but it's super blurry.
				//They were blending way too much. To solve this issue, I subtracted 10 from the values, making most of the blending < 0
				//From there, i multiplied everything by 100, so the positive values still appear, and the dots are now more than just a blur.
				//Side note, There is still a slight blur. This is intentional. The goal was making the dots actually dots instead of a circular blur.

				Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,0);
				Dots = float4(Dots.r-10,Dots.g -10,Dots.b -10,0);
				Dots = float4(Dots.r*100,Dots.g*100,Dots.b*100,0);

				//This fixes opacity issues down the road with the math involved in setting the BG color and Dot color.
				//It also sets the opacity of the dots to be something other than 0
				if(Dots.r >1)
				{
				Dots = float4(1,1,1,0);
				}
				if(Dots.r <0)
				{
				Dots = float4(0,0,0,0);
				}
				Dots = float4(Dots.r,Dots.g,Dots.b,Dots.r);

				//Make gradients


				float4 ColorGradiant1 = float4(
				(-yVal +1)*_MColor.r + (yVal)*_TColor.r,
				(-yVal+1)*_MColor.g + (yVal)*_TColor.g,
				(-yVal+1)*_MColor.b + (yVal)*_TColor.b,
				(-yVal+1)*_MColor.a + (yVal)*_TColor.a);

				float4 ColorGradiant2 = float4(
				(-yVal +1)*_BColor.r + (yVal)*_MColor.r,
				(-yVal+1)*_BColor.g + (yVal)*_MColor.g,
				(-yVal+1)*_BColor.b + (yVal)*_MColor.b,
				(-yVal+1)*_BColor.a + (yVal)*_MColor.a);

				//BG color - Dot Opacity * BgColor : If there's a dot, set the BG to 0 (also works with the blending of the dot)
				// + Dot Color * Dot Opacity : Replaces where then BG was with the following color. (also works with the blending stuff)

				float4 color = float4(
				ColorGradiant2.r - (Dots.a * ColorGradiant2.r) + ColorGradiant1.r * Dots.a,
				ColorGradiant2.g - (Dots.a * ColorGradiant2.g) + ColorGradiant1.g * Dots.a,
				ColorGradiant2.b - (Dots.a * ColorGradiant2.b) + ColorGradiant1.b * Dots.a,
				ColorGradiant2.a - (Dots.a * ColorGradiant2.a) + ColorGradiant1.a * Dots.a
				);


				return color;

				//Anywho, I think that's what's going on.

				//All that really matters is that it works, right?


			}
			ENDCG
		}
	}
}