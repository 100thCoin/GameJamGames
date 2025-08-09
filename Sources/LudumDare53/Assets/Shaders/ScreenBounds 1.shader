Shader "Custom/ScreenBounds" {
Properties
	{


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
				float3 screen_uv : TEXCOORD1;

			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screen_uv = float3(o.vertex.xy, o.vertex.w);

				return o;
			}

			sampler2D _VoidTexture1;
			sampler2D _MainTex;



			float4 frag(v2f i) : SV_Target
			{


				float4 tex = tex2D(_VoidTexture1, float2(0.5*i.screen_uv.x+0.5,0.5*-i.screen_uv.y+0.5));


				return tex;
			}
			ENDCG
		}
	}
}