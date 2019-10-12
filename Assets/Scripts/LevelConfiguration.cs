using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct LevelConfiguration
    {
        public Item[] items;
        public Vector3 solutionStartPosition;
        public int solutionItemId;
    }

    [Serializable]
    public struct Item
    {
        public Vector3 startingPosition;
        public Vector3 rotateDirection;
        public float scale;
        public float rotationSpeed;
        public int id;
        public int parentId;
    }

    // TODO add game event of type LevelConfigurationReference
}
