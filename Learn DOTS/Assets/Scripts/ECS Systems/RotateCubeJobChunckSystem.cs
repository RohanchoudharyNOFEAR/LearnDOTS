using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using NUnit.Framework;
using System.ComponentModel;
using Unity.Mathematics;
using Unity.Collections;

public partial struct RotateCubeJobChunckSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
       state.RequireForUpdate<IJobChunkComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var spinningCubesQuery = SystemAPI.QueryBuilder().WithAll<RotationComponent, LocalTransform>().Build();

        var job = new RotationJob
        {
            TransformTypeHandle = SystemAPI.GetComponentTypeHandle<LocalTransform>(),
            RotationSpeedTypeHandle = SystemAPI.GetComponentTypeHandle<RotationComponent>(true),
            DeltaTime = SystemAPI.Time.DeltaTime
        };

        // Unlike an IJobEntity, an IJobChunk must be manually passed a query.
        // Furthermore, IJobChunk does not pass and assign the state.Dependency JobHandle implicitly.
        // (This pattern of passing and assigning state.Dependency ensures that the entity jobs scheduled
        // in different systems will depend upon each other as needed.)
        state.Dependency = job.Schedule(spinningCubesQuery, state.Dependency);
    }
}

[BurstCompile]
struct RotationJob : IJobChunk
{
    public ComponentTypeHandle<LocalTransform> TransformTypeHandle;
  [Unity.Collections.ReadOnly]  public ComponentTypeHandle<RotationComponent> RotationSpeedTypeHandle;
    public float DeltaTime;

    public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask,
        in v128 chunkEnabledMask)
    {
        // The useEnableMask parameter is true when one or more entities in
        // the chunk have components of the query that are disabled.
        // If none of the query component types implement IEnableableComponent,
        // we can assume that useEnabledMask will always be false.
        // However, it's good practice to add this guard check just in case
        // someone later changes the query or component types.
        Assert.IsFalse(useEnabledMask);

        var transforms = chunk.GetNativeArray(ref TransformTypeHandle);
        var rotationSpeeds = chunk.GetNativeArray(ref RotationSpeedTypeHandle);
        //for (int i = 0, chunkEntityCount = chunk.Count; i < chunkEntityCount; i++)
        //{
        //    transforms[i] = transforms[i].RotateY(rotationSpeeds[i].RotationSpeed * DeltaTime);
        //}

        var enumerator = new ChunkEntityEnumerator(useEnabledMask, chunkEnabledMask, chunk.Count);
        while (enumerator.NextEntityIndex(out var i))
        {
            transforms[i] = transforms[i].RotateY(rotationSpeeds[i].RotationSpeed * DeltaTime);

        }
    }
}
