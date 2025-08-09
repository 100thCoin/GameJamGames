Shader "Custom/3DVoronoiNoise" {
	Properties {
		_CellSize ("Cell Size", Range(0, 5)) = 3.82
		_CellBright ("Cell Bright", Range(0, 5)) = 1.21

		_CellSchmoveRadius ("Cell Schmove Radius", Range(0, 5)) = 0.5
		_CellSchmoveSpeed ("Cell Schmove Speed", Range(0, 5)) = 0.5


	}
	SubShader {
		Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		#include "Random.cginc"

		float _CellSize;
		float _CellBright;

		float _CellSchmoveRadius;
		float _CellSchmoveSpeed;

		struct Input {
			float3 worldPos;
		};

		float3 voronoiNoise(float3 value){
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
							closestCell = cell;
							toClosestCell = toCell;
						}
					}
				}
			}

			//second pass to find the distance to the closest edge
			float minEdgeDistance = 10;
			for(int x2=-1; x2<=1; x2++){
				for(int y2=-1; y2<=1; y2++){
					for(int z2=-1; z2<=1; z2++){
						float3 cell = baseCell + float3(x2, y2, z2);
						float3 cellPosition = cell + rand3dTo3d(cell);
						float3 toCell = cellPosition - value;

						float3 diffToClosestCell = abs(closestCell - cell);
						bool isClosestCell = diffToClosestCell.x + diffToClosestCell.y + diffToClosestCell.z < 0.1;
						if(!isClosestCell){
							float3 toCenter = (toClosestCell + toCell) * 0.5;
							float3 cellDifference = normalize(toCell - toClosestCell);
							float edgeDistance = dot(toCenter, cellDifference);
							minEdgeDistance = min(minEdgeDistance, edgeDistance);
						}
					}
				}
			}

			float random = rand3dTo1d(closestCell);
    		return float3(minDistToCell, random, minEdgeDistance);
		}

		void surf (Input i, inout SurfaceOutputStandard o) {
			float3 _off = float3(cos(_Time.x*_CellSchmoveSpeed * 5)*_CellSchmoveRadius, sin(_Time.x*_CellSchmoveSpeed * 3)*_CellSchmoveRadius,0);

			float3 value = (i.worldPos.xyz + _off) / _CellSize;

			float3 noise = voronoiNoise(value);
			float3 dist = float3(noise.x,noise.x,noise.x);

			o.Albedo = 1-((dist+0.5)*_CellBright) > 0 ? 1 : 0;
		}
		ENDCG
	}
	FallBack "Standard"
}