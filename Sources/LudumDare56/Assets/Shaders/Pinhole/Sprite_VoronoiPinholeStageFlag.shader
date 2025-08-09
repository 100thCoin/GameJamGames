Shader "Custom/TreePinholes"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_ShadowColor ("Shadow Color", Color) = (1,1,1,1)
		_FogColor ("Fog Color", Color) = (0,0,0,1)


		_Tex_Base ("Stage Base", 2D) = "white" {}
		_Tex_Flag ("Flag", 2D) = "white" {}
		_Tex_Flag_Sway ("Flag Distort", 2D) = "white" {}
		_LeafSwayMagnitude ("Flag Sway Magnitude", Range(0, 5)) = 1


		_Color ("Color", Color) = (1,1,1,1)

		_Noise ("Noise Texture", 2D) = "white" {}
		_NoiseXOffset ("Noise X Offset", float) = 0 //modify this from a script that just changes the material, if using this for parallax.

		_PerlinScale("Perlin Scale", float) = 0.005
		_PerlinCut("Perlin Cut", float) = 0.55
		_PerlinMult("Perlin Mult", float) = 3

		_CellSize ("Cell Size", Range(0, 15)) = 1.7
		_CellBright ("Cell Bright", Range(0, 15)) = 1.4

		_CellSchmoveRadius ("Cell Schmove Radius", Range(0, 5)) = 0.5
		_CellSchmoveSpeed ("Cell Schmove Speed", Range(0, 5)) = 1.21
		_MaxIter ("Max Iterations", Range(0, 60)) = 60


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



			#include "Random.cginc"

		sampler2D _Tex_Base;
		sampler2D _Tex_Flag;
		sampler2D _Tex_Flag_Sway;

		float _LeafSwayMagnitude;

		float _CellSize;
		float _CellBright;

		float _CellSchmoveRadius;
		float _CellSchmoveSpeed;

		sampler2D _MainTex;
		sampler2D _Noise;

		float _PerlinScale;
		float _PerlinCut;
		float _PerlinMult;

		float _NoiseXOffset;

		float4 _ShadowColor;
		float4 _FogColor;

		float2 voronoiNoise(float3 value, float3 _off){
			value = value+_off;
			float3 baseCell = floor(value);

			//first pass to find the closest cell
			float minDistToCell = 10;
			float3 toClosestCell;
			float3 closestCell;
			for(int x1=-1; x1<=1; x1++){
				for(int y1=-1; y1<=1; y1++){
					for(int z1=-1; z1<=1; z1++){
						float3 cell = baseCell + float3(x1, y1, z1);
						float3 cellPosition = cell + rand3dTo3d(cell);
						float3 toCell = cellPosition - value;
						float distToCell = length(toCell);
						if(distToCell < minDistToCell){
							minDistToCell = distToCell;
							closestCell = cell - _off;

						}
					}
				}
			}

			float Perlin = tex2D(_Noise, float2(closestCell.x*_PerlinScale*_CellSize,closestCell.y*_PerlinScale*_CellSize)).r;
			Perlin = saturate((Perlin - _PerlinCut)*_PerlinMult);

    		return float2(minDistToCell,Perlin);
		}

		float _MaxIter;

		float Pinhole(v2f i)
		{
			float pins = 0;

			int j = 0;
			while(j < 60 && j < _MaxIter)
			{
				float3 _off = float3(cos(-j*50 + _Time.x*_CellSchmoveSpeed * (1+j*0.1))*_CellSchmoveRadius, sin(-j*50 + _Time.x*_CellSchmoveSpeed * (2+j*0.1))*_CellSchmoveRadius,0) + float3(j*100,j*100,0);

				float3 value = (i.wpos.xyz + float3(_NoiseXOffset,0,_Time.x*0.15)) / _CellSize;



				float2 noise = voronoiNoise(value,_off); // x is distance, y is using the perlin
				float dist = 1-((noise.x+0.5)*_CellBright) > 0 ? noise.y : 0;
				pins += dist;
				j++;
			}
			return saturate(pins);
		}

			fixed4 frag (v2f i) : SV_Target
			{

				float p = Pinhole(i)*1;

				float4 Tex = tex2D(_MainTex, i.uv);
				float4 trunk = tex2D(_Tex_Base, i.uv);
				float4 leaf_LUT = tex2D(_Tex_Flag_Sway, i.uv);
				float4 leaf_f = tex2D(_Tex_Flag, i.uv + float2(0,(sin(_Time.x*50 + leaf_LUT.g*1.5 + i.wpos.x*0.1)*(leaf_LUT.r*_LeafSwayMagnitude)*0.03 - 0.015f*leaf_LUT.r*_LeafSwayMagnitude)));

				float4 LayerCompile = (trunk*trunk.a)*saturate(1-leaf_f.a)+leaf_f*leaf_f.a;
				float4 col = float4(LayerCompile.rgb* (0.75+p),LayerCompile.a);
				col = float4(col.r * lerp(_ShadowColor.r,1,p), col.g * lerp(_ShadowColor.g,1,p), col.b * lerp(_ShadowColor.b,1,p), col.a);

				col = float4(col.r*i.color.r + _FogColor.r,col.g*i.color.g + _FogColor.g,col.b*i.color.b + _FogColor.b,col.a*i.color.a);

				return col;
			}
			ENDCG
		}
	}
}
