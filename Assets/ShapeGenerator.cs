using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    ShapeSettings settings;
    INoiseFilter[] noiseFilters;

    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float[] noiseLayersStorage = new float[noiseFilters.Length];
        float elevation = 0;

        for (int i = 0; i < noiseFilters.Length; i++)
        {
            noiseLayersStorage[i] = noiseFilters[i].Evaluate(pointOnUnitSphere);
            if (settings.noiseLayers[i].enabled)
            {
                float mask = 1;
                if (settings.noiseLayers[i].useMask)
                {
                    for (int m = 0; m < settings.noiseLayers[i].masks.Length; m++)
                    {
                        mask *= noiseLayersStorage[settings.noiseLayers[i].masks[m]];
                    }
                }
                elevation += noiseLayersStorage[i] * mask;
            }
        }
        return pointOnUnitSphere * settings.planetRadius * (1+elevation);
    }
}
