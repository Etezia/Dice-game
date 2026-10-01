using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private string pigGameScene;
    [SerializeField] private string yahtzeeGameScene;

    public void StartPigGame() => SceneManager.LoadScene(pigGameScene);

	public void StartYahtzeeGame() => SceneManager.LoadScene(yahtzeeGameScene);

	public void Exit() => Application.Quit();
}
