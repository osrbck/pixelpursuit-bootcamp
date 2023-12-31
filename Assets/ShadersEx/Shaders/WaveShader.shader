Shader "BootCamp/WaveShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Amplitude ("Amplitude", float) = 0
        _Frenquency ("Frequency", float) = 0
        _Speed ("Speed",float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Amplitude;
            float _Frequency;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.y = _Amplitude * sin(v.vertex.xyz.z * v.vertex.xyz.x * _Frequency + _Speed * _Time.y);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
