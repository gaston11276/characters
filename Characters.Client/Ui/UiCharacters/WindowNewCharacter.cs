using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Spawnmenu.Client;

namespace Gaston11276.Characters.Client
{
	public class WindowNewCharacter : Window
	{
		Editbox editForename = new Editbox();
		Editbox editMiddlename = new Editbox();
		Editbox editSurname = new Editbox();
		Editbox editGender = new Editbox();
		Editbox editDateOfBirth = new Editbox();

		PanelSpawnLocation panelSpawnLocation = new PanelSpawnLocation();

		Textbox buttonCreate = new Textbox();

		protected List<fpGuid> onWindowCharacterCloseCallbacks = new List<fpGuid>();
		protected List<fpVoid> onCreateCallbacks = new List<fpVoid>();

		public WindowNewCharacter()
		{
			defaultPadding = 0.0025f;
		}

		protected override void OnOpen()
		{
			base.OnOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}

		public void RegisterOnCreateCallback(fpVoid OnCreate)
		{
			onCreateCallbacks.Add(OnCreate);
		}

		public async Task SetUi()
		{
			await panelSpawnLocation.SetUi();
		}

		public void SetCharacterInfo(Character character)
		{
			character.Forename = editForename.GetText();
			character.Middlename = editMiddlename.GetText();
			character.Surname = editSurname.GetText();

			if (string.IsNullOrWhiteSpace(character.Middlename)) character.Middlename = null;

			if (editGender.GetText().StartsWith("F"))
			{
				character.Gender = (short)Gender.Female;
				character.Model = ((uint)PedHash.FreemodeFemale01).ToString();
			}
			else
			{
				character.Gender = (short)Gender.Male;
				character.Model = ((uint)PedHash.FreemodeMale01).ToString();
			}

			character.AnimationSet = "move_m@drunk@verydrunk";

			DateTime birthdate = new DateTime(2000, 01, 01);
			character.DateOfBirth = birthdate;

			Vector3 spawnLocation = panelSpawnLocation.GetSpawnLocation();
			NFive.SDK.Core.Models.Position pos = new NFive.SDK.Core.Models.Position(spawnLocation.X, spawnLocation.Y, spawnLocation.Z);
			character.Position = pos;
		}

		public void ClearEdit()
		{
			editForename.ClearText();
			editMiddlename.ClearText();
			editSurname.ClearText();
			editGender.SetText("M/F");
			editDateOfBirth.SetText("2000-01-01");
			buttonCreate.Disable();
		}

		protected void CreateColumn(UiElementFiveM uiPanel, Dimension hDimension, HGravity gravity, UiElementFiveM uiColumn, string label = null)
		{
			uiColumn.SetOrientation(Orientation.Vertical);
			uiColumn.SetPadding(new UiRectangle(defaultPadding));
			uiColumn.SetGravity(gravity);
			uiColumn.SetHDimension(hDimension);
			uiPanel.AddElement(uiColumn);

			if (label != null)
			{
				Textbox header = new Textbox();
				header.SetPadding(new UiRectangle(defaultPadding));
				header.SetText(label);
				header.SetFlags(UiElement.TRANSPARENT);
				if (label.Length == 0)
				{
					header.SetTextFlags(UiElement.TRANSPARENT);
				}
				uiColumn.AddElement(header);
			}
		}

		public override void CreateUi()
		{
			base.CreateUi();

			SetFlags(HIDDEN);
			SetPadding(new UiRectangle(defaultPadding));
			SetAlignment(VAlignment.Top);
			SetAlignment(HAlignment.Right);
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetOrientation(Orientation.Vertical);

			Textbox header = new Textbox();
			header.SetText("New Character");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.6f);
			header.SetTextFlags(UiElementTextbox.TEXT_OUTLINE);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			header.SetMargin(new UiRectangle(0.005f));
			AddElement(header);

