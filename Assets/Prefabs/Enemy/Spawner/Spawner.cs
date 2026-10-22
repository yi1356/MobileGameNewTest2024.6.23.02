using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VFX 
{
    public ParticleSystem particle;
    public float size;
}

public class Spawner : Enemy
{
    [Header("À¿ÕˆÃÿ–ß")]
    public VFX[] vfxs; 
    protected override void Death()
    {
        for (int i=0;i<vfxs.Length;i++)
        {
            ParticleSystem a=GameObject.Instantiate<ParticleSystem>(vfxs[i].particle,this.transform.position,this.transform.rotation);
            a.transform.localScale = vfxs[i].size*Vector3.one;
        } 
    }
}
