using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
	[SerializeField]
	private string sceneName = default;

	private void Start()
	{
		SceneManager.LoadScene(sceneName);
	}
}