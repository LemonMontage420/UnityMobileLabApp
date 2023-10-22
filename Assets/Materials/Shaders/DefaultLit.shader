Shader "Custom/DefaultLit"
{
Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        [NoScaleOffset] _BumpMap ("Bump Map", 2D) = "black" {}
        _HeightScale ("Height Scale", Float) = 0.1
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        [Gamma] _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
 
        sampler2D _MainTex;
        sampler2D _BumpMap;
 
        struct Input 
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
            INTERNAL_DATA
        };
 
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        half _HeightScale;
 
        float3 HeightToNormal(float height, float3 normal, float3 pos)
        {
            float3 worldDirivativeX = ddx(pos);
            float3 worldDirivativeY = ddy(pos);
            float3 crossX = cross(normal, worldDirivativeX);
            float3 crossY = cross(normal, worldDirivativeY);
            float3 d = abs(dot(crossY, worldDirivativeX));
            float3 inToNormal = ((((height + ddx(height)) - height) * crossY) + (((height + ddy(height)) - height) * crossX)) * sign(d);
            inToNormal.y *= -1.0;
            return normalize((d * normal) - inToNormal);
        }
 
        float3 WorldToTangentNormalVector(Input IN, float3 normal) {
            float3 t2w0 = WorldNormalVector(IN, float3(1,0,0));
            float3 t2w1 = WorldNormalVector(IN, float3(0,1,0));
            float3 t2w2 = WorldNormalVector(IN, float3(0,0,1));
            float3x3 t2w = float3x3(t2w0, t2w1, t2w2);
            return normalize(mul(t2w, normal));
        }
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
 
            half h = tex2D(_BumpMap, IN.uv_MainTex).r * _HeightScale;
            IN.worldNormal = WorldNormalVector(IN, float3(0,0,1));
            float3 worldNormal = HeightToNormal(h, IN.worldNormal, IN.worldPos);
 
            o.Normal = WorldToTangentNormalVector(IN, worldNormal);
 
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
