Shader "Unlit/NTSC"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha ("Alpha",float) = 0.5
		_Speed("Speed", float) = 1

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


			float _Speed;

			float _Alpha;

			fixed4 frag (v2f i) : SV_Target
			{


				float textureWidth = _MainTex_TexelSize.z;
				float textureHeight = _MainTex_TexelSize.w;

				float2 modUV = GetModUV(float2((i.uv.x+_Time.x*_Speed) % 1,i.uv.y), textureWidth, textureHeight);


				float4 col = tex2D(_MainTex, modUV);

				//return float4(modUV.x,modUV.y,0,1);
				//return float4(/*modUV + float2((sin(_Time.y+modUV.y*50)*0.1),0)*/ + float2(sin(modUV.x*15 + modUV.y*15 + _Time.y) + sin(modUV.x*20 - modUV.y*20 + _Time.y*0.5),0),0,1);

				return float4(col.rgb,_Alpha);


			}
			ENDCG
		}
	}
}
