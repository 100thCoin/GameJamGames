Shader "Custom/WaterCaustics"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorLUT ("Color LUT", 2D) = "white" {}

		_CellScaleX ("Cell Scale X", Range(0, 15)) = 0.15
		_CellScaleY ("Cell Scale Y", Range(0, 15)) = 0.15
		_CellScaleZ ("Cell Scale Z", Range(0, 15)) = 1.5

		_CellOffsetZ ("Cell Offset Z", Range(0, 15)) = 0


		_CellSpeedX ("Cell Speed X", Range(0, 15)) = 0.79
		_CellSpeedY ("Cell Speed Y", Range(0, 15)) = 0.16
		_CellSpeedZ ("Cell Speed Z", Range(0, 15)) = 6.71

		_Thin ("Thinning", Range(0, 15)) = 7.18

		_FalloffY ("Falloff Y Pos", Range(-1, 1)) = -0.59

		_RippleFalloff ("Ripple Vertical Mult Offset", float) = 0.5
		_RippleForce ("Ripple Mult", float) = 0.21
		_RippleScale ("Ripple Scale", float) = 0.43
		_RippleScaleV ("Ripple Scale V", float) = 1
		_RippleSpeed("Ripple Speed", float) = 7
		_RippleOffset("Ripple Offset", float) = 0
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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



			#ifndef WHITE_NOISE
			#define WHITE_NOISE

			//to 1d functions


			float rand4dTo1d(float4 value, float4 dotDir = float4(12.9898, 78.233, 37.719, 17.4265)){
				float4 smallValue = cos(value);
				float random = dot(smallValue, dotDir);
				random = frac(sin(random) * 143758.5453);
				return random;
			}

			//get a scalar random value from a 3d value
			float rand3dTo1d(float3 value, float3 dotDir = float3(12.9898, 78.233, 37.719)){
				//make value smaller to avoid artefacts
				float3 smallValue = cos(value);
				//get scalar value from 3d vector
				float random = dot(smallValue, dotDir);
				//make value more random by making it bigger and then taking the factional part
				random = frac(sin(random) * 143758.5453);
				return random;
			}

			float rand2dTo1d(float2 value, float2 dotDir = float2(12.9898, 78.233)){
				float2 smallValue = cos(value);
				float random = dot(smallValue, dotDir);
				random = frac(sin(random) * 143758.5453);
				return random;
			}

			float rand1dTo1d(float3 value, float mutator = 0.546){
				float random = frac(sin(value + mutator) * 143758.5453);
				return random;
			}

			//to 2d functions

			float2 rand3dTo2d(float3 value){
				return float2(
					rand3dTo1d(value, float3(12.989, 78.233, 37.719)),
					rand3dTo1d(value, float3(39.346, 11.135, 83.155))
				);
			}

			float2 rand2dTo2d(float2 value){
				return float2(
					rand2dTo1d(value, float2(12.989, 78.233)),
					rand2dTo1d(value, float2(39.346, 11.135))
				);
			}

			float2 rand1dTo2d(float value){
				return float2(
					rand2dTo1d(value, 3.9812),
					rand2dTo1d(value, 7.1536)
				);
			}

			//to 3d functions

			float3 rand3dTo3d(float3 value){
				return float3(
					rand3dTo1d(value, float3(12.989, 78.233, 37.719)),
					rand3dTo1d(value, float3(39.346, 11.135, 83.155)),
					rand3dTo1d(value, float3(73.156, 52.235, 09.151))
				);
			}

			float3 rand2dTo3d(float2 value){
				return float3(
					rand2dTo1d(value, float2(12.989, 78.233)),
					rand2dTo1d(value, float2(39.346, 11.135)),
					rand2dTo1d(value, float2(73.156, 52.235))
				);
			}

			float3 rand1dTo3d(float value){
				return float3(
					rand1dTo1d(value, 3.9812),
					rand1dTo1d(value, 7.1536),
					rand1dTo1d(value, 5.7241)
				);
			}

			// to 4d // TEMP
			float4 rand4dTo4d(float4 value){
				return float4(
					rand4dTo1d(value, float4(12.989, 78.233, 37.719, -12.15)),
					rand4dTo1d(value, float4(39.346, 11.135, 83.155, -11.44)),
					rand4dTo1d(value, float4(73.156, 52.235, 09.151, 62.463)),
					rand4dTo1d(value, float4(-12.15, 12.235, 41.151, -1.135))
				);
			}

			#endif

			float _CellScaleX;
			float _CellScaleY;
			float _CellScaleZ;

			float _CellOffsetZ;

			float _CellSpeedX;
			float _CellSpeedY;
			float _CellSpeedZ;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			float _FalloffY;

			float _Thin;

			sampler2D _ColorLUT;

			float _RippleFalloff;
			float _RippleForce;
			float _RippleScale;
			float _RippleScaleV;
			float _RippleSpeed;
			float _RippleOffset;

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

				return float2(minDistToCell,minEdgeDistance);
			}

			float2 GetModUV(float2 uv, float width, float height)
			{
				return float2(floor(uv.x*width)/width + 0.5/width,floor(uv.y*height)/height + 0.5/height);
			}

			float GetModUVZ(float z, float height)
			{
				return floor(z*height)/height + 0.5/height;
			}

			fixed4 frag (v2f i) : SV_Target
			{


				float textureWidth = _MainTex_TexelSize.z;
				float textureHeight = _MainTex_TexelSize.w;

				float2 modUV = GetModUV(i.uv, textureWidth, textureHeight);

				float d = (1-(((modUV.x-0.5)*(modUV.x-0.5) + (modUV.y-0.5-_FalloffY)*(modUV.y-0.5-_FalloffY)))*4);


				float2 Ripples = + float2((sin(modUV.x*15*_RippleScale+_RippleOffset) + sin(modUV.x*28*_RippleScale + modUV.y*15*_RippleScale + _Time.y*0.5*_RippleSpeed+_RippleOffset)*-1 + sin(modUV.x*-22*_RippleScale + modUV.y*-21*_RippleScale*_RippleScaleV*5 + _Time.y*0.5*_RippleSpeed+_RippleOffset)*2 + sin(modUV.y*40*_RippleScale - _Time.y*0.5*_RippleSpeed+_RippleOffset)*3),0);
				float2 RippleMod = Ripples*(saturate((modUV.y+_RippleFalloff)*(modUV.y+_RippleFalloff)))*(0.04*_RippleForce);

				float3 value = float3((modUV.x+_CellSpeedX*_Time.x + RippleMod.x)/_CellScaleX,(modUV.y+_CellSpeedY*_Time.x + RippleMod.y)/_CellScaleY,(_CellSpeedZ*_Time.x + _CellOffsetZ)/_CellScaleZ);
				float2 noise = voronoiNoise(value,float3(0,0,0)); // x is distance, y is using the perlin

				float N = saturate(noise.r*d);

				N *= (1-noise.g*_Thin);

				float4 col = tex2D(_ColorLUT, float2((1-N)*(1-N),0.5));

				//col = float4(N,N,N,1);
				//col = float4(noise.r,noise.r,noise.r,1);

				return col;
			}
			ENDCG
		}
	}
}
