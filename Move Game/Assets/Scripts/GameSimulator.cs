using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameSimulator : MonoBehaviour
{
	[SerializeField]
	private List<TeamButton> buttons = default;

	[SerializeField, Range(.1f, 2f)]
	private float minPressInterval = 1f;
	[SerializeField, Range(.1f, 2f)]
	private float maxPressInterval = 2f;

	private float _Interval;
	private float _PassedTime;

	private bool pressed;

	private void Start()
	{
		_Interval = Random.Range(minPressInterval, maxPressInterval);
	}

	private void Update()
	{
		if (_PassedTime < _Interval)
		{
			_PassedTime += Time.deltaTime;
			if (!pressed)
			{
				buttons[Random.Range(0, buttons.Count)].Hit(Vector3.zero);
				pressed = true;
			}
		}
		else
		{
			_Interval   = Random.Range(minPressInterval, maxPressInterval);
			_PassedTime = 0;
			pressed     = false;
		}
	}
}