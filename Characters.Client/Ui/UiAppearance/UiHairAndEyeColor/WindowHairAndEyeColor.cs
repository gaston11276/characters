using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowHairAndEyeColor: Window
	{
		UiElementFiveM uiColumnHairIndexLabels = new UiElementFiveM();
		UiElementFiveM uiColumnHairIndexValues = new UiElementFiveM();
		UiElementFiveM uiColumnHairIndexDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnHairIndexIncrease = new UiElementFiveM();
		UiElementFiveM uiColumnHairColorValues = new UiElementFiveM();
		UiElementFiveM uiColumnHairColorDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnHairColorIncrease = new UiElementFiveM();
		UiElementFiveM uiColumnHairSecondaryColorValues = new UiElementFiveM();
		UiElementFiveM uiColumnHairSecondaryColorDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnHairSecondaryColorIncrease = new UiElementFiveM();

		EntryHair uiHair;
		EntryEyeColor uiEyeColor;

		Character character;

		public WindowHairAndEyeColor()
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
			await uiHair.SetUi();
			await uiEyeColor.SetUi();
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;
			await WindowManager.Delay(10);
		}

		public async Task SetDefaults()
		{
			character.PedHeadOverlays.Eyebrows.Opacity = 1f;

			if (character.Gender == (short)Gender.Female)
			{
				character.PedHeadOverlays.Eyebrows.Index = 10;
			}
			await WindowManager.Delay(WindowManager.delayMs);
		}

		protected void CreateColumn(UiElementFiveM uiPanel, HGravity gravity, UiElementFiveM uiColumn, string label = null)
		{
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetGravity(VGravity.Top);
			uiColumn.SetVDimension(Dimension.Fill);
			uiColumn.SetFlags(TRANSPARENT);
			uiPanel.AddElement(uiColumn);

			if (label != null)
			{
				Textbox header = new Textbox();
				header.SetFont(Font.CharletComprimeColonge);
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

			CreateColumn(panel, HGravity.Left, uiColumnHairIndexLabels, "");
			CreateColumn(panel, HGravity.Right, uiColumnHairIndexValues, "");
			CreateColumn(panel, HGravity.Center, uiColumnHairIndexDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnHairIndexIncrease, "");
			CreateColumn(panel, HGravity.Right, uiColumnHairColorValues, "Color");
			CreateColumn(panel, HGravity.Center, uiColumnHairColorDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnHairColorIncrease, "");
			CreateColumn(panel, HGravity.Right, uiColumnHairSecondaryColorValues, "Highlight");
			CreateColumn(panel, HGravity.Center, uiColumnHairSecondaryColorDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnHairSecondaryColorIncrease, "");
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
			header.SetText("Hair and Eye Color");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			CreateColumns();

			uiHair = CreateHair("Hair");
			uiEyeColor = CreateEyeColor("Eyes");
		}

		private EntryHair CreateHair(string label)
		{
			EntryHair entry = new EntryHair();

			entry.uiHairLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiHairLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiHairLabel.SetText(label);
			entry.uiHairLabel.SetFlags(TRANSPARENT);
			uiColumnHairIndexLabels.AddElement(entry.uiHairLabel);

			entry.uiHairIndex.SetFont(Font.CharletComprimeColonge);
			entry.uiHairIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiHairIndex.SetFlags(TRANSPARENT);
			uiColumnHairIndexValues.AddElement(entry.uiHairIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexDecrease.OnMouseButton);
			uiColumnHairIndexDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexIncrease.OnMouseButton);
			uiColumnHairIndexIncrease.AddElement(entry.btnIndexIncrease);

			// Color 1
			entry.uiColorId.SetFont(Font.CharletComprimeColonge);
			entry.uiColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiColorId.SetFlags(UiElement.TRANSPARENT);
			uiColumnHairColorValues.AddElement(entry.uiColorId);

			entry.btnColorIdDecrease.SetText("-");
			entry.btnColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdDecrease.SetProperties(CANFOCUS);
			entry.btnColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnColorIdDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnColorIdDecrease.OnMouseButton);
			uiColumnHairColorDecrease.AddElement(entry.btnColorIdDecrease);

			entry.btnColorIdIncrease.SetText("+");
			entry.btnColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdIncrease.SetProperties(CANFOCUS);
			entry.btnColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnColorIdIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnColorIdIncrease.OnMouseButton);
			uiColumnHairColorIncrease.AddElement(entry.btnColorIdIncrease);

			// Color 2
			entry.uiSecondaryColorId.SetFont(Font.CharletComprimeColonge);
			entry.uiSecondaryColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiSecondaryColorId.SetFlags(TRANSPARENT);
			uiColumnHairSecondaryColorValues.AddElement(entry.uiSecondaryColorId);

			entry.btnSecondaryColorIdDecrease.SetText("-");
			entry.btnSecondaryColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnSecondaryColorIdDecrease.SetProperties(CANFOCUS);
			entry.btnSecondaryColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseSecondaryColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnSecondaryColorIdDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnSecondaryColorIdDecrease.OnMouseButton);
			uiColumnHairSecondaryColorDecrease.AddElement(entry.btnSecondaryColorIdDecrease);

			entry.btnSecondaryColorIdIncrease.SetText("+");
			entry.btnSecondaryColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnSecondaryColorIdIncrease.SetProperties(CANFOCUS);
			entry.btnSecondaryColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseSecondaryColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnSecondaryColorIdIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnSecondaryColorIdIncrease.OnMouseButton);
			uiColumnHairSecondaryColorIncrease.AddElement(entry.btnSecondaryColorIdIncrease);

			entry.GetNumberOfHairIndex = GetNumberOfPedHairVariations;
			entry.GetIndex = GetHairIndex;
			entry.SetIndex = SetHairIndex;
			entry.GetNumberOfHairColors = GetNumberOfPedHairColors;
			entry.GetColor = GetHairColor;
			entry.SetColor = SetHairColor;
			entry.GetSecondaryColor = GetSecondaryHairColor;
			entry.SetSecondaryColor = SetSecondaryHairColor;

			return entry;
		}

		private EntryEyeColor CreateEyeColor(string label)
		{
			EntryEyeColor entry = new EntryEyeColor();

			entry.uiEyeColorLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiEyeColorLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiEyeColorLabel.SetText(label);
			entry.uiEyeColorLabel.SetFlags(TRANSPARENT);
			uiColumnHairIndexLabels.AddElement(entry.uiEyeColorLabel);

			entry.uiEyeColorIndex.SetFont(Font.CharletComprimeColonge);
			entry.uiEyeColorIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiEyeColorIndex.SetFlags(TRANSPARENT);
			uiColumnHairColorValues.AddElement(entry.uiEyeColorIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexDecrease.OnMouseButton);
			uiColumnHairColorDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexIncrease.OnMouseButton);
			uiColumnHairColorIncrease.AddElement(entry.btnIndexIncrease);

			entry.GetNumberOfEyeColors = GetNumberOfPedEyeColors;
			entry.GetColor = GetEyeColor;
			entry.SetColor = SetEyeColor;

			return entry;
		}

		public int GetNumberOfPedHairVariations()
		{
			return API.GetNumberOfPedDrawableVariations(Game.PlayerPed.Handle, (int)PedComponentType.Hair);
		}
		public int GetHairIndex()
		{
			return character.PedComponents.Hair.Index;
		}
		public void SetHairIndex(int index)
		{
			character.PedComponents.Hair.Index = index;
			ApplyHairIndexToPed();
		}
		public int GetNumberOfPedHairColors()
		{
			return API.GetNumHairColors();
		}
		public int GetHairColor()
		{
			return character.PedHairColor;
		}
		public void SetHairColor(int colorId)
		{
			character.PedHairColor = colorId;
			ApplyHairColorToPed();
		}
		public int GetSecondaryHairColor()
		{
			return character.PedSecondHairColor;
		}

		public void SetSecondaryHairColor(int colorId)
		{
			character.PedSecondHairColor = colorId;
			ApplyHairColorToPed();
		}
		
		public async Task ApplyToPed()
		{
			ApplyHairIndexToPed();
			ApplyHairColorToPed();
			ApplyEyeColorToPed();

			await WindowManager.Delay(WindowManager.delayMs);
		}

		public void ApplyHairIndexToPed()
		{
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Hair, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
		}

		public void ApplyHairColorToPed()
		{
			API.SetPedHairColor(Game.PlayerPed.Handle, character.PedHairColor, character.PedSecondHairColor);
		}

		private int GetNumberOfPedEyeColors()
		{
			return (int)PedEyeColor.NumberOfPedEyeColors;
		}
		private int GetEyeColor()
		{
			return (int)character.PedEyeColor;
		}
		private void SetEyeColor(int eyeColor)
		{
			character.PedEyeColor = (PedEyeColor)eyeColor;
			ApplyEyeColorToPed();
		}

		private void ApplyEyeColorToPed()
		{
			API.SetPedEyeColor(Game.PlayerPed.Handle, (int)character.PedEyeColor);
		}
	}
}
