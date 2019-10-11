using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class BooleanReference : ScriptableObject
    {
        [SerializeField] private bool value;

        public void SetValue(bool value)
        {
            this.value = value;
        }

        public bool GetValue()
        {
            return value;
        }
    }
}
