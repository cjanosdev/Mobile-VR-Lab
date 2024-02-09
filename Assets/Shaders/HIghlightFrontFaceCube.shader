Shader "Custom/HIghlightFrontFaceCube"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _HighlightColor ("Highlight Color", Color) = (1,1,0,1) // Yellow highlight color
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
            };

            float4 _Color;
            float4 _HighlightColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal); // Convert normal to world space
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float3 worldNormal = normalize(i.normal);
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.pos.xyz);
                
                // Calculate dot product of normal and view direction
                float ndotv = dot(worldNormal, viewDir);

                // Check if the dot product is positive, which indicates the front face
                if (ndotv > 0)
                {
                    // Apply highlight color
                    return _HighlightColor;
                }
                else
                {
                    // Apply base color
                    return _Color;
                }
            }
            ENDCG
        }
    }
}
