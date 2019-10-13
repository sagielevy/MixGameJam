using System;
using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField] private Transform world;
        [SerializeField] private Text gameResult;

        private ITrailValidator validator;
        private ITrail solutionTrail;

        private void Start()
        {
            validator = new TrailValidatorMSE();
            NewLevel();
        }

        public void NewLevel()
        {
            loadingScreen.enabled = true;
            levelPlayable.SetValue(false);
            levelGenerator.GenerateNewLevel();
            levelGenerator.LoadGeneratedLevel();
            
            var config = levelConfigurationReference.GetLevelConfiguration();
            var solutionBall = Array.Find(world.GetComponentsInChildren<ItemAnimator>(),
                item => item.GetId() == config.solutionItemId);

            solutionTrailGenerator.transform.position = solutionBall.transform.position + config.solutionStartPositionOnItem;
            solutionTrailGenerator.StartSimulation(samples =>
            {
                solutionTrail = samples;
                levelGenerator.LoadGeneratedLevel();
                loadingScreen.enabled = false;
                levelPlayable.SetValue(true);
            }, solutionBall.transform, config.solutionStartPositionOnItem);
        }

        public void AttemptPlayerSolution(ITrail playerTrail)
        {
            StartCoroutine(validator.Validate(solutionTrail, playerTrail, validatorThreshold)
                ? LoadNextLevelDelay()
                : TryAgainDelay());
        }

        private IEnumerator<WaitForSeconds> TryAgainDelay()
        {
            levelPlayable.SetValue(false);
            gameResult.text = "Try again...";
            yield return new WaitForSeconds(3f);

            gameResult.text = "";
            levelGenerator.LoadGeneratedLevel();
            levelPlayable.SetValue(true);
        }

        private IEnumerator<WaitForSeconds> LoadNextLevelDelay()
        {
            levelPlayable.SetValue(false);
            gameResult.text = "Success!";
            yield return new WaitForSeconds(3f);

            gameResult.text = "";
            NewLevel();
        }
    }
}
