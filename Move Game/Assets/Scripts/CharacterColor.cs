using UnityEngine;

public class CharacterColor : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer spriteRenderer = default;

	public void SetColor(Color color) => spriteRenderer.color = color;
}