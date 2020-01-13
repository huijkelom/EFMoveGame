using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeamUI : MonoBehaviour
{
	private int _Team;
	private Color _Color;

	private Animator _Character;
	private Vector3 characterStart;
	private Vector3 characterEnd;
	[SerializeField]
	private int stepCount = 10;
	private int steps = 0;

	public float progress => (float) steps / stepCount;

	public UnityIntEvent doneEvent = new UnityIntEvent();
	public UnityEvent hitEvent = new UnityEvent();

	[Header("References")]
	[SerializeField]
	private TeamButton button = default;
	private static readonly int Running = Animator.StringToHash("Running");

	public int teamNumber => _Team;
	public TeamButton GetButton() => button;

	public void Hit()
	{
		hitEvent.Invoke();

		steps++;
		Debug.Log(progress);
		StartCoroutine(MoveCharacter(.5f));
	}

	public IEnumerator MoveCharacter(float duration)
	{
		Vector3 startPos  = _Character.transform.position;
		Vector3 targetPos = Vector3.Lerp(characterStart, characterEnd, progress);

		_Character.SetBool(Running, true);
		for (float elapsed = 0; elapsed < duration; elapsed += Time.deltaTime)
		{
			_Character.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
			yield return null;
		}

		_Character.SetBool(Running, false);
		_Character.transform.position = targetPos;

		if (progress >= 1) Win();
	}

	public void Init(int player, Animator character)
	{
		_Character     = character;
		characterStart = _Character.transform.position;
		characterEnd   = new Vector3(-characterStart.x, characterStart.y, characterStart.z);

		_Team  = player;
		_Color = PlayerColourContainer.GetPlayerColour(_Team + 1);
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