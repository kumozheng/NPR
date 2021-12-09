

    // Color
    float3 highColor = tex2D(_HighColor_Tex, i.uv).rgb * _HighColor.rgb * lightColor;    
    // Specular
    float3 halfVector = normalize(viewDirWS + i.lightDirWS);
    float halfHdotN = 0.5 * dot(halfVector, normalWS) + 0.5; 
    float specularNoExp = 1.0 - step(halfHdotN, (1.0 - _HighColor_Power));
    float specularExp = pow(halfHdotN, exp2(lerp(11, 1, _HighColor_Power)));
    float specular = lerp(specularNoExp, specularExp, _Is_SpecularToHighColor);
    // Mask
    float4 highColorMask = tex2D(_HighColorMask, i.uv);
    float tweakHighColorMask = saturate(highColorMask.g + _Tweak_HighColorMaskLevel);
    highColor = highColor * tweakHighColorMask * specular;
    // high light affected by shadow
    // float3 tweakHiColor = highColor * (1.0 - shadowMask +  shadowMask * _TweakHighColorOnShadow);
    // color = saturate(color  - tweakHighColorMask) + tweakHiColor;
    // color = saturate(color  - tweakHighColorMask) + highColor;
    color = saturate(color + highColor);