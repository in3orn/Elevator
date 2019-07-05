using Krk.Common.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Krk.Misc
{
    public class ExitMediator : MonoBehaviour
    {
        [SerializeField] ShowableElement panel;
        [SerializeField] Button exitButton;
        [SerializeField] Button cancelButton;

        void Start()
        {
            panel.Init(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                panel.Show();
        }

        void OnEnable()
        {
            exitButton.onClick.AddListener(HandleExitButtonClicked);
            cancelButton.onClick.AddListener(HandleCancelButtonClicked);
        }

        void HandleExitButtonClicked()
        {
            panel.Hide();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        void HandleCancelButtonClicked()
        {
            panel.Hide();
        }
    }
}