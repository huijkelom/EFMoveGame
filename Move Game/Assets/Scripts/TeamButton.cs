using System;
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

	private UnityEvent hitEvent = new UnityEvent();

	public void Init(TeamUI teamUi, Color color)
	{
		image.color = color;
		hitEvent.AddListener(teamUi.Hit);
	}

	public void SetText(string value)
	{
		text.text = value;
	}

	public void Hit(Vector3 hitPosition)
	{
		hitEvent.Invoke();
	}
}