// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
 
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
 
// Basic Test for World Spece (World Aligned)
 
Shader "Custom/WS Base (m)" {
    Properties {
 
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _SideTex ("Side Color", 2D) = "white" {}

        _MainTexF ("Base Color", 2D) = "white" {}
        _SideTexF ("Side Color", 2D) = "white" {}

          _MainTexF2 ("Base Color", 2D) = "white" {}
        _SideTexF2 ("Side Color", 2D) = "white" {}

        _UVs ("UV Scale", float) = 1.0
        _Glow ("glow", float) = 0

    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 300
       
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardSpecular fullforwardshadows vertex:vert
 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
 
        #include "UnityCG.cginc"
 
        sampler2D _MainTex;
        sampler2D _SideTex;
          sampler2D _MainTexF;
        sampler2D _SideTexF;
           sampler2D _MainTexF2;
        sampler2D _SideTexF2;
        fixed4 _Color;
        float _UVs;
        float _Glow;
//        float3 worldPos;
//        float3 worldNormal;
 
        struct Input {
 
                float2 uv_MainTex;
                float3 worldPos;
                float3 worldNormal;
 
        };
 
 
        //---- World Position Alignment Test ----
 
        void vert (inout appdata_full v) {
 
       //Texture Coordinates rotation and rescale
 
   
        }
 
 
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)
 
        void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
 
 
        //---- World Space (Aligned) Evaluation Here -----
 
            float3 Pos = IN.worldPos / (-1.0 * abs(_UVs) );

            float cutoff = saturate( IN.worldPos.z*0.5+12);
            float cutoff2 = saturate( -IN.worldPos.z*0.0525+4);

            float3 c00 = tex2D (_MainTex, IN.worldPos / 10);
 
            float3 c1 = tex2D ( _SideTex, -Pos.zy ).rgb;
            float3 c2 = tex2D ( _MainTex, -Pos.xz).rgb;
            float3 c3 = tex2D ( _SideTex, -Pos.xy).rgb;
 
            float alpha21 = abs (IN.worldNormal.x);
            float alpha23 = abs (IN.worldNormal.z);
 
            float3 c21 = lerp ( c2, c1, alpha21 ).rgb;
            float3 c23 = lerp ( c21, c3, alpha23 ).rgb;
 
            //---- Base Color Adjustment -----

            float3 c00F = tex2D (_MainTexF, IN.worldPos / 10);
 
            float3 c1F = tex2D ( _SideTexF, -Pos.zy ).rgb;
            float3 c2F = tex2D ( _MainTexF, -Pos.xz).rgb;
            float3 c3F = tex2D ( _SideTexF, -Pos.xy).rgb;
 
            float alpha21F = abs (IN.worldNormal.x);
            float alpha23F = abs (IN.worldNormal.z);
 
            float3 c21F = lerp ( c2F, c1F, alpha21F ).rgb;
            float3 c23F = lerp ( c21F, c3F, alpha23F ).rgb;


            // ---------------------------------


             float3 c00F2 = tex2D (_MainTexF2, IN.worldPos / 10);
 
            float3 c1F2 = tex2D ( _SideTexF2, -Pos.zy ).rgb;
            float3 c2F2 = tex2D ( _MainTexF2, -Pos.xz).rgb;
            float3 c3F2 = tex2D ( _SideTexF2, -Pos.xy).rgb;
 
            float alpha21F2 = abs (IN.worldNormal.x);
            float alpha23F2 = abs (IN.worldNormal.z);
 
            float3 c21F2 = lerp ( c2F2, c1F2, alpha21F2 ).rgb;
            float3 c23F2 = lerp ( c21F2, c3F2, alpha23F2 ).rgb;



 
            o.Albedo = ((c23 + _Glow) * cutoff + ((c23F + _Glow) * (1-cutoff))) * cutoff2 + (c23F2 * (1-cutoff2));
 
 
        }
        ENDCG
    }
    FallBack "Diffuse"
}