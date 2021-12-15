using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowAppearance : Window
	{
		WindowModel windowModel = new WindowModel();
		WindowHeadBlendData windowHeadBlendData = new WindowHeadBlendData();
		WindowFaceFeatures windowFaceFeatures = new WindowFaceFeatures();
		WindowHairAndEyeColor windowHairAndEyeColor = new WindowHairAndEyeColor();
		WindowHeadOverlays windowHeadOverlays = new WindowHeadOverlays();
		WindowDecorations windowDecorations = new WindowDecorations();
		WindowComponents windowComponents = new WindowComponents();

		Textbox buttonModel = new Textbox();
		Textbox buttonHeadBlendData = new Textbox();
		Textbox buttonFaceFeatures = new Textbox();
		Textbox buttonHairAndEyeColor = new Textbox();
		Textbox buttonHeadOverlays = new Textbox();
		Textbox buttonDecorations = new Textbox();
		Textbox buttonComponents = new Textbox();

		protected List<fpVoid> onSaveCallbacks = new List<fpVoid>();
		protected List<fpVoid> onRevertCallbacks = new List<fpVoid>();

		Character character;

		public WindowAppearance()
		{
			defaultPadding = 0.0025f;
		}

		public void RegisterOnSaveCallback(fpVoid OnSave)
		{
			onSaveCallbacks.Add(OnSave);
		}

		public void RegisterOnRevertCallback(fpVoid OnRevert)
		{
			onRevertCallbacks.Add(OnRevert);
		}

		protected override void OnOpen()
		{
			Game.Player.CanControlCharacter = false;
			UiCamera.SetMode(CameraMode.Front);
			base.OnOpen();
		}

		protected override void OnClose()
		{
			windowModel.Close();
			windowHeadBlendData.Close();
			windowFaceFeatures.Close();
			windowHairAndEyeColor.Close();
			windowHeadOverlays.Close();
			windowDecorations.Close();
			windowComponents.Close();

			Game.Player.CanControlCharacter = true;
			UiCamera.SetMode(CameraMode.Game);
			base.OnClose();
		}

		public async Task SetUi()
		{
			await windowModel.SetUi();
			await windowHeadBlendData.SetUi();
			await windowFaceFeatures.SetUi();
			await windowHairAndEyeColor.SetUi();
			await windowHeadOverlays.SetUi();
			await windowDecorations.SetUi();
			await windowComponents.SetUi();
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;

			await windowModel.SetCharacter(character);
			await windowHeadBlendData.SetCharacter(character);
			await windowFaceFeatures.SetCharacter(character);
			await windowHairAndEyeColor.SetCharacter(character);
			await windowHeadOverlays.SetCharacter(character);
			await windowDecorations.SetCharacter(character);
			await windowComponents.SetCharacter(character);

			UpdateButtons(character.ModelHash);
		}

		public async Task ApplyToPed()
		{
			await windowModel.ApplyToPed();
			await windowHeadBlendData.ApplyToPed();
			await windowFaceFeatures.ApplyToPed();
			await windowHairAndEyeColor.ApplyToPed();
			await windowHeadOverlays.ApplyToPed();
			await windowDecorations.ApplyToPed();
			await windowComponents.ApplyToPed();
		}

		public async Task SetDefaults()
		{
			await windowModel.SetDefaults();
			await windowHeadBlendData.SetDefaults();
			await windowFaceFeatures.SetDefaults();
			await windowHairAndEyeColor.SetDefaults();
			await windowHeadOverlays.SetDefaults();
			await windowDecorations.SetDefaults();
			await windowComponents.SetDefaults();
		}

		public override void CreateUi()
		{
			base.CreateUi();

			SetFlags(HIDDEN);
			SetPadding(new UiRectangle(defaultPadding));
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetAlignment(HAlignment.Left);
			SetOrientation(Orientation.Vertical);

			Textbox header = new Textbox();
			header.SetText("Appearance");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.6f);
			header.SetTextFlags(UiElementTextbox.TEXT_OUTLINE);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			header.SetMargin(new UiRectangle(0.005f));
			AddElement(header);
			
			buttonModel.SetText("Model");
			buttonModel.SetFont(Font.Monospace);
			buttonModel.SetHDimension(Dimension.Fill);
			buttonModel.SetPadding(new UiRectangle(defaultPadding));
			buttonModel.SetProperties(CANFOCUS);
			buttonModel.RegisterOnLMBRelease(CloseAll);
			buttonModel.RegisterOnLMBRelease(windowModel.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonModel.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonModel.OnMouseButton);
			AddElement(buttonModel);

			buttonHeadBlendData.SetText("Head Blend Data");
			buttonHeadBlendData.SetFont(Font.Monospace);
			buttonHeadBlendData.SetHDimension(Dimension.Fill);
			buttonHeadBlendData.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadBlendData.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonHeadBlendData.SetProperties(CANFOCUS);
			buttonHeadBlendData.RegisterOnLMBRelease(CloseAll);
			buttonHeadBlendData.RegisterOnLMBRelease(windowHeadBlendData.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonHeadBlendData.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonHeadBlendData.OnMouseButton);
			AddElement(buttonHeadBlendData);

			buttonFaceFeatures.SetText("Face Features");
			buttonFaceFeatures.SetFont(Font.Monospace);
			buttonFaceFeatures.SetHDimension(Dimension.Fill);
			buttonFaceFeatures.SetPadding(new UiRectangle(defaultPadding));
			buttonFaceFeatures.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonFaceFeatures.SetProperties(CANFOCUS);
			buttonFaceFeatures.RegisterOnLMBRelease(CloseAll);
			buttonFaceFeatures.RegisterOnLMBRelease(windowFaceFeatures.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonFaceFeatures.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonFaceFeatures.OnMouseButton);
			AddElement(buttonFaceFeatures);

			buttonHairAndEyeColor.SetText("Hair and Eye Color");
			buttonHairAndEyeColor.SetFont(Font.Monospace);
			buttonHairAndEyeColor.SetHDimension(Dimension.Fill);
			buttonHairAndEyeColor.SetPadding(new UiRectangle(defaultPadding));
			buttonHairAndEyeColor.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonHairAndEyeColor.SetProperties(CANFOCUS);
			buttonHairAndEyeColor.RegisterOnLMBRelease(CloseAll);
			buttonHairAndEyeColor.RegisterOnLMBRelease(windowHairAndEyeColor.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonHairAndEyeColor.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonHairAndEyeColor.OnMouseButton);
			AddElement(buttonHairAndEyeColor);

			buttonHeadOverlays.SetText("Head Overlays");
			buttonHeadOverlays.SetFont(Font.Monospace);
			buttonHeadOverlays.SetHDimension(Dimension.Fill);
			buttonHeadOverlays.SetPadding(new UiRectangle(defaultPadding));
			buttonHeadOverlays.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonHeadOverlays.SetProperties(CANFOCUS);
			buttonHeadOverlays.RegisterOnLMBRelease(CloseAll);
			buttonHeadOverlays.RegisterOnLMBRelease(windowHeadOverlays.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonHeadOverlays.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonHeadOverlays.OnMouseButton);
			AddElement(buttonHeadOverlays);

			buttonDecorations.SetText("Decorations");
			buttonDecorations.SetFont(Font.Monospace);
			buttonDecorations.SetHDimension(Dimension.Fill);
			buttonDecorations.SetPadding(new UiRectangle(defaultPadding));
			buttonDecorations.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonDecorations.SetProperties(CANFOCUS);
			buttonDecorations.RegisterOnLMBRelease(CloseAll);
			buttonDecorations.RegisterOnLMBRelease(windowDecorations.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonDecorations.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonDecorations.OnMouseButton);
			AddElement(buttonDecorations);

			buttonComponents.SetText("Components");
			buttonComponents.SetFont(Font.Monospace);
			buttonComponents.SetHDimension(Dimension.Fill);
			buttonComponents.SetPadding(new UiRectangle(defaultPadding));
			buttonComponents.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonComponents.SetProperties(CANFOCUS);
			buttonComponents.RegisterOnLMBRelease(CloseAll);
			buttonComponents.RegisterOnLMBRelease(windowComponents.Toggle);
			WindowManager.RegisterOnMouseMoveCallback(buttonComponents.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonComponents.OnMouseButton);
			AddElement(buttonComponents);

			UiElementFiveM panelButtons = new UiElementFiveM();
			panelButtons.SetHDimension(Dimension.Fill);
			panelButtons.SetPadding(new UiRectangle(0f));
			panelButtons.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			panelButtons.SetFlags(TRANSPARENT);
			AddElement(panelButtons);

			UiElementFiveM panelButtonsLeft = new UiElementFiveM();
			panelButtonsLeft.SetHDimension(Dimension.Fill);
			panelButtonsLeft.SetGravity(HGravity.Left);
			panelButtonsLeft.SetPadding(new UiRectangle(0f));
			panelButtonsLeft.SetFlags(TRANSPARENT);
			panelButtons.AddElement(panelButtonsLeft);

			UiElementFiveM panelButtonsRight = new UiElementFiveM();
			panelButtonsRight.SetHDimension(Dimension.Fill);
			panelButtonsRight.SetGravity(HGravity.Right);
			panelButtonsRight.SetPadding(new UiRectangle(0f));
			panelButtonsRight.SetFlags(TRANSPARENT);
			panelButtons.AddElement(panelButtonsRight);

			Textbox buttonSave= new Textbox();
			buttonSave.SetText("Save");
			buttonSave.SetFont(Font.Monospace);
			buttonSave.SetPadding(new UiRectangle(defaultPadding));
			buttonSave.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonSave.SetProperties(CANFOCUS);
			buttonSave.RegisterOnLMBRelease(Save);
			WindowManager.RegisterOnMouseMoveCallback(buttonSave.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonSave.OnMouseButton);
			panelButtonsLeft.AddElement(buttonSave);

			Textbox buttonRevert= new Textbox();
			buttonRevert.SetText("Revert");
			buttonRevert.SetFont(Font.Monospace);
			buttonRevert.SetPadding(new UiRectangle(defaultPadding));
			buttonRevert.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonRevert.SetProperties(CANFOCUS);
			buttonRevert.RegisterOnLMBRelease(Revert);
			WindowManager.RegisterOnMouseMoveCallback(buttonRevert.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonRevert.OnMouseButton);
			panelButtonsRight.AddElement(buttonRevert);

			windowModel.CreateUi();
			windowModel.RegisterOnModelChange(OnModelChange);
			windowHeadBlendData.CreateUi();
			windowFaceFeatures.CreateUi();
			windowHairAndEyeColor.CreateUi();
			windowHeadOverlays.CreateUi();
			windowDecorations.CreateUi();
			windowComponents.CreateUi();

			WindowManager.AddWindow(windowModel);
			WindowManager.AddWindow(windowHeadBlendData);
			WindowManager.AddWindow(windowFaceFeatures);
			WindowManager.AddWindow(windowHairAndEyeColor);
			WindowManager.AddWindow(windowHeadOverlays);
			WindowManager.AddWindow(windowDecorations);
			WindowManager.AddWindow(windowComponents);

			Refresh();
		}

		private void Save()
		{
			foreach (fpVoid OnSave in onSaveCallbacks)
			{
				OnSave();
			}
			Close();
		}

		private void Revert()
		{
			foreach (fpVoid OnRevert in onRevertCallbacks)
			{
				OnRevert();
			}
			Close();
		}

		private async void OnModelChange(PedHash pedHash)
		{
			character.Model = ((uint)pedHash).ToString();

			if (pedHash == PedHash.FreemodeMale01)
			{
				character.Gender = (short)Gender.Male;
				await SetDefaults();
				await ApplyToPed();
			}
			else if (pedHash == PedHash.FreemodeFemale01)
			{
				character.Gender = (short)Gender.Female;
				await SetDefaults();
				await ApplyToPed();
			}
			else
			{
				windowComponents.ResetPedComponents();
			}

			UpdateButtons(character.ModelHash);
		}

		private void UpdateButtons(PedHash pedHash)
		{
			if (pedHash == PedHash.FreemodeMale01 || pedHash == PedHash.FreemodeFemale01)
			{
				buttonHeadBlendData.Enable();
				buttonFaceFeatures.Enable();
				buttonHairAndEyeColor.Enable();
				buttonHeadOverlays.Enable();
			}
			else
			{
				buttonHeadBlendData.Disable();
				buttonFaceFeatures.Disable();
				buttonHairAndEyeColor.Disable();
				buttonHeadOverlays.Disable();
			}
		}

		void CloseAll()
		{
			windowModel.Close();
			windowHeadBlendData.Close();
			windowFaceFeatures.Close();
			windowHairAndEyeColor.Close();
			windowHeadOverlays.Close();
			windowDecorations.Close();
			windowComponents.Close();
		}
	}
}
