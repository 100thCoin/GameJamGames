Shader "Unlit/StripeTransition"
{
	Properties
	{
		_Slope ("Slope", float) = 1
		_XOffset ("X Offset", float) = 0
		_LineWeight ("Line Wight", float) = 0
		_Color("Color", color) = (0,0,0,1)

		_RoomCurrent ("Current Room", 2D ) = "white" {}
		_RoomNext ("Current Room", 2D ) = "white" {}

		_Horizontal ("Horizontal", float) = 0
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
                float4 worldPos : TEXCOORD1;
               	float4 screenPos : TEXCOORD2;

			};

			v2f vert(appdata v)
			{
				v2f o;
				o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _RoomCurrent;
			sampler2D _RoomNext;

			float4 _Color;

			float _Slope;
			float _XOffset;
			float _LineWeight;

			float _Horizontal;

			float4 frag(v2f i) : SV_Target
			{

				if(_Horizontal > 0.5)
				{
					fixed4 HCurrentRoom = tex2D(_RoomCurrent, float2(i.uv.x,i.uv.y));
					fixed4 HNextRoom = tex2D(_RoomNext, float2(i.uv.x,i.uv.y));

					float4 Hpos = (i.uv.y - _XOffset) - i.uv.x*_Slope > 0 ? HCurrentRoom : HNextRoom;

					float H_LineOffX = cos(atan2(_Slope,1))*-_LineWeight;
					float H_LineOffY = sin(atan2(_Slope,1))*-_LineWeight;

					float HOff1 = ((i.uv.y - _XOffset - H_LineOffX) - i.uv.x*_Slope > 0 ? 1 : 0) & ((i.uv.y - _XOffset + H_LineOffX) - i.uv.x*_Slope > 0 ? 0 : 1);


					float4 Hcol = float4(Hpos.r-HOff1,Hpos.g-HOff1,Hpos.b-HOff1,1);

					if(HOff1 == 1)
					{
						Hcol = _Color;
					}

					return Hcol;
				}

				fixed4 CurrentRoom = tex2D(_RoomCurrent, float2(i.uv.x,i.uv.y));
				fixed4 NextRoom = tex2D(_RoomNext, float2(i.uv.x,i.uv.y));

				float4 pos = (i.uv.x - _XOffset) - i.uv.y*_Slope > 0 ? CurrentRoom : NextRoom;

				float _LineOffX = cos(atan2(_Slope,1))*-_LineWeight;
				float _LineOffY = sin(atan2(_Slope,1))*-_LineWeight;

				float Off1 = ((i.uv.x - _XOffset - _LineOffX) - i.uv.y*_Slope > 0 ? 1 : 0) & ((i.uv.x - _XOffset + _LineOffX) - i.uv.y*_Slope > 0 ? 0 : 1);


				float4 col = float4(pos.r-Off1,pos.g-Off1,pos.b-Off1,1);

				if(Off1 == 1)
				{
					col = _Color;
				}

				return col;

			}
			ENDCG
		}
	}
}
