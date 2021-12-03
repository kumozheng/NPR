// Rotate & Scale
float2 uvMatcapView = mul(UNITY_MATRIX_V, float4(normalWS, 0)).rg * 0.5 + 0.5;
float2 scaleUV_MC = (uvMatcapView - _Tweak_MatCapUV)/ (1.0 - 2.0 * _Tweak_MatCapUV);
float2 uv_Rot_MC = rotateUV(scaleUV_MC, float2(0.5, 0.5), _Rotate_MatCapUV * PI);
float4 matcapTex = tex2D(_MatCapTex, uv_Rot_MC);
float3 matcapColor = matcapTex.rgb * _MatCapColor.rgb * lightColor;
// Affect by shadow
float3 matcapWithShadow = matcapColor * (1.0 - shadowMask + shadowMask * _TweakMatCapOnShadow);
float3 matcap = lerp(matcapColor, matcapWithShadow, _Is_UseTweakMatCapOnShadow);
// Mask
float4 matcapMask = tex2D(_MatCapMask,i.uv);
float matcapMaskLevel = saturate(matcapMask.g + _Tweak_MatcapMaskLevel);
// Add Mode
float3 matcapAddMode =color + matcap * matcapMaskLevel;
color = matcapAddMode;


