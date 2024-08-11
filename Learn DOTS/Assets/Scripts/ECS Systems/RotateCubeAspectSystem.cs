using System.Collections;


using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct RotateCubeAspectSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AspectComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltatime = SystemAPI.Time.DeltaTime;
        double elpasedTime = SystemAPI.Time.ElapsedTime;

        foreach(var movement in SystemAPI.Query<RotateCubeAspect>())
        {
            movement.move(elpasedTime);
        }
    }
}


readonly partial struct RotateCubeAspect : IAspect
{
    readonly RefRO<RotationComponent> rotation;
    readonly RefRW<LocalTransform> transform;

    public void move(double elapsedTime)
    {
        transform.ValueRW.Position.y = (float) math.sin(elapsedTime * rotation.ValueRO.RotationSpeed); 
    }

}