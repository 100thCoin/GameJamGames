Shader "Replace/B&W Noise"
{
	Properties
	{
		_cMul1("Mul",Color) = (1,1,1,1)
		_cAdd("Add",Color) = (0,0,0,0)
		_cSub("Sub",Color) = (0,0,0,0)
		_cMul2("Mul2",Color) = (1,1,1,1)

	}

	SubShader
	{
		Tags { "Queue" = "Transparent+1" }

		GrabPass
		{
			"_GrabTexture1"
		}
		
		Pass
		{
			Cull off


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

				sampler2D _GrabTexture1;
				float _Refraction;
				float _Power;
				float _AlphaPower;

				float4 _cMul1;
				float4 _cAdd;
				float4 _cSub;
				float4 _cMul2;

				float4 frag(PS_INPUT i) : SV_Target
				{

					float4 Test = tex2Dproj(_GrabTexture1, i.vGrabPos);
					//float4 Grey = (1,1,(Test.r + Test.b + Test.g) / 3);

					//float4 math = float4((Test.b + Test.g) / 2,(Test.b + Test.r) / 2,(Test.r + Test.g) / 2,1);
					float4 math = float4((Test.r*_cMul1.r+_cAdd.r-_cSub.r)*_cMul2.r,
										 (Test.g*_cMul1.g+_cAdd.g-_cSub.g)*_cMul2.g,
										 (Test.b*_cMul1.b+_cAdd.b-_cSub.b)*_cMul2.b,
										 (Test.a*_cMul1.a+_cAdd.a-_cSub.a)*_cMul2.a);


					float4 BigTest = math;

					return BigTest;
				}
			ENDCG
		}
	}
}