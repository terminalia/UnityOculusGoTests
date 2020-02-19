﻿Shader "Mistwork/SphericalMaskDissolve"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _ActorPosition ("Actor Position", Vector) = (0, 0, 0, 0)
        _Radius ("Sphere Radius", Range(0, 100)) = 0
        _Softness ("Sphere Softness", Range(0, 100)) = 0
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _BurnSize ("Burn Size", Range(0.0, 2.0)) = 0.03
        _BurnColor ("Burn Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_DissolveTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        //Spherical Mask
        sampler2D _DissolveTex;
        float4 _ActorPosition;
        half _Radius;
        half _Softness;
        half _BurnSize;
        float4 _BurnColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            half d = distance(_ActorPosition, IN.worldPos);
            half effect_value = saturate((d - _Radius) / -_Softness);
            half dissolve_value = tex2D(_DissolveTex, IN.uv_DissolveTex).r;

            clip(effect_value - dissolve_value);

            if (effect_value - dissolve_value < _BurnSize)
                o.Emission = _BurnColor;
                
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
