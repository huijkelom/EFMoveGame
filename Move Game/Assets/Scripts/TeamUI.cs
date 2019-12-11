using UnityEngine;
using UnityEngine.Events;

public class TeamUI : MonoBehaviour
{
	private int _Team;
	private Color _Color;

	public UnityIntEvent doneEvent = new UnityIntEvent();
	public UnityEvent hitEvent = new UnityEvent();

	[Header("References")]
	[SerializeField]
	private TeamButton button = default;
	[SerializeField]
	private StagedImage image = default;

	public int teamNumber => _Team;
	public TeamButton GetButton() => button;
	public int GetStage() => image.GetStage();

	public void Hit()
	{
		hitEvent.Invoke();
	}

	public void Init(int player)
	{
		_Team = player;
		_Color  = PlayerColourContainer.GetPlayerColour(_Team + 1);
		button.Init(this, _Color);
	}

	public void SetText(string value)
	{
		button.SetText(value);
	}

	public void Win()
	{
		doneEvent.Invoke(teamNumber);
	}
}