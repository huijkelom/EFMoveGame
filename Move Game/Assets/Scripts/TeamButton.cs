using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour, I_SmartwallInteractable
{
	[SerializeField]
	private Image image = default;

	[SerializeField]
	private TextMeshProUGUI text = default;

	private bool pressed = false;

	private UnityEvent hitEvent = new UnityEvent();

	public void Init(TeamCharacter teamCharacter, Color color)
	{
		image.color = color;
		hitEvent.AddListener(teamCharacter.Hit);
	}

	public void SetText(string value) => text.text = value;

	public void Hit(Vector3 hitPosition)
	{
		if (!pressed)
		{
			hitEvent.Invoke();
			StartCoroutine(blockPresses(.25f));
		}
	}

	private IEnumerator blockPresses(float delay)
	{
		pressed = true;
		yield return new WaitForSeconds(delay);
		pressed = false;
	}
}