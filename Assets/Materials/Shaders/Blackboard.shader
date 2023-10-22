//TODO:
//Make Points Part Of Shader And Controllable Via C# Script
//Make Line Of Best Fit Part Of Shader And Controllable Via C# Script
//Add Ability To Tint Both Chalk And Blackboard Color

Shader "Custom/Blackboard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Blackboard Texture", 2D) = "white" {}
        _PointTex ("Point Texture", 2D) = "white" {}
        _LineTex ("Line Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _LineTint ("Line Tine", Color) = (1, 1, 1, 1)
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
        sampler2D _PointTex;
        sampler2D _LineTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_PointTex;
            float2 uv_LineTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half4 _PointCoords12;
        half4 _PointCoords34;
        half4 _PointVisibility;

        half2 _LineCoords;
        half _LineVisibility;
        float _LineAngle;
        float4 _LineTint;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 lineUV;
            float sinX = sin ((_LineAngle / 90.0f) * 1.570796f);
            float cosX = cos ((_LineAngle / 90.0f) * 1.570796f);
            float sinY = sin ((_LineAngle / 90.0f) * 1.570796f);
            float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
            IN.uv_LineTex = mul ( IN.uv_LineTex + _LineCoords, rotationMatrix );

            // Albedo comes from a texture tinted by color
            fixed4 base = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 points = (tex2D(_PointTex, IN.uv_PointTex + float2(_PointCoords12.x, _PointCoords12.y)).a * _PointVisibility.x) + (tex2D(_PointTex, IN.uv_PointTex + float2(_PointCoords12.z, _PointCoords12.w)).a * _PointVisibility.y) + (tex2D(_PointTex, IN.uv_PointTex + float2(_PointCoords34.x, _PointCoords34.y)).a * _PointVisibility.z) + (tex2D(_PointTex, IN.uv_PointTex + float2(_PointCoords34.z, _PointCoords34.w)).a * _PointVisibility.w);
            fixed4 chalkLine = (tex2D(_LineTex, IN.uv_LineTex + float2(0.02f, 0.02f))).a * _LineVisibility * _LineTint;
            fixed4 c = base + points + chalkLine;
            c = saturate(c);
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
