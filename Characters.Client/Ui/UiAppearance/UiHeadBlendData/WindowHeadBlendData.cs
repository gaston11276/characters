using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowHeadBlendData : Window
	{
		UiElementFiveM uiColumnLabels = new UiElementFiveM();
		UiElementFiveM uiColumnValues = new UiElementFiveM();
		UiElementFiveM uiColumnDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnIncrease = new UiElementFiveM();

		Textbox valueParent1 = new Textbox();
		Textbox valueParent2 = new Textbox();
		Textbox valueShapeMix = new Textbox();
		Textbox valueSkinMix = new Textbox();

		Character character;

		public WindowHeadBlendData()
		{
			defaultPadding = 0.0025f;
		}

		protected override void OnOpen()
		{
			UiCamera.SetMode(CameraMode.Face);
			base.OnOpen();
		}

		protected override void OnClose()
		{
			
			base.OnClose();
		}

		public async Task SetUi()
		{
			valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
			valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
			valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
			valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
			await WindowManager.Delay(WindowManager.delayMs);
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;
			await WindowManager.Delay(10);
		}

		public async Task SetDefaults()
		{
			if (character.Gender == (short)Gender.Male)
			{
				character.PedHeadBlendData.Parent1 = 4;
				character.PedHeadBlendData.Parent2 = 10;
				character.PedHeadBlendData.ShapeMix = 0.5f;
				character.PedHeadBlendData.SkinMix = 0.5f;
			}
			else if (character.Gender == (short)Gender.Female)
			{
				character.PedHeadBlendData.Parent1 = 10;
				character.PedHeadBlendData.Parent2 = 31;
				character.PedHeadBlendData.ShapeMix = 0.9f;
				character.PedHeadBlendData.SkinMix = 0.3f;
			}

			await WindowManager.Delay(WindowManager.delayMs);
		}

		protected void CreateColumn(UiElementFiveM uiPanel, HGravity gravity, UiElementFiveM uiColumn, string label = null)
		{
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetFlags(TRANSPARENT);
			uiPanel.AddElement(uiColumn);

			if (label != null)
			{
				Textbox header = new Textbox();
				header.SetPadding(new UiRectangle(defaultPadding));
				header.SetText(label);
				header.SetFlags(TRANSPARENT);
				if (label.Length == 0)
				{
					header.SetTextFlags(TRANSPARENT);
				}
				uiColumn.AddElement(header);
			}
		}

		protected void CreateColumns()
		{
			UiElementFiveM panel = new UiElementFiveM();
			panel.SetPadding(new UiRectangle(0f));
			panel.SetFlags(TRANSPARENT);
			AddElement(panel);

			CreateColumn(panel, HGravity.Left, uiColumnLabels);
			CreateColumn(panel, HGravity.Right, uiColumnValues);
			CreateColumn(panel, HGravity.Center, uiColumnDecrease);
			CreateColumn(panel, HGravity.Center, uiColumnIncrease);
		}

		public override void CreateUi()
		{
			base.CreateUi();

			SetFlags(HIDDEN);
			SetPadding(new UiRectangle(defaultPadding));
			SetAlignment(HAlignment.Right);
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetOrientation(Orientation.Vertical);

			Textbox header = new Textbox();
			header.SetText("Head Blend Data");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			CreateColumns();

			Textbox labelParent1 = new Textbox();
			labelParent1.SetText("Parent 1");
			labelParent1.SetFont(Font.CharletComprimeColonge);
			labelParent1.SetPadding(new UiRectangle(defaultPadding));
			labelParent1.SetFlags(TRANSPARENT);
			uiColumnLabels.AddElement(labelParent1);

			valueParent1.SetText($"{0}/{45}");
			valueParent1.SetFont(Font.CharletComprimeColonge);
			valueParent1.SetPadding(new UiRectangle(defaultPadding));
			valueParent1.SetFlags(TRANSPARENT);
			uiColumnValues.AddElement(valueParent1);

			Textbox btnShape1Decrease = new Textbox();
			btnShape1Decrease.SetText("-");
			btnShape1Decrease.SetPadding(new UiRectangle(defaultPadding));
			btnShape1Decrease.SetProperties(CANFOCUS);
			btnShape1Decrease.RegisterOnLMBRelease(DecreaseParent1);
			WindowManager.RegisterOnMouseMoveCallback(btnShape1Decrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShape1Decrease.OnMouseButton);
			uiColumnDecrease.AddElement(btnShape1Decrease);

			Textbox btnShape1Increase = new Textbox();
			btnShape1Increase.SetText("+");
			btnShape1Increase.SetPadding(new UiRectangle(defaultPadding));
			btnShape1Increase.SetProperties(CANFOCUS);
			btnShape1Increase.RegisterOnLMBRelease(IncreaseParent1);
			WindowManager.RegisterOnMouseMoveCallback(btnShape1Increase.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShape1Increase.OnMouseButton);
			uiColumnIncrease.AddElement(btnShape1Increase);

			Textbox labelParent2 = new Textbox();
			labelParent2.SetText("Parent 2");
			labelParent2.SetFont(Font.CharletComprimeColonge);
			labelParent2.SetPadding(new UiRectangle(defaultPadding));
			labelParent2.SetFlags(TRANSPARENT);
			uiColumnLabels.AddElement(labelParent2);

			valueParent2.SetText($"{0}/{45}");
			valueParent2.SetFont(Font.CharletComprimeColonge);
			valueParent2.SetPadding(new UiRectangle(defaultPadding));
			valueParent2.SetFlags(TRANSPARENT);
			uiColumnValues.AddElement(valueParent2);

			Textbox btnShape2Decrease = new Textbox();
			btnShape2Decrease.SetText("-");
			btnShape2Decrease.SetPadding(new UiRectangle(defaultPadding));
			btnShape2Decrease.SetProperties(CANFOCUS);
			btnShape2Decrease.RegisterOnLMBRelease(DecreaseParent2);
			WindowManager.RegisterOnMouseMoveCallback(btnShape2Decrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShape2Decrease.OnMouseButton);
			uiColumnDecrease.AddElement(btnShape2Decrease);

			Textbox btnShape2Increase = new Textbox();
			btnShape2Increase.SetText("+");
			btnShape2Increase.SetPadding(new UiRectangle(defaultPadding));
			btnShape2Increase.SetProperties(CANFOCUS);
			btnShape2Increase.RegisterOnLMBRelease(IncreaseParent2);
			WindowManager.RegisterOnMouseMoveCallback(btnShape2Increase.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShape2Increase.OnMouseButton);
			uiColumnIncrease.AddElement(btnShape2Increase);

			Textbox labelShapeMix = new Textbox();
			labelShapeMix.SetText("Shape Mix");
			labelShapeMix.SetFont(Font.CharletComprimeColonge);
			labelShapeMix.SetPadding(new UiRectangle(defaultPadding));
			labelShapeMix.SetFlags(TRANSPARENT);
			uiColumnLabels.AddElement(labelShapeMix);

			valueShapeMix.SetText($"{string.Format("{0:0.0#}", 0.0f)}");
			valueShapeMix.SetFont(Font.CharletComprimeColonge);
			valueShapeMix.SetPadding(new UiRectangle(defaultPadding));
			valueShapeMix.SetFlags(TRANSPARENT);
			uiColumnValues.AddElement(valueShapeMix);

			Textbox btnShapeMixDecrease = new Textbox();
			btnShapeMixDecrease.SetText("-");
			btnShapeMixDecrease.SetPadding(new UiRectangle(defaultPadding));
			btnShapeMixDecrease.SetProperties(CANFOCUS);
			btnShapeMixDecrease.RegisterOnLMBRelease(DecreaseShapeMix);
			WindowManager.RegisterOnMouseMoveCallback(btnShapeMixDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShapeMixDecrease.OnMouseButton);
			uiColumnDecrease.AddElement(btnShapeMixDecrease);

			Textbox btnShapeMixIncrease = new Textbox();
			btnShapeMixIncrease.SetText("+");
			btnShapeMixIncrease.SetPadding(new UiRectangle(defaultPadding));
			btnShapeMixIncrease.SetProperties(CANFOCUS);
			btnShapeMixIncrease.RegisterOnLMBRelease(IncreaseShapeMix);
			WindowManager.RegisterOnMouseMoveCallback(btnShapeMixIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnShapeMixIncrease.OnMouseButton);
			uiColumnIncrease.AddElement(btnShapeMixIncrease);

			Textbox labelSkinMix = new Textbox();
			labelSkinMix.SetText("Skin Mix");
			labelSkinMix.SetFont(Font.CharletComprimeColonge);
			labelSkinMix.SetPadding(new UiRectangle(defaultPadding));
			labelSkinMix.SetFlags(TRANSPARENT);
			uiColumnLabels.AddElement(labelSkinMix);

			valueSkinMix.SetText($"{string.Format("{0:0.0#}", 0.0f)}");
			valueSkinMix.SetFont(Font.CharletComprimeColonge);
			valueSkinMix.SetPadding(new UiRectangle(defaultPadding));
			valueSkinMix.SetFlags(TRANSPARENT);
			uiColumnValues.AddElement(valueSkinMix);

			Textbox btnSkinMixDecrease = new Textbox();
			btnSkinMixDecrease.SetText("-");
			btnSkinMixDecrease.SetPadding(new UiRectangle(defaultPadding));
			btnSkinMixDecrease.SetProperties(CANFOCUS);
			btnSkinMixDecrease.RegisterOnLMBRelease(DecreaseSkinMix);
			WindowManager.RegisterOnMouseMoveCallback(btnSkinMixDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnSkinMixDecrease.OnMouseButton);
			uiColumnDecrease.AddElement(btnSkinMixDecrease);

			Textbox btnSkinMixIncrease = new Textbox();
			btnSkinMixIncrease.SetText("+");
			btnSkinMixIncrease.SetPadding(new UiRectangle(defaultPadding));
			btnSkinMixIncrease.SetProperties(CANFOCUS);
			btnSkinMixIncrease.RegisterOnLMBRelease(IncreaseSkinMix);
			WindowManager.RegisterOnMouseMoveCallback(btnSkinMixIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(btnSkinMixIncrease.OnMouseButton);
			uiColumnIncrease.AddElement(btnSkinMixIncrease);
		}

		private async void IncreaseParent1()
		{
			if (character.PedHeadBlendData.Parent1 < 44)
			{
				character.PedHeadBlendData.Parent1++;
				valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
				await ApplyToPed();
			}
		}
		private async void DecreaseParent1()
		{
			if (character.PedHeadBlendData.Parent1 > 0)
			{
				character.PedHeadBlendData.Parent1--;

				valueParent1.SetText($"{character.PedHeadBlendData.Parent1}/{45}");
				await ApplyToPed();
			}
		}
		private async void IncreaseParent2()
		{
			if (character.PedHeadBlendData.Parent2 < 44)
			{
				character.PedHeadBlendData.Parent2++;
				valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
				await ApplyToPed();
			}
		}
		private async void DecreaseParent2()
		{
			if (character.PedHeadBlendData.Parent2 > 0)
			{
				character.PedHeadBlendData.Parent2--;

				valueParent2.SetText($"{character.PedHeadBlendData.Parent2}/{45}");
				await ApplyToPed();
			}
		}
		private async void IncreaseShapeMix()
		{
			character.PedHeadBlendData.ShapeMix += 0.1f;
			if (character.PedHeadBlendData.ShapeMix > 1f)
			{
				character.PedHeadBlendData.ShapeMix = 1f;
			}
			valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
			await ApplyToPed();
		}
		private async void DecreaseShapeMix()
		{
			character.PedHeadBlendData.ShapeMix -= 0.1f;
			if (character.PedHeadBlendData.ShapeMix < 0f)
			{
				character.PedHeadBlendData.ShapeMix = 0f;
			}
			valueShapeMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.ShapeMix)}");
			await ApplyToPed();
		}
		private void IncreaseSkinMix()
		{
			character.PedHeadBlendData.SkinMix += 0.1f;
			if (character.PedHeadBlendData.SkinMix > 1f)
			{
				character.PedHeadBlendData.SkinMix = 1f;
			}
			valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
			UpdatePed();
		}
		private void DecreaseSkinMix()
		{
			character.PedHeadBlendData.SkinMix -= 0.1f;
			if (character.PedHeadBlendData.SkinMix < 0f)
			{
				character.PedHeadBlendData.SkinMix = 0f;
			}
			valueSkinMix.SetText($"{string.Format("{0:0.0#}", character.PedHeadBlendData.SkinMix)}");
			UpdatePed();
		}

		public async Task ApplyToPed()
		{
			API.SetPedHeadBlendData(Game.PlayerPed.Handle,
									character.PedHeadBlendData.Parent1,
									character.PedHeadBlendData.Parent2,
									0,
									character.PedHeadBlendData.Parent1,
									character.PedHeadBlendData.Parent2,
									0,
									character.PedHeadBlendData.ShapeMix,
									character.PedHeadBlendData.SkinMix,
									0f,
									false);
			await WindowManager.Delay(WindowManager.delayMs);
		}

		private void UpdatePed()
		{
			API.UpdatePedHeadBlendData(Game.PlayerPed.Handle,
									character.PedHeadBlendData.ShapeMix,
									character.PedHeadBlendData.SkinMix,
									0f);
		}
	}
}
