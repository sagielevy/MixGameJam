using UnityEngine;

namespace Assets.Scripts
{
    public interface ILevelGenerator
    {
        ItemData[] ItemPositions(int minItemCount,
            int maxItemCount,
            float worldRadius,
            float minRotationSpeed,
            float maxRotationSpeed,
            float minScale,
            float maxScale);
    }

    public struct ItemData
    {
        public Vector3 startingPosition;
        public Vector3 rotateDirection;
        public float scale;
        public float rotationSpeed;
        public int id;
        public int parentId;
    }
}
