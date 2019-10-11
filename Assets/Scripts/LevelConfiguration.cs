using UnityEngine;

namespace Assets.Scripts
{
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

    public class LevelConfigurationReference : ScriptableObject
    {
        private LevelConfiguration levelConfiguration;

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
