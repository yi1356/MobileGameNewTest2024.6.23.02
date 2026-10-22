using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualiser : MonoBehaviour
{
    public Renderer mesh;
    public Color DamageEmmisionColor;
    public float BlinkSpeed = 20f;
    public string EmmisionColorPropertyName = "_Addition";//???
    public HealthComponent healthComponent;
    Color OrigionalEmissionColor;
    // Start is called before the first frame update
    void Start()
    {
        Material mat = mesh.material;
        mesh.material = new Material(mat);

        OrigionalEmissionColor = mesh.material.GetColor(EmmisionColorPropertyName);
        healthComponent.onTakeDamage += TookDamage;
    }

    private void TookDamage(float health, float delta, float maxHealth, GameObject inStigator)
    {
        //颜色变化
        Color currenEmmisonColor = mesh.material.GetColor(EmmisionColorPropertyName);
        if (Mathf.Abs((currenEmmisonColor - OrigionalEmissionColor).grayscale) < 0.1f)//颜色差异在一定范围内，才能改变颜色
        {
            mesh.material.SetColor(EmmisionColorPropertyName, DamageEmmisionColor);
        }
    }

    private void Update()
    {
        Color currentEmmisionColor = mesh.material.GetColor(EmmisionColorPropertyName);
        Color newEmissionColor = Color.Lerp(currentEmmisionColor, OrigionalEmissionColor, Time.deltaTime * BlinkSpeed);
        mesh.material.SetColor(EmmisionColorPropertyName, newEmissionColor);
    }

}
