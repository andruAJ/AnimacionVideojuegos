Shader "Jacobo/SH_FireSlashLit"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)

        _Dissolve ("Dissolve Amount", Range(0,1)) = 0
        _EdgeThickness ("Edge Thickness", Range(0,1)) = 0.15
        _EdgeColor ("Edge Color", Color) = (1,0.5,0,1)

        _EmissionIntensity ("Emission Intensity", Range(0,10)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv           : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;

            float4 _MainTex_ST;
            float4 _Color;
            float4 _EdgeColor;

            float _Dissolve;
            float _EdgeThickness;
            float _EmissionIntensity;

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv;

                // Dissolve direction de abajo hacia arriba
                float gradient = uv.y;

                // Noise
                float noise = tex2D(_NoiseTex, uv * 3).r;

                // Combinar noise + dirección
                float dissolveVal = gradient + noise * 0.25;

                // Calcular máscara de dissolve
                float mask = step(_Dissolve, dissolveVal);

                // EDGE
                float edge = smoothstep(_Dissolve, _Dissolve + _EdgeThickness, dissolveVal);
                float4 edgeCol = _EdgeColor * edge;

                // TEXTURA BASE + COLOR
                float4 tex = tex2D(_MainTex, uv) * _Color;

                // EMISSION
                float4 emission = edgeCol * _EmissionIntensity;

                float alpha = tex.a * mask;

                return float4(tex.rgb + emission.rgb, alpha);
            }

            ENDHLSL
        }
    }
}
