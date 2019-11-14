using UnityEngine;
using UnityEngine.Events;

public class ButtonWithStagedImage : MonoBehaviour
{
	private int _Player;
	private Color _Color;

	public UnityIntEvent doneEvent = new UnityIntEvent();

	[Header("References")]
	[SerializeField]
	private TeamButton button;
	[SerializeField]
	private StagedImage image;

	public TeamButton GetButton() => button;

	public void Init(int player)
	{
		_Player = player;
		_Color  = PlayerColourContainer.GetPlayerColour(_Player + 1);
		button.SetColor(_Color);
	}
}