Shader "Custom/TreePinholes"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PerlinScale("Perlin Scale", float) = 0.5
		_PerlinCut("Perlin Cut", float) = 0.5
		_PerlinMult("Perlin Mult", float) = 0.5


		_TestFalloff("Test F", float) = 0.5

		_TestPx("Test x", float) = 0.5
		_TestPy("Test y", float) = 0.5
		_TestScale("Test S", float) = 0.5
		_TestRad("Test R", float) = 0.5
		_TestVel("Test V", float) = 0.5


	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 wpos : TEXCOORD1;

			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.wpos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
				return o;
			}



			sampler2D _MainTex;

			float _PerlinScale;
			float _PerlinCut;
			float _PerlinMult;

			float _TestPx;
			float _TestPy;
			float _TestScale;
			float _TestRad;
			float _TestVel;
			float _TestFalloff;

			fixed4 frag (v2f i) : SV_Target
			{
				
				//float2 GridUV = float2((i.uv.x/abs(_CellSize))%1,(i.uv.y/abs(_CellSize))%1);

				float2 p_off = float2(_TestPx + cos(_Time.x * _TestVel * 3)*_TestRad, _TestPy + sin(_Time.x * _TestVel * 4)*_TestRad);

				float dSq = saturate(1-((p_off.x - i.wpos.x)*(p_off.x - i.wpos.x)*_TestScale + (p_off.y - i.wpos.y)*(p_off.y - i.wpos.y)*_TestScale));

				float Perlin = tex2D(_MainTex, float2(i.wpos.x*_PerlinScale,i.wpos.y*_PerlinScale)).r;
				Perlin = saturate((Perlin - _PerlinCut)*_PerlinMult);
				float Perlin2 = tex2D(_MainTex, float2(p_off.x*_PerlinScale,p_off.y*_PerlinScale)).r;
				Perlin2 = saturate((Perlin2 - _PerlinCut)*_PerlinMult);


				float lumMap = saturate(1-((i.wpos.x)*(i.wpos.x)*_TestFalloff + (i.wpos.y)*(i.wpos.y)*_TestFalloff));
				//float lum = saturate(1-((p_off.x)*(p_off.x)*_TestFalloff + (p_off.y)*(p_off.y)*_TestFalloff));
				float lum = Perlin2;

				float pin = dSq > 0 ? lum : 0;


				float4 col = float4(pin,pin,pin,1);
				return col;
			}
			ENDCG
		}
	}
}
