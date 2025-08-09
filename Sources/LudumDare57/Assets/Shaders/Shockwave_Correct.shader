Shader "Unlit/Shockwave"
{
	Properties
	{
		_Temp ("Temp", float) = 2
		_Temp2 ("Temp2", float) = 2
		_Mult ("Mult", float) = 1

	}

	SubShader
	{
		Tags { "Queue" = "Transparent+1" }

		GrabPass
		{
			"_GrabTexture"
		}
		
		Pass
		{
			Cull [_Cull]

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
					float2 uv : TEXCOORD0;
					float4 vTangentUOs_flTangentVSign : TANGENT;
					float4 vColor : COLOR;
				};

				struct PS_INPUT
				{
					float4 vGrabPos : TEXCOORD0;
					float4 vPos : SV_POSITION;
					float4 vColor : COLOR;
					float2 uv : TEXCOORD1;
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
					o.uv.xy = i.uv.xy;

					// Color
					o.vColor = i.vColor;

					return o;
				}

				sampler2D _GrabTexture;
				float _Temp;
				float _Temp2;
				float _Mult;

				float4 frag(PS_INPUT i) : SV_Target
				{




					float M = abs(atan2(i.uv.y-0.5,i.uv.x-0.5)/(3.141592*2)+(0.5));
					float A = (sqrt((i.uv.x-0.5)*(i.uv.x-0.5) + (i.uv.y-0.5)*(i.uv.y-0.5))*_Temp + (_Temp2*-1 + 2.4));

					float deriv = 0;

					A = 2.414213562373-A;
					float modA = 0;
					if(A < 0 || A > 2.414213562373)
					{
						modA = 0;
					}
					else if(A > 0 && A < (1.70711))
					{
						modA = -(A-1)*(A-1) + 1;
						deriv = -2*A+2;
					}
					else if(A < (2.414213562373))
					{
						modA = (A-2.414213562373)*(A-2.414213562373);
						deriv = 2*A - (2.414213562373*2);
					}


					float M2 = abs(atan2(i.uv.y-0.5,i.uv.x-0.5)/(3.141592*2)+(0.25));

					float NormR = M*2;
					if(NormR > 1)
					{
						NormR = 2-NormR;
					}

					float NormG = M2*2;
					if(NormG > 1)
					{
						NormG = 2-NormG;
					}


					if(deriv < 0)
					{
						NormR = 0.5;
						NormG = 0.5;
					}

					NormR = (NormR-0.5)*2; // convert from a range of [0,1]
					NormG = (NormG-0.5)*2; // to a range of [-1,1]

					NormR*=abs(deriv);
					NormG*=abs(deriv);



					float2 uvs = float2(NormR,NormG);

					//float4 col = float4(abs(uvs),0,1);

					//return col;

					// Sample grab texture
					float4 Grab = tex2Dproj(_GrabTexture, i.vGrabPos + float4(_Mult*uvs,0,0));

					return Grab;
				}
			ENDCG
		}
	}
}