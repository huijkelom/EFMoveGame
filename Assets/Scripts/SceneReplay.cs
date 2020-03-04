using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReplay : MonoBehaviour
{
	[SerializeField]
	private string sceneName = default;
	[SerializeField]
	private bool autoSwitch = true;

	private void Start()
	{
		if (autoSwitch) SceneManager.LoadScene(sceneName);
	}

	public void SwitchScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}