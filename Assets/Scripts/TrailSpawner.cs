using UnityEngine;

namespace Assets.Scripts
{ 
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailSpawner : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRenderer;

        private void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        public void Configure(float animateTimeSeconds)
        {
            trailRenderer.time = animateTimeSeconds;
        }
    }
}
