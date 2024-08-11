using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ExecuteAuth : MonoBehaviour
{
    public bool MainThread;
    public bool IJobEntity;
    public bool Aspect;
    public bool Prefabs;
    public bool IJobChunk;
    public class Baker : Baker<ExecuteAuth>
    {
        public override void Bake(ExecuteAuth authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            if (authoring.MainThread) { AddComponent<MainThreadComponent>(entity); }
            if (authoring.IJobEntity) { AddComponent<IJobEntityComponent>(entity); }
            if (authoring.Aspect) { AddComponent<AspectComponent>(entity); }
            if (authoring.Prefabs) { AddComponent<PrefabsComponent>(entity); }
            if (authoring.IJobChunk) { AddComponent<IJobChunkComponent>(entity); }
        }
    }
}

public struct MainThreadComponent : IComponentData
{
    
}

public struct IJobEntityComponent : IComponentData { }
public struct AspectComponent : IComponentData { }
public struct PrefabsComponent : IComponentData { }
public struct IJobChunkComponent: IComponentData{ }

