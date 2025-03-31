using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineView : MonoBehaviour
{
    [SerializeField] private int numberOfPoints = 200;
    [SerializeField] private float waveFrequency = 1f;
    [SerializeField] private float waveHeight = 1f;
    [SerializeField] private float waveSpeed = 2f;
    [SerializeField] private float lineLength = 20f;

    [SerializeField] private Color lineColor = Color.white; // Цвет линии
    [SerializeField] private Color emissionColor = Color.green; // Эмиссионный цвет

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.useWorldSpace = false;

        // Создаем новый материал с эмиссией
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = lineColor;
        lineMaterial.SetColor("_EmissionColor", emissionColor); // Устанавливаем эмиссионный цвет

        // Включаем эмиссию
        lineMaterial.EnableKeyword("_EMISSION");

        // Применяем материал к LineRenderer
        lineRenderer.material = lineMaterial;
    }

    void Update()
    {
        float step = lineLength / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = i * step;
            float y = Mathf.Sin((x + Time.time * waveSpeed) * waveFrequency) * waveHeight;
            float z = 0f;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
        }
    }
}
