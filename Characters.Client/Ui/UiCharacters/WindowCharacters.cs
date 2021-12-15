using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;



namespace Gaston11276.Characters.Client
{
	public class WindowCharacters: Window
	{
		UiElementFiveM panelCharacters = new UiElementFiveM();
		List<Character> characters = new List<Character>();
		Guid selectedCharacterId;

		Textbox buttonPlay = new Textbox();
		Textbox buttonDelete = new Textbox();

		protected List<fpGuid> onWindowCharacterCloseCallbacks = new List<fpGuid>();
		protected List<fpGuid> onPlayCallbacks = new List<fpGuid>();
		protected List<fpGuid> onDeleteCallbacks = new List<fpGuid>();

		WindowNewCharacter windowNewCharacter = new WindowNewCharacter();

		public WindowCharacters()
		{
			defaultPadding = 0.0025f;
		}

		protected override void OnOpen()
		{
			Game.Player.CanControlCharacter = false;
			//API.FreezePedCameraRotation(Game.PlayerPed.Handle);
			//Game.Player.Character.IsPositionFrozen = true;

			//panelCharacters.Clear();
			//Textbox entryCharacter = new Textbox();
			//entryCharacter.SetText("Characters loading...");
			//entryCharacter.SetPadding(new UiRectangle(defaultPadding));
			//panelCharacters.AddElement(entryCharacter);
			//Refresh();

			base.OnOpen();
		}

		protected override void OnClose()
		{
			windowNewCharacter.Close();

			buttonPlay.Disable();
			buttonDelete.Disable();
			ClearSelect();

			Game.Player.CanControlCharacter = true;
			//Game.Player.Character.IsPositionFrozen = false;
			base.OnClose();

			foreach (fpGuid onClose in onWindowCharacterCloseCallbacks)
			{
				onClose(selectedCharacterId);
			}
		}

		void OnPlay()
		{
			foreach (fpGuid onPlay in onPlayCallbacks)
			{
				Logger.Debug($"OnPlay() Character Id: {selectedCharacterId}");
				onPlay(selectedCharacterId);
			}
			Close();
		}

		void OnDelete()
		{
			foreach (fpGuid onDelete in onDeleteCallbacks)
			{
				onDelete(selectedCharacterId);
			}
		}

		public void RegisterOnCloseCallback(fpGuid OnClose)
		{
			onWindowCharacterCloseCallbacks.Add(OnClose);
		}

		public void RegisterOnPlayCallback(fpGuid OnPlay)
		{
			onPlayCallbacks.Add(OnPlay);
		}

		public void RegisterOnCreateCallback(fpVoid OnCreate)
		{
			windowNewCharacter.RegisterOnCreateCallback(OnCreate);
		}

		public void RegisterOnDeleteCallback(fpGuid OnDelete)
		{
			onDeleteCallbacks.Add(OnDelete);
		}

		public async void SetUi()
		{
			await windowNewCharacter.SetUi();
		}

		public void SetCharacters(List<Character> characters)
		{
			this.characters = characters;
		}

		public void UpdateCharacterList()
		{
			panelCharacters.Clear();

			foreach (Character character in characters)
			{
				Textbox entryCharacter = new Textbox();
				entryCharacter.SetText(character.FullName);
				entryCharacter.SetFont(Font.HouseScript);
				entryCharacter.SetFontSize(0.4f);
				entryCharacter.Id = character.Id;
				entryCharacter.SetHDimension(Dimension.Fill);
				entryCharacter.SetPadding(new UiRectangle(defaultPadding));
				entryCharacter.SetProperties(CANFOCUS | SELECTABLE);
				entryCharacter.RegisterOnSelectIdCallback(OnCharacterSelect);
				entryCharacter.RegisterOffSelect(OffCharacterSelect);
				WindowManager.RegisterOnMouseMoveCallback(entryCharacter.OnMouseMove);
				WindowManager.RegisterOnMouseButtonCallback(entryCharacter.OnMouseButton);
				panelCharacters.AddElement(entryCharacter);
			}
			Refresh();
		}

		private void ClearSelect()
		{
			foreach (UiElementFiveM element in panelCharacters.GetElements())
			{
				element.ClearFlags(SELECTED);
			}
		}

		private void OnCharacterSelect(Guid Id)
		{
			selectedCharacterId = Id;
			buttonPlay.Enable();
			buttonDelete.Enable();
		}

