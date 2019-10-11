using UnityEngine;

namespace Assets.Scripts
{
    public interface ILevelGenerator
    {
        LevelConfiguration GenerateLevel(int minItemCount,
                                         int maxItemCount,
                                         float worldRadius,
                                         float minRotationSpeed,
                                         float maxRotationSpeed,
                                         float minScale,
                                         float maxScale);
    }

    public struct LevelConfiguration
    {
        public Item[] items;
        public Vector3 solutionStartPosition;
    }

    public struct Item
    {
        public Vector3 startingPosition;
        public Vector3 rotateDirection;
        public float scale;
        public float rotationSpeed;
        public int id;
        public int parentId;
    }
}
