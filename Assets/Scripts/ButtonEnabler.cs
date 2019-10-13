using Assets.Scripts.SharedData;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class ButtonEnabler : MonoBehaviour
    {
        [SerializeField] private BooleanReference gameClickable;
        [SerializeField] private BooleanReference animate;
        private Button newGameButton;
        private Image buttonImage;
        private Text buttonText;

        private void Awake()
        {
            newGameButton = GetComponent<Button>();
            buttonImage = GetComponent<Image>();
            buttonText = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            newGameButton.enabled = !animate.GetValue() && gameClickable.GetValue();
            buttonImage.enabled = newGameButton.enabled;
            buttonText.enabled = newGameButton.enabled;
        }
    }
}
