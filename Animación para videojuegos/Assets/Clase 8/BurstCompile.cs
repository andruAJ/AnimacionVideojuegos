using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Unity.Mathematics;

#region Job

[Unity.Burst.BurstCompile]
public struct BurstCompile: IWeightedAnimationJob
{
    public ReadWriteTransformHandle driven;

    public float minLocal;
    public float maxLocal;
    public FloatProperty jobWeight{ get; set; }

    public void ProcessAnimation(AnimationStream stream)
    {
        float w = jobWeight.Get(stream);
        if (w <= 0f) return;
        float3 p = driven.GetLocalPosition(stream);
        float3 clamped = math.clamp(p, minLocal, maxLocal);
        float3 res = math.lerp(p, clamped, w);
        driven.SetLocalPosition(stream, res);
    }   

    public void ProcessRootMotion(AnimationStream stream) 
    {
    
    }

}
#endregion

#region data

[System.Serializable]
public struct LocalPosClampData: IAnimationJobData
{
    [SyncSceneToStream] public Transform constrainedObject;
    public Vector3 minLocal;
    public Vector3 maxLocal;

    
    public bool IsValid() => 
        constrainedObject != null &&
        minLocal.x <= maxLocal.x &&
        minLocal.y <= maxLocal.y &&
        minLocal.z <= maxLocal.z;

    public void SetDefaultValues()
    {
        constrainedObject = null;
        minLocal = new Vector3(-0.5f, -0.5f, -0.5f);
        maxLocal = new Vector3(0.5f, 0.5f, 0.5f);
    }
}

#endregion

#region Binder

public class LocalPosClampBinder : AnimationJobBinder<BurstCompile, LocalPosClampData>
{
    public override BurstCompile Create(Animator animator, ref LocalPosClampData data, Component component)
    {
        var job = new BurstCompile 
        {
            driven = ReadWriteTransformHandle.Bind(animator, data.constrainedObject),
            minLocal = data.minLocal.x,
            maxLocal = data.maxLocal.x,
            jobWeight = FloatProperty.Bind(animator, component, "m_Wight")
        };   

        return job;
    }
    public override void Destroy(BurstCompile job)
    {

    }
}
#endregion