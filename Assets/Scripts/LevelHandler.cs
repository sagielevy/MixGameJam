using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelHandler : MonoBehaviour
    {
        [SerializeField] private BooleanReference levelPlayable;
        [SerializeField] private LevelConfigurationReference levelConfigurationReference;
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private SolutionTrailGenerator solutionTrailGenerator;
        [SerializeField] private float validatorThreshold = 0.2f;
        [SerializeField] private Image loadingScreen;

        private ITrailValidator validator;
        private ITrail solutionTrail;

        private void Start()
        {
            validator = new TrailValidatorMSE(validatorThreshold);
            NewLevel();
        }

        private void NewLevel()
        {
            loadingScreen.enabled = true;
            levelPlayable.SetValue(false);
            levelGenerator.GenerateNewLevel();
            levelGenerator.LoadGeneratedLevel();

            solutionTrailGenerator.transform.parent = GameObject.Find("World").transform;
            solutionTrailGenerator.transform.position = levelConfigurationReference
                .GetLevelConfiguration().solutionStartPosition;
            solutionTrailGenerator.StartSimulation(samples =>
            {
                solutionTrail = samples;
                levelGenerator.LoadGeneratedLevel();
                loadingScreen.enabled = false;
                levelPlayable.SetValue(true);
            });
        }

        public void AttemptPlayerSolution(ITrail playerTrail)
        {
            StartCoroutine(validator.Validate(solutionTrail, playerTrail)
                ? LoadNextLevelDelay()
                : TryAgainDelay());
        }

        private IEnumerator<WaitForSeconds> TryAgainDelay()
        {
            levelPlayable.SetValue(false);
            yield return new WaitForSeconds(3);
            levelPlayable.SetValue(true);
        }

        private IEnumerator<WaitForSeconds> LoadNextLevelDelay()
        {
            levelPlayable.SetValue(false);
            yield return new WaitForSeconds(3);

            NewLevel();
        }
    }
}
