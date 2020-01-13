using UnityEngine;

public class CharacterColor : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer[] spriteRenderers;

	public void SetColor(Color color)
	{
		for (int i = 0; i < spriteRenderers.Length; i++)
		{
			spriteRenderers[i].color = color;
		}
	}
}