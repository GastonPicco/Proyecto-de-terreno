using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TestURP : MonoBehaviour
{
    static readonly FieldInfo ShadowDistanceField;
    [SerializeField]
    public float ShadowDistanceFieldValue;
    [SerializeField]
    private float _ShadowDistanceFieldValue;

    [SerializeField]
    public RenderPipelineAsset RenderPipeline;

    static TestURP()
    {
        var RenderPipelineAssetClassType = typeof(UniversalRenderPipelineAsset);
        const BindingFlags PrivateInstanceBindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
        ShadowDistanceField = RenderPipelineAssetClassType.GetField("m_ShadowDistance", PrivateInstanceBindingFlags);
    }

    void Start()
    {
        // Must clone the render pipeline asset to protect project level settings from runtime value changes.
        RenderPipeline = Instantiate(QualitySettings.renderPipeline);
    }
    void Update()
    {
        if (Application.isEditor) FixRenderPipelineReferenceInEditor();

        if (_ShadowDistanceFieldValue != ShadowDistanceFieldValue) {
            _ShadowDistanceFieldValue = ShadowDistanceFieldValue;
            SetShadowDistance(ShadowDistanceFieldValue);
        }
    }

    public void SetShadowDistance(float Value)
    {
        ShadowDistanceField.SetValue(RenderPipeline, Value);
    }

    public void FixRenderPipelineReferenceInEditor()
    {
        // UNITY BUG: Unity loses the references to the cloned render pipeline assets when Quality Settings window shown in Unity inspector. We re-assign them here.
        if (!QualitySettings.renderPipeline) QualitySettings.renderPipeline = RenderPipeline;
    }
}
