Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range (.001, 0.03)) = .005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
        };

        sampler2D _MainTex;
        fixed4 _OutlineColor;
        float _OutlineWidth;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // If the object is at the silhouette edge, draw the outline
            if (dot(normalize(IN.worldNormal), float3(0,1,0)) < 0.01)
            {
                o.Albedo = _OutlineColor.rgb;
                o.Alpha = _OutlineColor.a;
                return;
            }
            else
            {
                // Otherwise, draw the regular texture
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
