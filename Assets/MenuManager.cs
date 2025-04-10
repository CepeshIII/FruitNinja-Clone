using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private Button ExitButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button BackToMainMenuButton;

    private void OnEnable()
    {
        sceneLoader = (SceneLoader)SceneLoader.Instance;

        ExitButton.onClick.AddListener(() => sceneLoader.Exit());
        RestartButton.onClick.AddListener(() => sceneLoader.LoadMainScene());
        BackToMainMenuButton.onClick.AddListener(() => sceneLoader.LoadMainMenuScene());

    }
}
