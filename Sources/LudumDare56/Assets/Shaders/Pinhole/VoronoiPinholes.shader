Shader "Custom/TreePinholes"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PerlinScale("Perlin Scale", float) = 0.5
		_PerlinCut("Perlin Cut", float) = 0.5
		_PerlinMult("Perlin Mult", float) = 0.5

		_CellSize ("Cell Size", Range(0, 15)) = 3.82
		_CellBright ("Cell Bright", Range(0, 15)) = 1.21

		_CellSchmoveRadius ("Cell Schmove Radius", Range(0, 5)) = 0.5
		_CellSchmoveSpeed ("Cell Schmove Speed", Range(0, 5)) = 0.5
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



			#include "Random.cginc"

		float _CellSize;
		float _CellBright;

		float _CellSchmoveRadius;
		float _CellSchmoveSpeed;

		sampler2D _MainTex;

		float _PerlinScale;
		float _PerlinCut;
		float _PerlinMult;

		struct Input {
			float3 worldPos;
		};

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

			float Perlin = tex2D(_MainTex, float2(closestCell.x*_PerlinScale*_CellSize,closestCell.y*_PerlinScale*_CellSize)).r;
			Perlin = saturate((Perlin - _PerlinCut)*_PerlinMult);

    		return float2(minDistToCell,Perlin);
		}

		float Pinhole(v2f i)
		{
			float pins = 0;

			int j = 0;
			while(j < 60)
			{
				float3 _off = float3(cos(-j*50 + _Time.x*_CellSchmoveSpeed * (1+j*0.1))*_CellSchmoveRadius, sin(-j*50 + _Time.x*_CellSchmoveSpeed * (2+j*0.1))*_CellSchmoveRadius,0) + float3(j*100,j*100,0);

				float3 value = (i.wpos.xyz + float3(0,0,_Time.x*0.15)) / _CellSize;



				float2 noise = voronoiNoise(value,_off); // x is distance, y is using the perlin
				float dist = 1-((noise.x+0.5)*_CellBright) > 0 ? noise.y : 0;
				pins += dist;
				j++;
			}
			return pins;
		}

			fixed4 frag (v2f i) : SV_Target
			{

				float p = Pinhole(i);
				float4 col = float4(p,p,p,1);
				return col;
			}
			ENDCG
		}
	}
}
