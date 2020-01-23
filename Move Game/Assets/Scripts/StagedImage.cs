//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class StagedImage : MonoBehaviour
//{
//	[SerializeField]
//	private List<Sprite> Images = default;
//	[SerializeField]
//	private Image image = default;
//
//	private int currentStage = 0;
//
//	[SerializeField]
//	private int stages = 10;
//
//	public TeamUI teamUi;
//
//	public int GetStage() => currentStage;
//
//	private Sprite GetStageImage(int stage) =>
//		Images[Mathf.FloorToInt(((float) stage).Map(0, stages, 0, Images.Count))];
//
//	public void NextStage()
//	{
//		if (currentStage < stages)
//		{
//			currentStage++;
//			teamUi.SetText(currentStage.ToString());
//
//			if (currentStage >= stages)
//			{
//				teamUi.Win();
//				image.sprite = Images[Images.Count - 1];
//				return;
//			}
//
//			image.sprite = GetStageImage(currentStage);
//		}
//	}
//}