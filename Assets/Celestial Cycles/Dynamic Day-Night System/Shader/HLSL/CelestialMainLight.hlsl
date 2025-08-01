


void CelestialMainLight_half(float3 WorldPos, out half3 Direction, out half3 DirectionUp, out half3 DirectionRight,out half LightIntensity, 
 out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW
   Direction = half3(0.5, 0.5, 0);
   Color = 1;
   DistanceAtten = 1;
   ShadowAtten = 1;
   DirectionUp = half3(0, 1, 0);   // Default world up
   DirectionRight = half3(1, 0, 0); // Default world right
LightIntensity = 1;
#else
   half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
   Light mainLight = GetMainLight(shadowCoord);
   Direction = mainLight.direction;
   Color = mainLight.color;
   DistanceAtten = mainLight.distanceAttenuation;
   ShadowAtten = mainLight.shadowAttenuation;

LightIntensity = dot(mainLight.color, half3(0.333, 0.333, 0.333));  // ✅ Approximate brightness
   // Compute UpVector and RightVector using an orthonormal basis
   DirectionUp = normalize(cross(normalize(mainLight.direction), half3(1, 0, 0)));
   DirectionRight = normalize(cross(DirectionUp, mainLight.direction));
#endif
}

void SampleSH_half(half3 normalWS, out half3 Ambient)
{
    // LPPV is not supported in Ligthweight Pipeline
    real4 SHCoefficients[7];
    SHCoefficients[0] = unity_SHAr;
    SHCoefficients[1] = unity_SHAg;
    SHCoefficients[2] = unity_SHAb;
    SHCoefficients[3] = unity_SHBr;
    SHCoefficients[4] = unity_SHBg;
    SHCoefficients[5] = unity_SHBb;
    SHCoefficients[6] = unity_SHC;

    Ambient = max(half3(0, 0, 0), SampleSH9(SHCoefficients, normalWS));
}