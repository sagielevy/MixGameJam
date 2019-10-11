using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct LevelConfiguration
    {
        public Item[] items;
        public Vector3 solutionStartPosition;
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

    [CreateAssetMenu]
    public class LevelConfigurationReference : ScriptableObject
    {
        [SerializeField] private LevelConfiguration levelConfiguration;

        public LevelConfiguration GetLevelConfiguration()
        {
            return levelConfiguration;
        }

        public void SetLevelConfiguration(LevelConfiguration value)
        {
            levelConfiguration = value;
        }
    }

    // TODO add game event of type LevelConfigurationReference
}
