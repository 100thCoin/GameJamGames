Shader "Custom/TreeCutout" {
	Properties {
		//_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		 [NoScaleOffset] _MainTex ("Leaves", 2D) = "white" {}
        [NoScaleOffset] _MainTexX ("Sparkles", 2D) = "white" {}
        [NoScaleOffset] _MainTexS ("Stem", 2D) = "white" {}

        [NoScaleOffset] _Voronoi ("Voronoi", 2D) = "white" {}
        [NoScaleOffset] _Perlin ("Perlin", 2D) = "white" {}
        _pow ("Power", float) = 1
        _speedX ("Rotation Speed", float) = 1 

        _WindSize ("Wind Size", float) = 1 
        _WindSpeed ("Wind Speed", float) = 1 
        _WindForce ("Wind Force", float) = 1 

	}
	SubShader {
		Tags { "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		 #pragma surface surf Lambert alphatest:_Cutoff vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
            sampler2D _MainTexX;
            sampler2D _MainTexS;

            sampler2D _Voronoi;
            sampler2D _Perlin;

            float _pow;
            float _speedX;
            float _speedY;

            float _WindSize;
            float _WindSpeed;
            float _WindForce;
		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		 void vert(inout appdata_full v) {
                v.normal.xyz = v.normal;
 
            }

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color\

			 // sample texture and return it

                float x = _WindSize*IN.uv_MainTex.x + _Time.y*_WindSpeed*_WindSize;
                float Wind = (sin(sin(0.8*x)+0.45*x)+sin(x))*_WindForce;


                fixed4 Vor = tex2D(_Voronoi, float2(IN.uv_MainTex.x + Wind*0.5,IN.uv_MainTex.y));
                fixed4 Per = tex2D(_Perlin, float2(IN.uv_MainTex.x + _Time.y*_speedX,IN.uv_MainTex.y + _Time.y*_speedY));

                float R = Vor.r * 2 -1;
                float G = Vor.g * 2 -1;
                float B = R;

                float _Rad = 0;
                float rad = _Rad + _Time.y * _speedX;

                R = (R * cos(rad) + G * sin(rad)) / 1.41;
                G = (G * cos(rad) + B * sin(-rad)) / 1.41;

                R = ((R + 1) * 0.5);
                G = ((G + 1) * 0.5);


                fixed4 leaves = tex2D(_MainTex, float2(IN.uv_MainTex.x + _pow*( R-0.5) + Wind,IN.uv_MainTex.y + _pow*(G-0.5)));
                fixed4 sparkles = tex2D(_MainTexX, float2(IN.uv_MainTex.x + _pow*(R-0.5)*IN.uv_MainTex.y*3 + Wind*0.5*IN.uv_MainTex.y,IN.uv_MainTex.y + _pow*(G-0.5)*IN.uv_MainTex.y));

                fixed4 stem = tex2D(_MainTexS, float2(IN.uv_MainTex.x + Wind*0.8*IN.uv_MainTex.y ,IN.uv_MainTex.y));


                fixed4 ColorCompile = float4(leaves.rgb*leaves.a,leaves.a) + float4(stem.rgb*stem.a*(1-leaves.a),stem.a*(1-leaves.a));

                //return float4(ColorCompile);

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = ColorCompile.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = ColorCompile.a;
		}
		ENDCG
	}
	Fallback "Transparent/Cutout/VertexLit"
}
