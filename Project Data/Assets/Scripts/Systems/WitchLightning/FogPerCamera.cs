using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class FogPerCamera : MonoBehaviour
{
    [SerializeField]
    private bool fogEnabled = true;

    private Camera camera;
    private bool revertFogState = false;

    public Light witchLight;
    private bool lightEnabled = true;

    void Awake()
    {
        this.camera = this.GetComponent<Camera>();
    }

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += this.OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += this.OnEndCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= this.OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= this.OnEndCameraRendering;
    }

    void Start()
    {
        this.camera.Render();
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == this.camera)
        {
            this.OnPreRender();
        }
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == this.camera)
        {
            this.OnPostRender();
        }
    }

    private void OnPreRender()
    {
        revertFogState = RenderSettings.fog;
        RenderSettings.fog = this.fogEnabled;
        witchLight.enabled = lightEnabled;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = revertFogState;
        witchLight.enabled = !lightEnabled;
    }
}
