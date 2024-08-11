using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotateAuthComponent : MonoBehaviour
{
    public float RotationSpeed = 2f;

    public class RotationComponentBaker : Baker<RotateAuthComponent>
    {
        public override void Bake(RotateAuthComponent authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new RotationComponent { RotationSpeed = authoring.RotationSpeed });
        }
    }

}
