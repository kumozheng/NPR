#ifndef __TOON_OUTLINE_HLSL_
    #define __TOON_OUTLINE_HLSL_

    uniform sampler2D _BaseMap;
    uniform float4 _BaseColor;
    uniform float4 _Outline_Color;
    uniform float _Outline_Width;
    uniform float _Farthest_Distance;
    uniform float _Nearest_Distance;
    uniform float _Offset_Z;

    struct VertexInput {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float4 color : COLOR;
        float2 texcoord : TEXCOORD0;
    };
    
    struct VertexOutput {
        float4 pos : SV_POSITION;
        float2 uv : TEXCOORD0; 
        float3 color : TEXCOORD1;

    };

    float3x3 Adjugate(float3x3 m){
        return float3x3(
            (m[1][1] * m[2][2] - m[2][1] * m[1][2]),
            (m[0][2] * m[2][1] - m[0][1] * m[2][2]),
            (m[0][1] * m[1][2] - m[0][2] * m[1][1]),
            (m[1][2] * m[2][0] - m[1][0] * m[2][2]),
            (m[0][0] * m[2][2] - m[0][2] * m[2][0]),
            (m[1][0] * m[0][2] - m[0][0] * m[1][2]),
            (m[1][0] * m[2][1] - m[2][0] * m[1][1]),
            (m[2][0] * m[0][1] - m[0][0] * m[2][1]),
            (m[0][0] * m[1][1] - m[1][0] * m[0][1])
        );
    }

    float3 CalNormalVS(float3 normalOS){
        float3x3 matrix_at_mv = transpose(Adjugate(UNITY_MATRIX_MV));
        return normalize(mul((float3x3)matrix_at_mv, normalOS));
    }

    VertexOutput vert (VertexInput v) {
        VertexOutput o = (VertexOutput)0;
        o.uv = v.texcoord;

        float4 posCS = UnityObjectToClipPos(v.vertex);
        float objectDistanceToCamera = distance(mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz, _WorldSpaceCameraPos);
        float outlineWidth = 0.01 * _Outline_Width * smoothstep(_Farthest_Distance, _Nearest_Distance, objectDistanceToCamera);

        float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));
        float aspect = abs(nearUpperRight.y / nearUpperRight.x);

        #if defined(OUTLINE_NORMAL_IN_COLOR)
            float3 normalOS = v.color.xyz;

        #else
            float3 normalOS = v.normal.xyz;
        #endif
        
        // float3 normalVS = CalNormalVS(normalOS.xyz);// normalize(mul((float3x3)UNITY_MATRIX_IT_MV, normalOS.xyz));
        float3 normalVS = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, normalOS.xyz));
        float3 normalCS = normalize(TransformViewToProjection(normalVS.xyz)) * posCS.w;
        normalCS.x *= aspect;
        o.pos = float4(posCS.xy + outlineWidth * normalCS.xy, posCS.zw);

        float offset_Z = _Offset_Z * -0.01;
        float4 cameraPosCS = mul(UNITY_MATRIX_VP, float4(_WorldSpaceCameraPos.xyz, 1));
        o.pos.z = o.pos.z + offset_Z * cameraPosCS.z;
        o.color = normalCS;
        return o;
    }
    
    float4 frag(VertexOutput i) : SV_Target{
        float3 color = _Outline_Color.rgb;
        #if defined(USE_LIGHTING)
            float3 lightColor = _LightColor0.rgb;
            float4 baseMap = tex2D(_BaseMap, i.uv);
            color = color * baseMap.rgb * _BaseColor.rgb * lightColor;
        #endif
        return float4(color, 1.0);
    }



#endif