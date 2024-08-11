using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct RotateCubeSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
       state.RequireForUpdate<MainThreadComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltatime = SystemAPI.Time.DeltaTime;
        foreach(var (transform, Rotation) in SystemAPI.Query <RefRW<LocalTransform> ,RefRO<RotationComponent>>())
        {
            transform.ValueRW = transform.ValueRO.RotateY(deltatime*Rotation.ValueRO.RotationSpeed);
        }
       
    }
}
