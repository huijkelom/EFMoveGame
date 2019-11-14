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

	[SerializeField]
	private UnityEvent hitEvent = default;

	public void SetColor(Color color)
	{
		image.color = color;
	}

	public void SetText(string value)
	{
		text.text = value;
	}

	public void Hit(Vector3 hitPosition)
	{
		hitEvent.Invoke();
	}

	public void Win()
	{
		Debug.Log("Woo");
	}
}