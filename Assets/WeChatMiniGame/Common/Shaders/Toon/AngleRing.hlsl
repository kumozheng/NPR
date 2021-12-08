float3 offsetU_AR = lerp(mul(UNITY_MATRIX_V, float4(i.normalWS, 0)).xyz, float3(0, 0, 1), _AR_OffsetU);
float2 offsetV_AR = float2((offsetU_AR.r * 0.5 + 0.5), lerp(i.uv.g, (offsetU_AR.g * 0.5 + 0.5), _AR_OffsetV));
float4 angleRingTex = tex2D(_AngelRingTex, offsetV_AR);
float3 angleRingColor = angleRingTex.rgb * _AngelRingColor.rgb * lightColor;
float3 angleRingColorWithAlpha = angleRingColor * angleRingTex.a;
color = lerp(color + angleRingColor, (color * (1.0 - angleRingTex.a)) + angleRingColorWithAlpha, _ARTex_AlphaOn);
color = saturate(color);
