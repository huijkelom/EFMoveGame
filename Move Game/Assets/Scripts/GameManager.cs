using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private bool autoPlay = default;
	[Space]
	[SerializeField]
	[Range(1, 4)]
	private int playerCount = default;

	private List<TeamUI> teams = new List<TeamUI>();

	[Header("References")]
	[SerializeField]
	private AutoPlayer autoPlayer = default;

	[SerializeField]
	private RectTransform teamContainer = default;

	[SerializeField]
	private TeamUI teamPrefab = default;

	[SerializeField]
	private GameTimer timer = default;

	private void Start()
	{
		for (int i = 0; i < playerCount; i++)
		{
			TeamUI player = Instantiate(teamPrefab, teamContainer);
			player.Init(i);
			player.doneEvent.AddListener(TeamWon);
			autoPlayer.AddPlayer(player);

			teams.Add(player);
		}

		if (autoPlay) autoPlayer.gameObject.SetActive(true);
	}

	private void TeamWon(int winner)
	{
		timer.PauseTimer(true);
		foreach (TeamUI team in teams) team.GetButton().gameObject.SetActive(false);

		ScoreScreenController.MoveToScores(teams.Select(x => x.GetStage()).ToList());

		Debug.Log($"Team {winner} won");
	}

	public void GameOver()
	{
		TeamWon(teams.OrderByDescending(x => x.GetStage()).First().teamNumber);
	}
}