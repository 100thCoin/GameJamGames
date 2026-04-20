Shader "Unlit/SunRays"
{
Properties
	{
			_MainTex ("Texture", 2D) = "white" {}

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
			

			fixed4 frag (v2f i) : SV_Target
			{
				
				float grad = 0;
				float d = (i.uv.x-0.5)*(i.uv.x-0.5) + (i.uv.y-0.5)*(i.uv.y-0.5);
				d = sqrt(d)*0.5;
				if(d < (i.color.a) && d > (i.color.a)-0.5f)
				{
					grad = 1;
				}

				float4 col = float4(i.color.rgb,grad);

				return col;
			}
			ENDCG
		}
	}
}
