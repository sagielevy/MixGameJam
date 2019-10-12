using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct LevelConfiguration
    {
        public Item[] items;
        public Vector3 solutionStartPositionOnItem;
        public int solutionItemId;
    }

    [Serializable]
    public class Item
    {
        public Vector3 startingPosition;
        public Vector3 rotateDirection;
        public float scale;
        public float aggregatedScale;
        public float rotationSpeed;
        public int id;
        public Item parent;
    }

    // TODO add game event of type LevelConfigurationReference
}
