Shader "Unlit/NTSC"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_VerticalFalloff ("Vertical Mult Offset", float) = 1
		_Force ("Mult", float) = 1
		_RippleScale ("Ripple Scale", float) = 1
		_RippleScaleV ("Ripple Scale V", float) = 1
		_RippleSpeed("Ripple Speed", float) = 1
		_RippleOffset("Ripple Offset", float) = 1

		}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float2 GetModUV(float2 uv, float width, float height)
			{
				return float2(floor(uv.x*width)/width + 0.5/width,floor(uv.y*height)/height + 0.5/height);
			}

			float _Force;
			float _VerticalFalloff;
			float _RippleScale;
			float _RippleSpeed;
			float _RippleOffset;
			float _RippleScaleV;

			fixed4 frag (v2f i) : SV_Target
			{


				float textureWidth = _MainTex_TexelSize.z;
				float textureHeight = _MainTex_TexelSize.w;

				float2 modUV = GetModUV(i.uv, textureWidth, textureHeight);

				//float2 Ripples = + float2((sin(modUV.x*15 + modUV.y*15 + _Time.y) + sin(modUV.x*20 - modUV.y*20 + _Time.y*0.5)),0);
				float2 Ripples = + float2((sin(modUV.x*15*_RippleScale+_RippleOffset) + sin(modUV.x*28*_RippleScale + modUV.y*15*_RippleScale + _Time.y*0.5*_RippleSpeed+_RippleOffset)*-1 + sin(modUV.x*-22*_RippleScale + modUV.y*-21*_RippleScale*_RippleScaleV*5 + _Time.y*0.5*_RippleSpeed+_RippleOffset)*2 + sin(modUV.y*40*_RippleScale - _Time.y*0.5*_RippleSpeed+_RippleOffset)*3),0);
				float2 RippleMod = Ripples*(saturate((modUV.y+_VerticalFalloff)*(modUV.y+_VerticalFalloff)))*(0.04*_Force);
				//return float4(Ripples,0,1);
				//return float4(RippleMod*10,0,1);

				float4 col = tex2D(_MainTex, modUV + RippleMod);

				//return float4(modUV.x,modUV.y,0,1);
				//return float4(/*modUV + float2((sin(_Time.y+modUV.y*50)*0.1),0)*/ + float2(sin(modUV.x*15 + modUV.y*15 + _Time.y) + sin(modUV.x*20 - modUV.y*20 + _Time.y*0.5),0),0,1);

				return col;


			}
			ENDCG
		}
	}
}
