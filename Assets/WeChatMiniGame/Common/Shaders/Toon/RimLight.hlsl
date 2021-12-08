// Mask
float rimLightMask = tex2D(_Set_RimLightMask, i.uv).g;
rimLightMask =  saturate(rimLightMask + _Tweak_RimLightMaskLevel);
float3 rimLightColor = lerp(_RimLightColor.rgb, _RimLightColor.rgb * lightColor, _IsLightRimLight);
float rimArea = 1.0 - dot(viewDirWS, normalWS);
float rimLightPower = pow(rimArea, exp2(lerp(3, 0, _RimLight_Power)));
float haflLambertVert = 0.5 * dot(i.normalWS, i.lightDirWS) + 0.5;
float rimLightInsideMask = saturate((rimLightPower - _RimLight_InsideMask ) / (1.0 - _RimLight_InsideMask));
float rimLightInsideMaskWithLight = saturate(rimLightInsideMask - (1.0 - haflLambertVert + _Tweak_LightDirection_MaskLevel));
float rimLightInsideMaskFinal = lerp(rimLightInsideMask, rimLightInsideMaskWithLight, _LightDirection_MaskOn);
float3 rimLight = rimLightColor * rimLightInsideMaskFinal;
float rimLightPower_AP = pow(rimArea, exp2(lerp(3, 0, _Ap_RimLight_Power)));
float3 rimColor_AP =lerp(_Ap_RimLightColor.rgb,  _Ap_RimLightColor.rgb * lightColor, _IsLightRimLight);
float3 rimLight_AP = rimColor_AP * saturate((rimLightPower_AP - _RimLight_InsideMask) / (1.0 - _RimLight_InsideMask) - (haflLambertVert + _Tweak_LightDirection_MaskLevel));
rimLight = (rimLight + rimLight_AP) * rimLightMask;
color = color + rimLight;