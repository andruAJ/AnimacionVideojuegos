using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RangeDetector", story: "Update [Range_Detector] and assign [Target]", category: "Action", id: "96230cd45b9468e663b681effa06a0c4")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Range_Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnUpdate()
    {
        Target.Value = Range_Detector.Value.UpdateDetector();
        return Target.Value != null ? Status.Failure : Status.Success;
    }
}

