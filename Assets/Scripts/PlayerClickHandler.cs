using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerClickHandler : MonoBehaviour
    {
        [SerializeField] private float animateTimeSeconds = 10;
        [SerializeField] private LevelConfigurationReference configurationReference;
        [SerializeField] private BooleanReference animate;
        [SerializeField] private TrailSpawner prefab;

        private float clickStartTime;
        private TrailSpawner currSpawner;

        private void Update()
        {
            if (Time.time - clickStartTime >= animateTimeSeconds)
            {
                animate.SetValue(false);
            }
        }

        private void HandlePlayerClick()
        {
            // Ignore extra clicks while animating
            if (Time.time - clickStartTime < animateTimeSeconds)
            {
                ClearOldTrail();
                return;
            }

            var clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(clickRay.origin, clickRay.direction, out var hit))
            {
                StartTrail(hit);
            }
        }

        private void ClearOldTrail()
        {
            if (currSpawner == null) return;

            Destroy(currSpawner.gameObject);
            currSpawner = null;
        }

        private void StartTrail(RaycastHit hit)
        {
            currSpawner = Instantiate(prefab, hit.point, Quaternion.identity, hit.transform);
            currSpawner.Configure(animateTimeSeconds);
            clickStartTime = Time.time;
            animate.SetValue(true);
        }
    }
}