			UiElementFiveM panelEntries = new UiElementFiveM();
			panelEntries.SetHDimension(Dimension.Fill);
			panelEntries.SetPadding(new UiRectangle(0));
			panelEntries.SetMargin(new UiRectangle(0f, -defaultPadding, 0f, 0f));
			panelEntries.SetFlags(TRANSPARENT);
			AddElement(panelEntries);

			UiElementFiveM columnLabels = new UiElementFiveM();
			columnLabels.SetFlags(TRANSPARENT);
			CreateColumn(panelEntries, Dimension.Wrap, HGravity.Left, columnLabels);

			Textbox labelForename = new Textbox();
			labelForename.SetPadding(new UiRectangle(defaultPadding));
			labelForename.SetFlags(TRANSPARENT);
			labelForename.SetText("Forename:");
			labelForename.SetFont(Font.CharletComprimeColonge);
			columnLabels.AddElement(labelForename);
			Textbox labelMiddlename = new Textbox();
			labelMiddlename.SetPadding(new UiRectangle(defaultPadding));
			labelMiddlename.SetFlags(TRANSPARENT);
			labelMiddlename.SetText("Middlename:");
			labelMiddlename.SetFont(Font.CharletComprimeColonge);
			columnLabels.AddElement(labelMiddlename);
			Textbox labelSurname = new Textbox();
			labelSurname.SetPadding(new UiRectangle(defaultPadding));
			labelSurname.SetFlags(TRANSPARENT);
			labelSurname.SetText("Surname:");
			labelSurname.SetFont(Font.CharletComprimeColonge);
			columnLabels.AddElement(labelSurname);
			Textbox labelGender = new Textbox();
			labelGender.SetPadding(new UiRectangle(defaultPadding));
			labelGender.SetFlags(TRANSPARENT);
			labelGender.SetText("Gender:");
			labelGender.SetFont(Font.CharletComprimeColonge);
			columnLabels.AddElement(labelGender);
			Textbox labelDateOfBirth = new Textbox();
			labelDateOfBirth.SetPadding(new UiRectangle(defaultPadding));
			labelDateOfBirth.SetFlags(TRANSPARENT);
			labelDateOfBirth.SetText("Date of Birth:");
			labelDateOfBirth.SetFont(Font.CharletComprimeColonge);
			columnLabels.AddElement(labelDateOfBirth);

			UiElementFiveM columnEdits = new UiElementFiveM();
			columnEdits.SetFlags(TRANSPARENT);
			CreateColumn(panelEntries, Dimension.Fill, HGravity.Left, columnEdits);

