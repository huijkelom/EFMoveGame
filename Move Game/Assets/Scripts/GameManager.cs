using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private bool autoPlay = default;
	[Space]
	[SerializeField]
	[Range(1, 4)]
	private int playerCount = default;

	private List<ButtonWithStagedImage> teams = new List<ButtonWithStagedImage>();

	[Header("References")]
	[SerializeField]
	private AutoPlayer autoPlayer = default;

	[SerializeField]
	private RectTransform teamContainer = default;

	[SerializeField]
	private ButtonWithStagedImage teamPrefab = default;

	private void Start()
	{
		for (int i = 0; i < playerCount; i++)
		{
			ButtonWithStagedImage player = Instantiate(teamPrefab, teamContainer);
			player.Init(i);
			player.doneEvent.AddListener(TeamWon);
			autoPlayer.AddPlayer(player);
		}

		if (autoPlay) autoPlayer.gameObject.SetActive(true);
	}

	private void TeamWon(int winner)
	{
		foreach (ButtonWithStagedImage team in teams) team.GetButton().gameObject.SetActive(false);
	}
}