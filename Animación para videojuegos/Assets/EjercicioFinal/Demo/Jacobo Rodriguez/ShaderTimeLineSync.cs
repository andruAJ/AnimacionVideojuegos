using System;
using UnityEngine;

public class ShaderTimelineController : MonoBehaviour
{
    public float timelineTime = 0f;
    private bool shaderActive = false;

    [Header("Post Process Material")]
    [SerializeField] private Material postProcessMat;

    [Header("Time Source (optional)")]
    [SerializeField] private Material timeSourceMaterial; // Material del cual leer _ExternalTime si existe

    [Header("Shader Materials to Reset (optional)")]
    [SerializeField] private Material[] materialsToReset; // Materiales a forzar a 0 en reset

    private const string ExternalTimeProp = "_ExternalTime";
    private const string TimelineTimeProp = "_TimelineTime";

    // Método llamado por el Timeline
    void Start()
    {
        ReSetVignette();
    }
    public void StartShader()
    {
        // Si hay un material fuente y tiene la propiedad, léela del material.
        // En caso contrario, usa el valor global (fallback).
        float externalTime = 0f;
        if (timeSourceMaterial != null && timeSourceMaterial.HasProperty(ExternalTimeProp))
        {
            externalTime = timeSourceMaterial.GetFloat(ExternalTimeProp);
        }
        else
        {
            externalTime = Shader.GetGlobalFloat(ExternalTimeProp);
        }

        shaderActive = true;
        timelineTime = externalTime;   // iniciar desde el valor existente
        SetDefaultVignette();
    }

    public void ReSetVignette()
    {
        if (postProcessMat != null)
        {
            postProcessMat.SetFloat("_VignetteIntensity", 0f);
            postProcessMat.SetFloat("_VignettePower", 0f);
        }

        // Resetear tiempos de TODOS los shaders a 0 al llegar la señal
        ResetShaderTimesToZero();
        
    }

    // --- NUEVO MÉTODO ---
    public void SetDefaultVignette()
    {
        if (postProcessMat == null) return;
        postProcessMat.SetFloat("_VignetteIntensity", 2f);
        postProcessMat.SetFloat("_VignettePower", 2f);
    }

    private void ResetShaderTimesToZero()
    {
        // Reiniciar acumuladores locales y globales
        timelineTime = 0f;
        Shader.SetGlobalFloat(TimelineTimeProp, 0f);
        Shader.SetGlobalFloat(ExternalTimeProp, 0f);

        // Forzar en el material fuente si existe
        if (timeSourceMaterial != null && timeSourceMaterial.HasProperty(ExternalTimeProp))
        {
            timeSourceMaterial.SetFloat(ExternalTimeProp, 0f);
        }

        // Forzar en una lista opcional de materiales
        if (materialsToReset != null)
        {
            for (int i = 0; i < materialsToReset.Length; i++)
            {
                var mat = materialsToReset[i];
                if (mat == null) continue;
                if (mat.HasProperty(ExternalTimeProp)) mat.SetFloat(ExternalTimeProp, 0f);
                if (mat.HasProperty(TimelineTimeProp)) mat.SetFloat(TimelineTimeProp, 0f);
            }
        }
    }

    void Update()
    {
        if (!shaderActive) return;

        timelineTime += Time.deltaTime;

        // Enviar valor global
        Shader.SetGlobalFloat(TimelineTimeProp, timelineTime);
    }
}
