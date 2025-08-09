// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test/LivingTree3"
{
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset] _Voronoi ("Voronoi", 2D) = "white" {}
        [NoScaleOffset] _Perlin ("Perlin", 2D) = "white" {}
        _pow ("Power", float) = 1
        _speedX ("Speed X", float) = 1 
        _speedY ("Speed Y", float) = 1 

    }
    SubShader
    {
       Pass
		{
		zTest Always
		zWrite off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

            // vertex shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
            };

            // vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                // (multiply with model*view*projection matrix)
                o.vertex = UnityObjectToClipPos(v.vertex);
                // just pass the texture coordinate
                o.uv = v.uv;
                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;
            sampler2D _Voronoi;
            sampler2D _Perlin;

            float _pow;
            float _speedX;
            float _speedY;
            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture and return it
                fixed4 Vor = tex2D(_Voronoi, i.uv);
                fixed4 Per = tex2D(_Perlin, float2(i.uv.x + _Time.y*_speedX,i.uv.y + _Time.y*_speedY));

                float R = Vor.r * 2 -1;
                float G = Vor.g * 2 -1;
                float B = R;

                float _Rad = 0;
                float rad = _Rad + _Time.y * 2 * _speedX;

                R = (R * cos(rad) + G * sin(rad)) / 1.41;
                G = (G * cos(rad) + B * sin(-rad)) / 1.41;

                R = (R + 1) * 0.5;
                G = (G + 1) * 0.5;

                fixed4 col = tex2D(_MainTex, float2(i.uv.x + _pow*( R-0.5),i.uv.y + _pow*(G-0.5)));


                return float4(col.r,col.g,col.b,col.a);
            }
            ENDCG
        }
    }
}