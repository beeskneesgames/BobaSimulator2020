Shader "BeesKneesGames/ClippingPlane" {
    Properties {
        _Albedo ("Albedo", Color) = (0, 0, 0, 1)
    }

    SubShader {
        Tags {
            "RenderType" = "TransparentCutout"
            "Queue" = "AlphaTest"
        }

        // Render faces regardless of whether they point towards the camera or
        // away from it.
        Cull Off

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows addshadow
        #pragma target 3.0

        // Unity Lighting properties
        fixed4 _Albedo;

        // Plane clipping properties
        float4 _Plane;

        // Automatically filled by Unity.
        struct Input {
            float3 worldPos;
        };

        void surf(Input input, inout SurfaceOutputStandard output) {
            // Calculate signed distance to plane
            float distance = dot(input.worldPos, _Plane.xyz);
            distance += _Plane.w;

            // Discard surface above plane
            clip(-distance);

            output.Albedo = _Albedo;
        }
        ENDCG
    }
    FallBack "Standard"
}
