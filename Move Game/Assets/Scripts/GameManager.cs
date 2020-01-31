using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	private int playerCount = default;

	private List<TeamCharacter> teams = new List<TeamCharacter>();

	[Header("References")]
	[SerializeField]
	private RectTransform replayButton = default;
	[SerializeField]
	private GameTimer timer = default;
	[Space]
	[SerializeField]
	private RectTransform teamContainer = default;
	[SerializeField]
	private Transform charContainer = default;
	[SerializeField]
	private Transform flagContainer = default;
	[Space]
	[SerializeField]
	private TeamCharacter teamPrefab = default;
	[SerializeField]
	private AudioSource audioSource = default;
	[Header("Countdown")]
	[SerializeField]
	private TextMeshProUGUI countDownText = default;
	[SerializeField]
	private RectTransform countDownButton = default;
	[SerializeField]
	private AudioClip countdownBeep = default;
	[SerializeField]
	private AudioClip countdownBeepLast = default;

	private int _CurrentPlace = 0;
	private bool _started = false;
	private static readonly int Blinking = Animator.StringToHash("Blink");

	private void Start()
	{
		playerCount = int.Parse(GlobalGameSettings.GetSetting("Players"));

		for (int i = 0; i < playerCount; i++)
		{
			TeamCharacter player = Instantiate(teamPrefab, teamContainer);
			player.Init(i, charContainer.GetChild(i).GetComponent<Animator>(),
						flagContainer.GetChild(i).GetComponent<Flag>());

			player.doneEvent.AddListener(TeamFinished);

			teams.Add(player);
		}

		for (int i = playerCount; i < 4; i++)
		{
			Destroy(charContainer.GetChild(i).gameObject);
		}

		for (int i = 0; i < charContainer.childCount; i++)
		{
			charContainer.GetChild(i).GetComponent<CharacterColor>()
						 .SetColor(PlayerColourContainer.GetPlayerColour(i + 1));
		}

		timer.TimerRanOut.AddListener(TimeUp);
	}

	private void TimeUp()
	{
		foreach (TeamCharacter team in teams)
		{
			team.Deactivate();
		}

		countDownText.text = "Time's Up!";
		countDownButton.gameObject.SetActive(true);
		countDownButton.GetComponent<Animator>().SetBool(Blinking, true);
		replayButton.gameObject.SetActive(true);
	}

	// called through event in scene
	public void StartGame(int delay)
	{
		if (_started) return;
		_started = true;
		StartCoroutine(CountDown(delay));
	}

	public IEnumerator CountDown(int delay)
	{
		for (int i = 0; i < delay; i++)
		{
			audioSource.PlayOneShot(countdownBeep);
			countDownText.text = (delay - i).ToString();
			yield return new WaitForSeconds(1);
		}

		audioSource.PlayOneShot(countdownBeepLast);
		countDownButton.gameObject.SetActive(false);

		// Make player buttons interactable
		foreach (TeamCharacter team in teams) team.Activate();

		timer.StartTimer();
	}

	private void TeamFinished(TeamCharacter winner)
	{
		_CurrentPlace++;
		winner.Flag.Init(_CurrentPlace, timer.TimePassed);
		winner.GetButton().gameObject.SetActive(false);
		audioSource.Play();

		if (_CurrentPlace >= playerCount)
		{
			timer.PauseTimer(true);
			replayButton.gameObject.SetActive(true);
		}
	}
}