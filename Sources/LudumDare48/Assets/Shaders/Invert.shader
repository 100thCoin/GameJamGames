Shader "Replace/Invert"
{
	Properties
	{
		//_Refraction ("Refraction", Range (0.00, 10.0)) = 1.0
		//_Power ("Power", Range (1.00, 10.0)) = 1.0
		//_AlphaPower ("Vertex Alpha Power", Range (1.00, 10.0)) = 1.0
		_BumpMap( "Img", 2D ) = "bump" {}

		//_Cull ( "Face Culling", Int ) = 2
	}

	SubShader
	{
		Tags { "Queue" = "Transparent+1" }

		GrabPass
		{
			"_GrabTexture4"
		}
		
		Pass
		{
			Cull off
			Ztest Always
			zwrite off
			LOD 10

			CGPROGRAM
				#pragma target 3.0
				#pragma vertex vert
				#pragma fragment frag 
				#include "UnityCG.cginc"
				#include "UnityLightingCommon.cginc"
				#include "UnityStandardUtils.cginc"
				#include "UnityStandardInput.cginc"

				float3 Vec3TsToWs( float3 vVectorTs, float3 vNormalWs, float3 vTangentUWs, float3 vTangentVWs )
				{
					float3 vVectorWs;
					vVectorWs.xyz = vVectorTs.x * vTangentUWs.xyz;
					vVectorWs.xyz += vVectorTs.y * vTangentVWs.xyz;
					vVectorWs.xyz += vVectorTs.z * vNormalWs.xyz;
					return vVectorWs.xyz; // Return without normalizing
				}

				float3 Vec3TsToWsNormalized( float3 vVectorTs, float3 vNormalWs, float3 vTangentUWs, float3 vTangentVWs )
				{
					return normalize( Vec3TsToWs( vVectorTs.xyz, vNormalWs.xyz, vTangentUWs.xyz, vTangentVWs.xyz ) );
				}

				struct VS_INPUT
				{
					float4 vPosition : POSITION;
					float3 vNormal : NORMAL;
					float2 vTexcoord0 : TEXCOORD0;
					float4 vTangentUOs_flTangentVSign : TANGENT;
					float4 vColor : COLOR;
				};

				struct PS_INPUT
				{
					float4 vGrabPos : TEXCOORD0;
					float4 vPos : SV_POSITION;
					float4 vColor : COLOR;
					float2 vTexCoord0 : TEXCOORD1;
					float3 vNormalWs : TEXCOORD2;
					float3 vTangentUWs : TEXCOORD3;
					float3 vTangentVWs : TEXCOORD4;
				};

				PS_INPUT vert(VS_INPUT i)
				{
					PS_INPUT o;
					
					// Clip space position
					o.vPos = UnityObjectToClipPos(i.vPosition);
					
					// Grab position
					o.vGrabPos = ComputeGrabScreenPos(o.vPos);
					
					// World space normal
					o.vNormalWs = UnityObjectToWorldNormal(i.vNormal);

					// Tangent
					o.vTangentUWs.xyz = UnityObjectToWorldDir( i.vTangentUOs_flTangentVSign.xyz ); // World space tangentU
					o.vTangentVWs.xyz = cross( o.vNormalWs.xyz, o.vTangentUWs.xyz ) * i.vTangentUOs_flTangentVSign.w;

					// Texture coordinates
					o.vTexCoord0.xy = i.vTexcoord0.xy;

					// Color
					o.vColor = i.vColor;

					return o;
				}

				sampler2D _GrabTexture4;
				float _Refraction;
				float _Power;
				float _AlphaPower;

				float4 frag(PS_INPUT i) : SV_Target
				{

					float4 Test = tex2Dproj(_GrabTexture4, i.vGrabPos);
					//float4 Grey = (1,1,(Test.r + Test.b + Test.g) / 3);

					float4 InvertColors = float4(1-Test.r,1-Test.g,1-Test.b,1);



					float4 BigTest = InvertColors;

					return BigTest;
				}
			ENDCG
		}
	}
}