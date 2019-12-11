using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoPlayer : MonoBehaviour
{
	[SerializeField]
	private List<TeamButton> buttons = default;

	[SerializeField, Range(.1f, 2f)]
	private float minPressInterval = 1f;
	[SerializeField, Range(.1f, 2f)]
	private float maxPressInterval = 2f;

	private float _Interval;
	private float _PassedTime;
	private bool _Pressed;

	private void Start()
	{
		_Interval = Random.Range(minPressInterval, maxPressInterval);
	}

	private void Update()
	{
		if (_PassedTime < _Interval)
		{
			_PassedTime += Time.deltaTime;
			if (!_Pressed)
			{
				buttons[Random.Range(0, buttons.Count)].Hit(Vector3.zero);
				_Pressed = true;
			}
		}
		else
		{
			_Interval   = Random.Range(minPressInterval, maxPressInterval);
			_PassedTime = 0;
			_Pressed    = false;
		}
	}

	public void AddPlayer(TeamUI player)
	{
		buttons.Add(player.GetButton());
	}
}