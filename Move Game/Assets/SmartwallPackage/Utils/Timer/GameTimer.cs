using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
	public TextMeshProUGUI LabelOfTimer;
	public Image Gage;
	/// <summary>
	/// Time limit can be overwritten by the setting file if it contains a setting from Time.
	/// </summary>
	public float TimeLimit;
	public Color ColorWhenOutOfTime;
	public float PercentageOutOfTime = 15;
	private float _StartTime;
	private Color _ColourStart;
	private bool Paused = false;
	public UnityEvent TimerRanOut = new UnityEvent();
	public float TimePassed = 0;

	/// <summary>
	/// Start running the set timer.
	/// </summary>
	public void StartTimer()
	{
		_StartTime         = Time.time;
		LabelOfTimer.color = _ColourStart;
		StartCoroutine("RunTimer");
	}

	/// <summary>
	/// Pause or unpause the timer.
	/// </summary>
	public void PauseTimer(bool pause)
	{
		Paused = pause;
	}

	void Awake()
	{
		//Check if a Text class has been linked
		if (LabelOfTimer == null)
		{
			LabelOfTimer = gameObject.GetComponent<TextMeshProUGUI>(); //Try to find a Text class
			if (LabelOfTimer == null)
			{
				Debug.LogWarning(
					"L_Text | Start | Text changer has no label to change and can't find one on its gameobject: " +
					gameObject.name);
				return;
			}
			else
			{
				Debug.LogWarning(
					"L_Text | Start | Text changer has no label to change but it has found a Text class on its gameobject: " +
					gameObject.name);
			}
		}

		_ColourStart = LabelOfTimer.color;
	}

	private void Start()
	{
		int savedTime = 0;
		if (GlobalGameSettings.GetSetting("Use Time").Equals("No"))
		{
			Gage.color             = new Color(0, 0, 0, 0);
			LabelOfTimer.faceColor = new Color(0, 0, 0, 0);
			savedTime              = 10000000;
		}
		else
		{
			savedTime =  60 * int.Parse(GlobalGameSettings.GetSetting("Minutes"));
			savedTime += int.Parse(GlobalGameSettings.GetSetting("Seconds"));
		}

		TimeLimit = savedTime;

		int minutes = (int) (TimeLimit / 60);
		int seconds = (int) (TimeLimit % 60);
		LabelOfTimer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
	}

	IEnumerator RunTimer()
	{
		float t = TimeLimit;
		while (t > 0)
		{
			if (!Paused)
			{
				t          -= Time.deltaTime;
				TimePassed += Time.deltaTime;
			}

			if (t <= 0)
			{
				TimerRanOut.Invoke();
				t = 0;
			}

			int minutes = (int) (t / 60);
			int seconds = (int) (t % 60);
			Gage.fillAmount = t / TimeLimit;

			LabelOfTimer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
			if (t < (TimeLimit / PercentageOutOfTime))
			{
				float factor = t / PercentageOutOfTime;
				LabelOfTimer.color = Color.Lerp(ColorWhenOutOfTime, _ColourStart, factor);
			}

			yield return null;
		}
	}
}