			editForename.SetPadding(new UiRectangle(defaultPadding));
			editForename.SetHDimension(Dimension.Fill);
			editForename.SetGravity(HGravity.Left);
			editForename.SetProperties(CANFOCUS | SELECTABLE);
			editForename.SetFont(Font.CharletComprimeColonge);
			editForename.RegisterOnTextChanged(OnTextChanged);
			WindowManager.RegisterOnKeyCallback(editForename.OnKey);
			WindowManager.RegisterOnMouseButtonCallback(editForename.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(editForename.OnMouseMove);
			columnEdits.AddElement(editForename);
			
			editMiddlename.SetPadding(new UiRectangle(defaultPadding));
			editMiddlename.SetHDimension(Dimension.Fill);
			editMiddlename.SetGravity(HGravity.Left);
			editMiddlename.SetProperties(CANFOCUS | SELECTABLE);
			editMiddlename.SetFont(Font.CharletComprimeColonge);
			editMiddlename.RegisterOnTextChanged(OnTextChanged);
			WindowManager.RegisterOnKeyCallback(editMiddlename.OnKey);
			WindowManager.RegisterOnMouseButtonCallback(editMiddlename.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(editMiddlename.OnMouseMove);
			columnEdits.AddElement(editMiddlename);
			
			editSurname.SetPadding(new UiRectangle(defaultPadding));
			editSurname.SetHDimension(Dimension.Fill);
			editSurname.SetGravity(HGravity.Left);
			editSurname.SetProperties(CANFOCUS | SELECTABLE);
			editSurname.SetFont(Font.CharletComprimeColonge);
			editSurname.RegisterOnTextChanged(OnTextChanged);
			WindowManager.RegisterOnKeyCallback(editSurname.OnKey);
			WindowManager.RegisterOnMouseButtonCallback(editSurname.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(editSurname.OnMouseMove);
			columnEdits.AddElement(editSurname);

			editGender.SetText("M/F");
			editGender.SetPadding(new UiRectangle(defaultPadding));
			editGender.SetHDimension(Dimension.Fill);
			editGender.SetGravity(HGravity.Left);
			editGender.SetProperties(CANFOCUS | SELECTABLE);
			editGender.SetFont(Font.CharletComprimeColonge);
			editGender.RegisterOnTextChanged(OnTextChanged);
			WindowManager.RegisterOnKeyCallback(editGender.OnKey);
			WindowManager.RegisterOnMouseButtonCallback(editGender.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(editGender.OnMouseMove);
			columnEdits.AddElement(editGender);
			
			editDateOfBirth.SetText("2000-01-01");
			editDateOfBirth.SetPadding(new UiRectangle(defaultPadding));
			editDateOfBirth.SetHDimension(Dimension.Fill);
			editDateOfBirth.SetGravity(HGravity.Left);
			editDateOfBirth.SetProperties(CANFOCUS | SELECTABLE);
			editDateOfBirth.SetFont(Font.CharletComprimeColonge);
			editDateOfBirth.RegisterOnTextChanged(OnTextChanged);
			WindowManager.RegisterOnKeyCallback(editDateOfBirth.OnKey);
			WindowManager.RegisterOnMouseButtonCallback(editDateOfBirth.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(editDateOfBirth.OnMouseMove);
			columnEdits.AddElement(editDateOfBirth);

			panelSpawnLocation.CreateUi();
			AddElement(panelSpawnLocation);

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

			UiElementFiveM panelButtonsRight = new UiElementFiveM();
			panelButtonsRight.SetHDimension(Dimension.Fill);
			panelButtonsRight.SetPadding(new UiRectangle(0f));
			panelButtonsRight.SetFlags(TRANSPARENT);
			panelButtonsRight.SetGravity(HGravity.Right);
			panelButtons.AddElement(panelButtonsRight);

			buttonCreate.SetText("Create");
			buttonCreate.SetFont(Font.CharletComprimeColonge);
			buttonCreate.SetPadding(new UiRectangle(defaultPadding));
			buttonCreate.SetProperties(CANFOCUS);
			buttonCreate.RegisterOnLMBRelease(OnCreate);
			buttonCreate.Disable();
			WindowManager.RegisterOnMouseMoveCallback(buttonCreate.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(buttonCreate.OnMouseButton);
			panelButtonsRight.AddElement(buttonCreate);

			Textbox uiButtonCancel = new Textbox();
			uiButtonCancel.SetText("Cancel");
			uiButtonCancel.SetFont(Font.CharletComprimeColonge);
			uiButtonCancel.SetPadding(new UiRectangle(defaultPadding));
			uiButtonCancel.SetMargin(new UiRectangle(-defaultPadding, 0f, 0f, 0f));
			uiButtonCancel.SetProperties(CANFOCUS);
			uiButtonCancel.RegisterOnLMBRelease(Close);
			WindowManager.RegisterOnMouseMoveCallback(uiButtonCancel.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(uiButtonCancel.OnMouseButton);
			panelButtonsLeft.AddElement(uiButtonCancel);

		}

		private void OnTextChanged()
		{
			if (editForename.GetText().Length > 0 && editSurname.GetText().Length > 0)
			{
				buttonCreate.Enable();
			}
			else
			{
				buttonCreate.Disable();
			}
		}

		private void OnCreate()
		{
			foreach (fpVoid onCreate in onCreateCallbacks)
			{
				onCreate();
			}
			ClearEdit();
			Close();
		}
	}
}
