// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test/FlagWind2"
{
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
        [NoScaleOffset] _MainTex ("Leaves", 2D) = "white" {}


        _WindSize ("Wind Size", float) = 1 
        _WindSpeed ("Wind Speed", float) = 1 
        _WindForce ("Wind Force", float) = 1 
        _dist ("clamp", float) = 1 

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
                fixed4 color : COLOR;
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
                float4 vertex : SV_POSITION; // clip space position
                fixed4 color : COLOR;
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
                o.color = v.color;
                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;


            float _WindSize;
            float _WindSpeed;
            float _WindForce;

            float4 _Color;
            float _dist;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
                // sample texture and return it

                float x = _WindSize*(i.uv.y+i.uv.x*0.3) + _Time.y*_WindSpeed*_WindSize;
                float Wind = (sin(sin(0.8*x)+0.45*x)+sin(x))*_WindForce;


                fixed4 leaves = tex2D(_MainTex, float2(i.uv.x+ Wind * (-i.uv.y+_dist),i.uv.y));

                return leaves;

            }
            ENDCG
        }
    }
}