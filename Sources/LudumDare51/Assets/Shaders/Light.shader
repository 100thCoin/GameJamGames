Shader "Unlit/Checkerboard"
{
    Properties
    {
		_Col ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
    	cull front
		Lighting Off  Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Col;

            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(pos);
                o.uv = uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float4 c = float4(_Col.rgb,i.uv.y);
                return c;
            }
            ENDCG
        }
    }
}