		private void OffCharacterSelect()
		{
			Logger.Debug("WindowCharacters: OffCharacterSelect");
			buttonPlay.Disable();
			buttonDelete.Disable();
		}

		public void SetCharacterInfo(Character character)
		{
			windowNewCharacter.SetCharacterInfo(character);
		}

		public void ClearCharacterListEdit()
		{
			//uiNewCharacter.ClearCharacterListEdit();
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

		public override void CreateUi()
		{
			base.CreateUi();

			SetFlags(HIDDEN);
			SetPadding(new UiRectangle(defaultPadding));
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetOrientation(Orientation.Vertical);

		
			Textbox header = new Textbox();
			header.SetText("characters");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.6f);
			header.SetTextFlags(UiElementTextbox.TEXT_OUTLINE);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			header.SetMargin(new UiRectangle(0.005f));
			AddElement(header);

			panelCharacters.SetPadding(new UiRectangle(0f));
			panelCharacters.SetOrientation(Orientation.Vertical);
			panelCharacters.SetHDimension(Dimension.Fill);
			panelCharacters.SetFlags(TRANSPARENT);
			AddElement(panelCharacters);


			//CreateColumns();

			UiElementFiveM panelButtons = new UiElementFiveM();
			panelButtons.SetHDimension(Dimension.Fill);
			panelButtons.SetPadding(new UiRectangle(0f));
			panelButtons.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			panelButtons.SetFlags(TRANSPARENT);
			AddElement(panelButtons);

			UiElementFiveM panelButtonsLeft = new UiElementFiveM();
			panelButtonsLeft.SetHDimension(Dimension.Fill);
			panelButtonsLeft.SetPadding(new UiRectangle(0f));
			panelButtonsLeft.SetFlags(TRANSPARENT);
			panelButtonsLeft.SetGravity(HGravity.Left);
			panelButtons.AddElement(panelButtonsLeft);

			UiElementFiveM panelButtonsRight= new UiElementFiveM();
			panelButtonsRight.SetHDimension(Dimension.Fill);
			panelButtonsRight.SetPadding(new UiRectangle(0f));
			panelButtonsRight.SetFlags(TRANSPARENT);
			panelButtonsRight.SetGravity(HGravity.Right);
			panelButtons.AddElement(panelButtonsRight);

			buttonPlay.SetText("Play");
			buttonPlay.SetFont(Font.Monospace);
			buttonPlay.SetPadding(new UiRectangle(defaultPadding));
			buttonPlay.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			buttonPlay.SetProperties(CANFOCUS);
			buttonPlay.Disable();
			buttonPlay.RegisterOnLMBRelease(OnPlay);
			WindowManager.RegisterOnMouseMoveCallback(buttonPlay.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonPlay.OnMouseButton);
			panelButtonsLeft.AddElement(buttonPlay);

			buttonDelete.SetText("Delete");
			buttonDelete.SetFont(Font.Monospace);
			buttonDelete.SetPadding(new UiRectangle(defaultPadding));
			buttonDelete.SetMargin(new UiRectangle(-defaultPadding, 0f, 0f, 0f));
			buttonDelete.SetProperties(CANFOCUS);
			buttonDelete.RegisterOnLMBRelease(OnDelete);
			buttonDelete.Disable();
			WindowManager.RegisterOnMouseMoveCallback(buttonDelete.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonDelete.OnMouseButton);
			panelButtonsRight.AddElement(buttonDelete);

			Textbox uiButtonNew = new Textbox();
			uiButtonNew.SetText("New");
			uiButtonNew.SetFont(Font.Monospace);
			uiButtonNew.SetPadding(new UiRectangle(defaultPadding));
			uiButtonNew.SetMargin(new UiRectangle(-defaultPadding, 0f, 0f, 0f));
			uiButtonNew.SetProperties(CANFOCUS);
			uiButtonNew.RegisterOnLMBRelease(OnNew);
			WindowManager.RegisterOnMouseMoveCallback(uiButtonNew.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(uiButtonNew.OnMouseButton);
			panelButtonsRight.AddElement(uiButtonNew);

			windowNewCharacter.CreateUi();
			WindowManager.AddWindow(windowNewCharacter);

			Refresh();
		}

		private void OnNew()
		{
			windowNewCharacter.Toggle();
		}
	}
}
