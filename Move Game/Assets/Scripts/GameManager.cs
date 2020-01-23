using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	[Range(1, 4)]
	private int playerCount = default;

	[SerializeField]
	private float gameDoneTime = .5f;

	private List<TeamCharacter> teams = new List<TeamCharacter>();

	[Header("References")]
	[SerializeField]
	private RectTransform teamContainer = default;

	[SerializeField]
	private TeamCharacter teamPrefab = default;

	[SerializeField]
	private GameTimer timer = default;

	[SerializeField]
	private Transform charContainer = default;

	private void Start()
	{
		for (int i = 0; i < playerCount; i++)
		{
			TeamCharacter player = Instantiate(teamPrefab, teamContainer);
			player.Init(i, charContainer.GetChild(i).GetComponent<Animator>());
			player.doneEvent.AddListener(TeamWon);

			teams.Add(player);
		}

		for (int i = 0; i < charContainer.childCount; i++)
		{
			charContainer.GetChild(i).GetComponent<CharacterColor>()
						 .SetColor(PlayerColourContainer.GetPlayerColour(i + 1));
		}
	}

	private void TeamWon(int winner)
	{
		timer.PauseTimer(true);
		foreach (TeamCharacter team in teams) team.GetButton().gameObject.SetActive(false);

		StartCoroutine(delayedSceneMove(teams.Select(x => (int) x.progress).ToList()));

		Debug.Log($"Team {winner} won");
	}

	private IEnumerator delayedSceneMove(List<int> scores)
	{
		yield return new WaitForSeconds(gameDoneTime);
		ScoreScreenController.MoveToScores(scores);
	}

	public void GameOver() => TeamWon(teams.OrderByDescending(x => (int) x.progress).First().teamNumber);
}