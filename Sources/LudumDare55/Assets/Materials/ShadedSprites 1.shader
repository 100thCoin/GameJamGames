Shader "Sprites/3D Environment Sprite" {
  Properties {
      _Color  ("Main Tint", Color) = (1,1,1,1)
      _MainTex ("Main Texture", 2D) = "white" {}
      _DetailCol ("Detail Tint", Color) = (1,1,1,1)
      _DetailTex ("Detail Texture", 2D) = "white" {}
      _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
      _EmissionCol ("Emission", Color) = (0,0,0,1)
     	_DotCount("Dot Count", float) = 11

		_Speed("Dot Speed", float) = 10
  }
  
  SubShader {
      Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" "PreviewType"="Plane"}
      Cull Off
      LOD 200
         ZWrite off

      CGPROGRAM
      #pragma surface surf SimpleLambert alphatest:_Cutoff addshadow fullforwardshadows Blend SrcAlpha OneMinusSrcAlpha
      #pragma target 3.0
 
      sampler2D _MainTex;
      fixed4 _Color ;
      sampler2D _DetailTex;
      fixed4 _DetailCol;
      fixed4 _EmissionCol;
      float _DotCount;
      float _Speed;

      half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
                half4 c;
                c.rgb = s.Albedo * _Color .rgb * (atten) * _LightColor0.rgb;
                c.a = s.Alpha;
                return c;
            }
 
      struct Input {
          float2 uv_MainTex;
          float2 uv_DetailTex;
          fixed4 color : COLOR;
      };
 
      void surf (Input IN, inout SurfaceOutput o) {
      		float yVal = IN.uv_MainTex.g*2-1;

      	  float4 Dots = float4((cos(IN.uv_MainTex.x * _DotCount - _Time.x * _Speed*10)+(yVal*2) + 2.16),(cos(IN.uv_MainTex.y * _DotCount*0.1 - _Time.x * _Speed*10)+(yVal*2) + 2.16),0,0);
      	  Dots = float4(Dots.r*Dots.g,Dots.r*Dots.g,Dots.r*Dots.g,0);
		  Dots = float4(Dots.r-10,Dots.g -10,Dots.b -10,0);
		  Dots = float4(Dots.r*100,Dots.g*100,Dots.b*100,0);

          fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color ;
          //fixed4 d = tex2D(_DetailTex, IN.uv_DetailTex) * _DetailCol;
          o.Albedo = c.rgb + _EmissionCol.rgb;
          o.Alpha = saturate(Dots.r);
      }
      ENDCG
  }
  Fallback "Transparent/Cutout/VertexLit"
  }