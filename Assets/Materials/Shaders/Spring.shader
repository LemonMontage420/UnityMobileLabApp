Shader "Custom/Spring" {
    //show values to edit in inspector
    Properties {
        _Color ("Tint", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _Smoothness ("Smoothness", Range(0, 1)) = 0
        _Metallic ("Metalness", Range(0, 1)) = 0
        _SpringLinearity("Spring Linearity", Range(0.25, 4.0)) = 1.0
        _SpringLength("Spring Length", Range(0.0, 0.7)) = 0.0
    }
    SubShader 
    {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags{ "RenderType"="Opaque" "Queue"="Geometry"}

        CGPROGRAM

        //the shader is a surface shader, meaning that it will be extended by unity in the background 
        //to have fancy lighting and other features
        //our surface shader function is called surf and we use our custom lighting model
        //fullforwardshadows makes sure unity adds the shadow passes the shader might need
        //vertex:vert makes the shader use vert as a vertex shader function
        //addshadows tells the surface shader to generate a new shadow pass based on out vertex shader
        #pragma surface surf Standard fullforwardshadows vertex:vert addshadow
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;

        half _Smoothness;
        half _Metallic;
        float _SpringLength;
        float _SpringLinearity;

        //input struct which is automatically filled by unity
        struct Input 
        {
            float2 uv_MainTex;
            float3 localPos;
        };

        void vert (inout appdata_full v, out Input o) 
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.localPos = v.vertex.xyz;

            float scalingFactor = saturate(-o.localPos.y / 0.481763f); //Scale The Spring Scaling Factor To The 0-1 Range
            //_SpringDisplacement = (sin(_Time.y) / 4.0f) + ((1.0f / 4.0f) - (1.0f / 8.0f));
            scalingFactor = pow(scalingFactor, _SpringLinearity);
            o.localPos = float3(o.localPos.x, scalingFactor * -_SpringLength, o.localPos.z);

            v.vertex.xyz = o.localPos;
        }

        //the surface shader function which sets parameters the lighting function then uses
        void surf (Input i, inout SurfaceOutputStandard o) 
        {
            //sample and tint albedo texture
            fixed4 col = tex2D(_MainTex, i.uv_MainTex);
            col *= _Color;

            o.Albedo = col.rgb;
            //just apply the values for metalness, smoothness and emission
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Standard"
}