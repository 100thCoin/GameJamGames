Shader "Custom/Welleffect" {
	Properties
	{
		_DotCount("Dot Count", float) = 11
		_Distance("Size", Range(0,1)) = 1
		_TColor("Top Color", Color) = (1,1,1,1)
		_BColor("Bottom Color", Color) = (1,1,1,1)
		_Speed("Speed", float) = 10

		_ModX("Mod X", float) = 1.6
		_ModY("Mod Y", float) = 1.6
		_ModZ("Mod Z", float) = 2

		_Type("Mod U", int) = 0

	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off Cull Off ZWrite Off Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			//zTest Always
			//zWrite off
			//Blend SrcAlpha OneMinusSrcAlpha

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

				//Documentation of my shader code

				//Everything above this line is from "Shaders 101" by Making Stuff Look Good.
				//The stuff inside float4 frag is by 100th_Coin 

				//vector4 (hex pattern, green horizontal lines, 1 1)

				//creating green lines using cos(i.uv.y * _DotCount)
				//creating a hexagon pattern with two intersecting lines. The intersecting spots end up squished vertically however, which is why we also made the green ones.

				//multiply the red and green values. Blamo, we got dots. They're a bit blurry, so i subtract ten and multiply by 100.
				//those two lines of code also throw off the 'distance' between dots, but by tweaking that they look good.
				//introduce the _Distance variable. I did the best i could to line 0 up with the dots being gone, and the max of the range is the other end of the spectrum. 

				//that's the gist of creating the dots

			float4 _TColor;
			float4 _BColor;
			float _DotCount;
			float _Distance;
			float _Speed;

			float _ModX;
			float _ModY;
			float _ModZ;

			float _Type;

			float4 frag(v2f i) : SV_Target
			{

				float FixDistanceX = (-_Distance * 4 + 5.2); 
				float FixDistanceY = (-_Distance * 0.5 + 5.2);

				//As explained a few lines later, uncomment this next line if you want to see the mathy stuff. then uncomment the return Dots down below.

				//FixDistanceX = 0; FixDistanceY = 0; //  MATHY STUFF IF UNCOMMENTED  //

				//                                                                                                                                       There's a super trippy effect by setting it to * _Dotcount * 1 here, instead of * 2. Also try -2. Cool stuff.
				float4 DotsP = float4((cos(i.uv.x * _DotCount * _ModX + (i.uv.y * _DotCount)) + (cos(i.uv.x * _DotCount * + _ModY - (i.uv.y * _DotCount + _Time.x * _Speed * 10)))) - FixDistanceX,(cos(i.uv.y * _DotCount * _ModZ + _Time.x * _Speed * 10) + 1 ) - FixDistanceY,0,1);
				float4 DotsM = float4((cos(i.uv.x * _DotCount * _ModX + (i.uv.y * _DotCount)) - (cos(i.uv.x * _DotCount * + _ModY - (i.uv.y * _DotCount + _Time.x * _Speed * 10)))) - FixDistanceX,(cos(i.uv.y * _DotCount * _ModZ + _Time.x * _Speed * 10) + 1 ) - FixDistanceY,0,1);
				float4 DotsL = float4((cos(i.uv.x * _DotCount * _ModX + (i.uv.y * _DotCount)) * (cos(i.uv.x * _DotCount * + _ModY - (i.uv.y * _DotCount + _Time.x * _Speed * 10)))) - FixDistanceX,(cos(i.uv.y * _DotCount * _ModZ + _Time.x * _Speed * 10) + 1 ) - FixDistanceY,0,1);
				float4 DotsD = float4((cos(i.uv.x * _DotCount * _ModX + (i.uv.y * _DotCount)) / (cos(i.uv.x * _DotCount * + _ModY - (i.uv.y * _DotCount + _Time.x * _Speed * 10)))) - FixDistanceX,(cos(i.uv.y * _DotCount * _ModZ + _Time.x * _Speed * 10) + 1 ) - FixDistanceY,0,1);
				float4 DotsR = float4((cos(i.uv.x * _DotCount * _ModX + (i.uv.y * _DotCount)) % (cos(i.uv.x * _DotCount * + _ModY - (i.uv.y * _DotCount + _Time.x * _Speed * 10)))) - FixDistanceX,(cos(i.uv.y * _DotCount * _ModZ + _Time.x * _Speed * 10) + 1 ) - FixDistanceY,0,1);

				float4 Dots = DotsP;

				if(_Type == 1)
				{
					Dots = DotsM;
				}
				else if (_Type == 2)
				{
					Dots = DotsL;
				}
				else if (_Type == 3)
				{
					Dots = DotsD;
				}
				else if (_Type == 4)
				{
					Dots = DotsR;
				}
				else
				{
					Dots = DotsP;
				}






				//If you want to see what all this math looks like before the magic happens, see the comments below FixFloatX = (...);
				//after that, remove all the stuff below this line, and replace return color with return Dots

				//return Dots; //  MATHY STUFF IS UNCOMMENTED  //


				Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,1);
				Dots = float4(Dots.r-10,Dots.g -10,Dots.b -10,1);
				Dots = float4(Dots.r*100,Dots.g*100,Dots.b*100,1);

				if(Dots.r >1)
				{
				Dots = float4(1,1,1,0);
				}
				if(Dots.r <0)
				{
				Dots = float4(0,0,0,0);
				}
				Dots = float4(Dots.r,Dots.g,Dots.b,Dots.r);

				//Those above lines of code are important, staring with if(dots.r > 1) 
				//There's a lot of math involved with making sure the BG color are replaced with the dot color, and numbers > 1 and < 0 throw it off.


				//if there is no dot, ( _Bcolor ) - ( 0 ) + ( 0 ) = _Bcolor
				//_if there is a dot, ( _Bcolor ) - ( _Bcolor ) + ( _TColor ) = _Tcolor

				float4 color = float4(
				_BColor.r - (Dots.a * _BColor.r) + _TColor.r * Dots.a,
				_BColor.g - (Dots.a * _BColor.g) + _TColor.g * Dots.a,
				_BColor.b - (Dots.a * _BColor.b) + _TColor.b * Dots.a,
				_BColor.a - (Dots.a * _BColor.a) + _TColor.a * Dots.a
				);

				return color;

			}
			ENDCG
		}
	}
}