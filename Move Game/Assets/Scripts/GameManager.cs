using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	[Range(1, 4)]
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

	private int _CurrentPlace = 0;

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