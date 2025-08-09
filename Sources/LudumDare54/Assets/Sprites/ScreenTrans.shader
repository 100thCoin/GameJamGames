Shader "Custom/ScreenTrans" {
Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Top Color", Color) = (1,1,1,1)

		_Size("Size",float)=0.5
		_Height("Height",float)=0.5

		_OffX("Offset X",float)=0.5
		_OffY("Offset Y",float)=0.5

		_StretchX("Stretch X",float)=1
		_StretchY("Stretch Y",float)=1
		_MoveY("Move Y",float)=0

		_Pos("Pos",float)=0.5
		_Schoep("Schoep",int)=0

		_Fill("Fill",int)=0
		_OutFill("Out Fill",int)=0
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
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}

			sampler2D _MainTex;
			float4 _Color;
			float _Size;
			float _Speed;
			float _Height;

			float _OffX;
			float _OffY;
			float _StretchX;
			float _StretchY;
			float _Pos;

			int _Schoep;

			int _Fill;
			int _OutFill;

			float _MoveY;

			float csin(float input)
			{
				float pi = 3.14159265;
				return (input > -1*pi && input < pi) ? sin(input) : 0;
			}
		

			float schoep(v2f i, float input)
			{
				//return input;
				return (abs(i.uv.x/_StretchX-_OffX/_StretchX) > abs(i.uv.y/_StretchY-_OffY/_StretchY)) ? input : 0;
			}

			float4 frag(v2f i) : SV_Target
			{

				float mov = _Pos;

				float stripes = saturate(

				saturate(round(csin(sqrt(pow((i.uv.x-_OffX)*_Size/_StretchX,2) + pow((i.uv.y-_OffY)*_Size/_StretchY,2))+mov)+_Height))
				-saturate(round((sqrt(pow((i.uv.x-_OffX)*_Size/_StretchX,2) + pow((i.uv.y-_OffY)*_Size/_StretchY,2))+mov -0.89459)+_Height))*(4*(1-_OutFill*2))

				+_Fill

				);
				if(_Schoep == 1)
				{
				 	stripes = schoep(i,stripes);
				}
				float alpha = tex2D(_MainTex, i.uv).a;

				float4 colorCompile = float4(_Color.r,_Color.g,_Color.b,_Color.a*alpha)*stripes;

				return colorCompile;
	
			}
			ENDCG
		}
	}
}