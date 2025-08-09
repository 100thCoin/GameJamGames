Shader "Unlit/SunRays"
{
Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_speed ("speed", float) = 0.2
		_scale ("scale", float) = 0.2
		_slant ("slant", float) = 0.2
		_Vis ("vis", float) = 0.2

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		Cull Off ZWrite Off ZTest Always
					Blend SrcAlpha OneMinusSrcAlpha

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
				half4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 wpos : TEXCOORD1;
				half4 color : COLOR;

			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.wpos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
				o.color = v.color;
				return o;
			}
			
			float _CellSize;
			float _Cutoff;

			float _speed;
			float _scale;
			float _slant;
			float _Vis;

			fixed4 frag (v2f i) : SV_Target
			{
				float mix = 0;
				int j = 0;
				while(j < 6)
				{
					float slant = (i.uv.x - j*100) + i.uv.y*_slant - i.uv.x*_slant;

					float pos = slant + _Time.x * _speed;
					float pos2 = slant + _Time.x * -1 * _speed;
					float pos3 = slant + _Time.x * 0.4 * _speed;
					float pos4 = slant + _Time.x * - 0.7 * _speed;

					float rays = sin(pos * 90*_scale + i.wpos.x) + sin(pos2 * 41*_scale + i.wpos.x) + sin(pos3 * 47*_scale + i.wpos.x) + sin(pos * 53*_scale + i.wpos.x) + sin(pos4* 81*_scale + i.wpos.x);
					float XMask = saturate(sin(slant*3.141592*2 - 3.141592/2));

					rays *= XMask;
					rays -= 1-i.uv.y;
					if(rays < 0)
					{
						rays = 0;
					}
					mix += rays*0.1;
					j++;
				}

				mix *= _Vis;


				float4 col = float4(1,1,1,mix*i.color.r);
				return col;
			}
			ENDCG
		}
	}
}
