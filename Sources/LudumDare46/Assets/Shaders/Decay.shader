Shader "Custom/Wither" 
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		_wither("wither", float) = 0
		_Flip("flip", int) = 0

	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			float _wither;
			int _Flip;
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = float4(0,0,0,0);

				if(_Flip != 1)
				{

				if(IN.texcoord.x + _wither <1)
				{
					c = SampleSpriteTexture (float2(IN.texcoord.x,IN.texcoord.y)) * IN.color;
				}
				else
				{
					//c = SampleSpriteTexture (float2(IN.texcoord.x,IN.texcoord.y +  _wither+(IN.texcoord.x)-1)) * IN.color;
					c = SampleSpriteTexture (float2(IN.texcoord.x,IN.texcoord.y +(pow(IN.texcoord.x+_wither-1,2))-0)) * IN.color;
				}

				c.rgb = c.rgb*saturate(1-_wither - IN.texcoord.x) + (c.r+c.g+c.b)/3.0 * saturate(_wither + IN.texcoord.x) ;
				}
				else
				{
					float wither = -_wither;

					if(IN.texcoord.x - wither >1)
					{
						c = SampleSpriteTexture (float2(IN.texcoord.x,IN.texcoord.y)) * IN.color;
					}
					else
					{
						c = SampleSpriteTexture (float2(IN.texcoord.x,IN.texcoord.y +(pow(IN.texcoord.x-wither-1,2))-0)) * IN.color;
					}
					c.rgb = c.rgb*saturate(1-(wither+1.8) + IN.texcoord.x) + (c.r+c.g+c.b)/3.0 * saturate((wither+1.8) - IN.texcoord.x) ;

				}

				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}