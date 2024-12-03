Shader "Custom/TextBend"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _BendAmount ("Bend Amount", Range(-1, 1)) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            float _BendAmount;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.uv = v.uv;

                // Apply bending to the text's y-axis (or x-axis)
                o.pos.y += sin(o.pos.x * 0.1) * _BendAmount; // Sine wave for bending
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                return tex2D(_MainTex, i.uv) * i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}