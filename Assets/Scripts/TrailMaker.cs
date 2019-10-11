using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailMaker : MonoBehaviour
    {
        [SerializeField] private float animateTimeSeconds = 10;
        [SerializeField] private LevelConfigurationReference configurationReference;
        [SerializeField] private TrailRenderer trailRenderer;

        private float clickStartTime;

        private void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            
        }

        private void HandlePlayerClick()
        {
            // Ignore extra clicks while animating
            if (Time.time - clickStartTime < animateTimeSeconds)
            {
                return;
            }

            clickStartTime = Time.time;
        }

        private void AnimateItems()
        {
            
        }
    }
}
