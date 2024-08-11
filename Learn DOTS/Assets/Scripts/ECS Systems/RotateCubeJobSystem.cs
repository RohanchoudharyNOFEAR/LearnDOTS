using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial struct RotateCubeJobSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<IJobEntityComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltatime = SystemAPI.Time.DeltaTime;
        var RotationJob = new RotateJob { deltaTime = deltatime };
        RotationJob.Schedule();
      //  state.Dependency = RotationJob.Schedule(state.Dependency);

    }
}

[BurstCompile]
public partial struct RotateJob : IJobEntity
{
   public float deltaTime;

    [BurstCompile]
    public void Execute( ref LocalTransform transform, in RotationComponent rotation)//parameters are the component for query
    {
        transform = transform.RotateY(deltaTime * rotation.RotationSpeed);
    }

}
