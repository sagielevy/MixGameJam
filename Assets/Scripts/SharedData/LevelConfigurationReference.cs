using UnityEngine;

namespace Assets.Scripts
{
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
}
