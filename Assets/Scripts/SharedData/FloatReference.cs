using UnityEngine;

namespace Assets.Scripts.SharedData
{
    [CreateAssetMenu]
    public class FloatReference : ScriptableObject
    {
        [SerializeField] private float value;

        public void SetValue(float value)
        {
            this.value = value;
        }

        public float GetValue()
        {
            return value;
        }
    }
}
