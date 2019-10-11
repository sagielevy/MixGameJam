using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerClickHandler : MonoBehaviour
    {
        [SerializeField] private FloatReference animateTimeSeconds;
        [SerializeField] private LevelConfigurationReference configurationReference;
        [SerializeField] private BooleanReference animate;
        [SerializeField] private TrailSpawner prefab;

        private float clickStartTime;
        private TrailSpawner currSpawner;

        private void Update()
        {
            if (Time.time - clickStartTime >= animateTimeSeconds.GetValue())
            {
                animate.SetValue(false);
            }
        }

        private void HandlePlayerClick()
        {
            // Ignore extra clicks while animating
            if (Time.time - clickStartTime < animateTimeSeconds.GetValue())
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
            clickStartTime = Time.time;
            animate.SetValue(true);
        }
    }
}
