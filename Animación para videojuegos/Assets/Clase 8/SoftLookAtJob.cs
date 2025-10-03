using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Unity.Burst;
using Unity.Mathematics;

#region Job
[Unity.Burst.BurstCompile]
public struct SoftLookAtJob: IWeightedAnimationJob
{
    public ReadWriteTransformHandle driven;
    public ReadOnlyTransformHandle parent;
    public TransformSceneHandle targetScene;

    public float2 yawLimitDeg;
    public float2 pitchLimitDeg;
    public float2 deadZoneYaw;
    public float2 deadZonePitch;
    public FloatProperty jobWeight{ get; set; }
    public void ProcessAnimation(UnityEngine.Animations.AnimationStream stream)
    {
        float w = jobWeight.Get(stream);
        if (w <= 0f) return;

        quaternion parentWorldRot = parent.IsValid(stream) ? parent.GetRotation(stream) : quaternion.identity;
        float3 drivenPosition = driven.GetPosition(stream);
        float3 targetPosition = targetScene.GetPosition(stream);

        float3 toTargetWorld = targetPosition - drivenPosition;
        float len2 = math.lengthsq(toTargetWorld);
        if(len2  < 1e-8f) return;

        float3 dirLocal = math.mul(math.inverse(parentWorldRot), math.normalize(toTargetWorld));

        float yawDeg = math.degrees(math.atan2(dirLocal.x, dirLocal.z));
        float pitchDeg = math.degrees(math.asin(math.clamp(dirLocal.y,-1,1)));

        yawDeg = math.clamp(yawDeg, yawLimitDeg.x, yawLimitDeg.y);
        pitchDeg = math.clamp(pitchDeg, pitchLimitDeg.x, pitchLimitDeg.y);

        quaternion clampedLocal = quaternion.EulerXYZ(math.radians(new float3 (pitchDeg, yawDeg, 0f)));  

        quaternion currentLocal =  driven.GetLocalRotation(stream);
        quaternion resultLocal = math.slerp(currentLocal, clampedLocal, w);

        if (yawDeg > deadZoneYaw.x && yawDeg < deadZoneYaw.y && pitchDeg > deadZonePitch.x && pitchDeg < deadZonePitch.y)
        {
            return;
        }


        driven.SetLocalRotation(stream,  resultLocal);
    }
    public void ProcessRootMotion(UnityEngine.Animations.AnimationStream stream) 
    {

    }
}
#endregion

#region data

[System.Serializable]

public struct SoftLookAtData: IAnimationJobData
{
    [SyncSceneToStream] public Transform constrainedObject;
    public Transform target;
    public Vector2 yawLimitDeg;
    public Vector2 pitchLimitDeg;
    public Vector2 deadZoneYaw;
    public Vector2 deadZonePitch;
    public bool IsValid() => 
        constrainedObject != null && target != null &&
        yawLimitDeg.x <= yawLimitDeg.y &&
        pitchLimitDeg.x <= pitchLimitDeg.y;
    public void SetDefaultValues() { 
        constrainedObject = null;
        yawLimitDeg = new Vector2(-60f, 60f);
        pitchLimitDeg = new Vector2(-30f, 30f);
        deadZonePitch = new Vector2(-5f, 5f);
        deadZoneYaw = new Vector2(-5f, 5f);
    }
}
#endregion

#region Binder

public class SoftLookAtJobBinder: AnimationJobBinder<SoftLookAtJob, SoftLookAtData>
{
    public override SoftLookAtJob Create(Animator animator, ref SoftLookAtData data, Component component)
    {
        var job = new SoftLookAtJob();
        job.driven = ReadWriteTransformHandle.Bind(animator, data.constrainedObject);
        job.parent = ReadOnlyTransformHandle.Bind(animator, data.constrainedObject.parent);
        job.targetScene = animator.BindSceneTransform(data.target);
        job.yawLimitDeg = data.yawLimitDeg;
        job.pitchLimitDeg = data.pitchLimitDeg;
        job.deadZoneYaw = data.deadZoneYaw;
        job.deadZonePitch = data.deadZonePitch;
        job.jobWeight = FloatProperty.Bind(animator, component, "w_Wight");
        return job;
    }
    public override void Destroy(SoftLookAtJob job)
    {
        
    }
}
#endregion
