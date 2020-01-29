using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TeamCharacter : MonoBehaviour
{
	private int _Team;
	private Color _Color;

	public Flag Flag { get; private set; }
	private Animator _Character;
	private Vector3 _CharacterStart;
	private Vector3 _CharacterEnd;
	[SerializeField]
	private int stepCount = 10;
	private int _Steps = 0;

	public float Progress => (float) _Steps / stepCount;

	public UnityCharacterEvent doneEvent = new UnityCharacterEvent();
	public UnityEvent hitEvent = new UnityEvent();

	[Header("References")]
	[SerializeField]
	private TeamButton button = default;
	private static readonly int Running = Animator.StringToHash("Running");

	public int teamNumber => _Team;
	public TeamButton GetButton() => button;

	private Coroutine _RunningRoutine = null;

	public void Hit()
	{
		hitEvent.Invoke();

		_Steps++;
		Debug.Log(Progress);
		if (_RunningRoutine != null) StopCoroutine(_RunningRoutine);
		_RunningRoutine = StartCoroutine(MoveCharacter(.5f));
	}

	public IEnumerator MoveCharacter(float duration)
	{
		Vector3 startPos  = _Character.transform.position;
		Vector3 targetPos = Vector3.Lerp(_CharacterStart, _CharacterEnd, Progress);

		_Character.SetBool(Running, true);
		for (float elapsed = 0; elapsed < duration; elapsed += Time.deltaTime)
		{
			_Character.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
			yield return null;
		}

		_Character.SetBool(Running, false);
		_Character.transform.position = targetPos;

		if (Progress >= 1) Win();
	}

	public void Init(int player, Animator character, Flag flag)
	{
		_Character     = character;
		_CharacterStart = _Character.transform.position;
		_CharacterEnd   = new Vector3(-_CharacterStart.x, _CharacterStart.y, _CharacterStart.z);

		stepCount = Convert.ToInt16(GlobalGameSettings.GetSetting("Steps"));
		
		Flag   = flag;
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
		doneEvent.Invoke(this);
	}
}