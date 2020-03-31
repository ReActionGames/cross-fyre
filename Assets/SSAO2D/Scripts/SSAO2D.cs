using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SSAO2D : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float intensity = 0.3f;
    [Range(0.0f, 0.1f)]
    public float spread = 0.005f;
    public Vector2 offset = new Vector2(-0.004f, 0.004f);
    [Range(0.0f, 1.0f)]
    public float cutoff = 0.1f;
    [Range(0.000001f, 1.0f)]
    public float threshold = 0.0001f;

    private Shader shader;
    private Material material = null;
    private bool isInit;

    private const string SHADER_NAME = "Hidden/SSAO2D";
    private const string INTENSITY = "intensity";
    private const string SPREAD = "spread";
    private const string CUTOUT = "cutoff";
    private const string THRESHOLD = "threshold";
    private const string OFFSET = "offset";

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if (!isInit)
        {
            Shader shader = Shader.Find(SHADER_NAME);
            GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
            material = new Material(shader);
            isInit = true;
        }
    }

    private void OnDisable()
    {
        if (material != null)
        {
            DestroyImmediate(material);
        }
        isInit = false;
    }

    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(INTENSITY, intensity);
        material.SetFloat(SPREAD, spread);
        material.SetFloat(CUTOUT, cutoff);
        material.SetFloat(THRESHOLD, threshold);
        material.SetVector(OFFSET, offset);
        Graphics.Blit(source, destination, material);
    }
}
