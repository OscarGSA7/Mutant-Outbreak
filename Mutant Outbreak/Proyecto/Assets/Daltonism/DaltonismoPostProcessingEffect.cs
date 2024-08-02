using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(DaltonismoPostProcessingRenderer), PostProcessEvent.AfterStack, "Custom/DaltonismoPostProcessing")]
public sealed class DaltonismoPostProcessingEffect : PostProcessEffectSettings
{
    [Range(0f, 3f), Tooltip("Tipo de Daltonismo")]
    public FloatParameter daltonismo = new FloatParameter { value = 0f };
}

public sealed class DaltonismoPostProcessingRenderer : PostProcessEffectRenderer<DaltonismoPostProcessingEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/DaltonismoPostProcessing"));
        sheet.properties.SetFloat("_Daltonismo", settings.daltonismo);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
