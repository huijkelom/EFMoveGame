using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Flag : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI numberText = default;
	[SerializeField]
	private TextMeshProUGUI numberSuffixText = default;
	[SerializeField]
	private TextMeshProUGUI timeText = default;

	[SerializeField]
	private Color[] placeColors = default;

	[SerializeField]
	private AnimationCurve enterCurve = default;

	public void Init(int place, float time)
	{
		gameObject.SetActive(true);
		StartCoroutine(Appear(.5f));
		SetText(place, time);
	}

	public void SetText(int place, float time)
	{
		numberText.text      = "#"+place.ToString();
		numberText.faceColor = placeColors[place - 1];

		switch (place)
		{
			case 1:
				numberSuffixText.text = "#";
				break;
			case 2:
				numberSuffixText.text = "#";
				break;
			case 3:
				numberSuffixText.text = "#";
				break;
			case 4:
				numberSuffixText.text = "#";
				break;
			default:
				throw new InvalidEnumArgumentException("Invalid enum argument, can only be 1-4");
		}

		numberSuffixText.faceColor = placeColors[place - 1];

		int minutes = (int) (time / 60);
		int seconds = (int) (time % 60);
		timeText.text      = $"{minutes:D2}:{seconds:D2}";
		timeText.faceColor = placeColors[place - 1];
	}

	private IEnumerator Appear(float duration)
	{
		Vector3 target = transform.position;
		Vector3 start = transform.position + new Vector3(-12, 0, 0);

		for (float passed = 0; passed < duration; passed += Time.deltaTime)
		{
			transform.position = Vector3.Slerp(start, target, enterCurve.Evaluate(passed/duration));
			yield return null;
		}

		transform.position = target;
	}
}