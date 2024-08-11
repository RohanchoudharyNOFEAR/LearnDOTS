using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawnerAuthComponent : MonoBehaviour
{
    public GameObject CubePrefab;
    class SpawnerBaker : Baker<SpawnerAuthComponent>
    {
        public override void Bake(SpawnerAuthComponent authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnerComponent
            {
                CubePrefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.None)
            });
        }
    }
}
