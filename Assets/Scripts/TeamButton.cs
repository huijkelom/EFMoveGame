using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour, I_SmartwallInteractable
{
	[SerializeField]
	private Image image = default;

	[SerializeField]
	private TextMeshProUGUI text = default;

	private bool pressed = false;

	private UnityEvent hitEvent = new UnityEvent();

	private EventSystem eventSystem;

	Button _ButtonImOn;
	Animator _Anime;
	public AudioSource _AS;
	public string AnimationTriggerName = "Clicked";

	public void Init(TeamCharacter teamCharacter, Color color)
	{
		image.color = color;
		hitEvent.AddListener(teamCharacter.Hit);
	}

	public void SetText(string value) => text.text = value;

	private void Awake()
	{
		_ButtonImOn = gameObject.GetComponent<Button>();
		_Anime = gameObject.GetComponent<Animator>();
		_AS = gameObject.GetComponent<AudioSource>();
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	public void Hit(Vector3 hitPosition)
	{
		if (!pressed)
		{
			hitEvent.Invoke();
			StartCoroutine(blockPresses(.25f));
			if (_AS != null) _AS.Play();
			if (_Anime != null) _Anime.SetTrigger(AnimationTriggerName);
			//_ButtonImOn.onClick.Invoke()
			ExecuteEvents.Execute(_ButtonImOn.gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
		}
	}

	private IEnumerator blockPresses(float delay)
	{
		pressed = true;
		yield return new WaitForSeconds(delay);
		pressed = false;
	}
}