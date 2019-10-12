using UnityEngine;

namespace Assets.Scripts.SharedData
{
    [CreateAssetMenu]
    public class IntReference : ScriptableObject
    {
        [SerializeField] private int value;

        public void SetValue(int value)
        {
            this.value = value;
        }

        public int GetValue()
        {
            return value;
        }
    }
}
