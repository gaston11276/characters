using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowHeadOverlays : Window
	{
		UiElementFiveM uiColumnIndexLabels = new UiElementFiveM();
		UiElementFiveM uiColumnIndexValues = new UiElementFiveM();
		UiElementFiveM uiColumnIndexDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnIndexIncrease = new UiElementFiveM();
		UiElementFiveM uiColumnColorValues = new UiElementFiveM();
		UiElementFiveM uiColumnColorDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnColorIncrease = new UiElementFiveM();
		UiElementFiveM uiColumnOpacityValues = new UiElementFiveM();
		UiElementFiveM uiColumnOpacityDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnOpacityIncrease = new UiElementFiveM();

		EntryHeadOverlay uiBlemishes;
		EntryHeadOverlay uiFacialHair;
		EntryHeadOverlay uiEyebrows;
		EntryHeadOverlay uiAgeing;
		EntryHeadOverlay uiMakeup;
		EntryHeadOverlay uiBlush;
		EntryHeadOverlay uiComplexion;
		EntryHeadOverlay uiSunDamage;
		EntryHeadOverlay uiLipstick;
		EntryHeadOverlay uiMolesFreckles;
		EntryHeadOverlay uiChestHair;
		EntryHeadOverlay uiBodyBlemishes;

		Character character;

		public WindowHeadOverlays()
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
			await uiBlemishes.SetUi();
			await uiFacialHair.SetUi();
			await uiEyebrows.SetUi();
			await uiAgeing.SetUi();
			await uiMakeup.SetUi();
			await uiBlush.SetUi();
			await uiComplexion.SetUi();
			await uiSunDamage.SetUi();
			await uiLipstick.SetUi();
			await uiMolesFreckles.SetUi();
			await uiChestHair.SetUi();
			await uiBodyBlemishes.SetUi();
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

			CreateColumn(panel, HGravity.Left, uiColumnIndexLabels, "");
			CreateColumn(panel, HGravity.Right, uiColumnIndexValues, "");
			CreateColumn(panel, HGravity.Center, uiColumnIndexDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnIndexIncrease, "");
			CreateColumn(panel, HGravity.Right, uiColumnColorValues, "Color");
			CreateColumn(panel, HGravity.Center, uiColumnColorDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnColorIncrease, "");
			CreateColumn(panel, HGravity.Right, uiColumnOpacityValues, "Opacity");
			CreateColumn(panel, HGravity.Center, uiColumnOpacityDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnOpacityIncrease, "");
		}

		private EntryHeadOverlay CreateEntry(PedHeadOverlayType type, string label)
		{
			EntryHeadOverlay entry = new EntryHeadOverlay();

			entry.type = type;

			entry.uiOverlayLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiOverlayLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOverlayLabel.SetText(label);
			entry.uiOverlayLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexLabels.AddElement(entry.uiOverlayLabel);

			entry.uiOverlayIndex.SetFont(Font.CharletComprimeColonge);
			entry.uiOverlayIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOverlayIndex.SetProperties(UiElement.CANFOCUS);
			entry.uiOverlayIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexValues.AddElement(entry.uiOverlayIndex);

			entry.btnIndexDecrease.SetText("-");
			entry.btnIndexDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnIndexDecrease);

			entry.btnIndexIncrease.SetText("+");
			entry.btnIndexIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIndexIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnIndexIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIndexIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIndexIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnIndexIncrease);

			entry.uiColorId.SetFont(Font.CharletComprimeColonge);
			entry.uiColorId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiColorId.SetFlags(UiElement.TRANSPARENT);
			uiColumnColorValues.AddElement(entry.uiColorId);

			entry.btnColorIdDecrease.SetText("-");
			entry.btnColorIdDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdDecrease.RegisterOnLMBRelease(entry.DecreaseColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnColorIdDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnColorIdDecrease.OnMouseButton);
			uiColumnColorDecrease.AddElement(entry.btnColorIdDecrease);

			entry.btnColorIdIncrease.SetText("+");
			entry.btnColorIdIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnColorIdIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnColorIdIncrease.RegisterOnLMBRelease(entry.IncreaseColor);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnColorIdIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnColorIdIncrease.OnMouseButton);
			uiColumnColorIncrease.AddElement(entry.btnColorIdIncrease);

			// Opacity
			entry.uiOpacity.SetFont(Font.CharletComprimeColonge);
			entry.uiOpacity.SetPadding(new UiRectangle(defaultPadding));
			entry.uiOpacity.SetFlags(UiElement.TRANSPARENT);
			uiColumnOpacityValues.AddElement(entry.uiOpacity);

			entry.btnOpacityDecrease.SetText("-");
			entry.btnOpacityDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnOpacityDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnOpacityDecrease.RegisterOnLMBRelease(entry.DecreaseOpacity);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnOpacityDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnOpacityDecrease.OnMouseButton);
			uiColumnOpacityDecrease.AddElement(entry.btnOpacityDecrease);

			entry.btnOpacityIncrease.SetText("+");
			entry.btnOpacityIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnOpacityIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnOpacityIncrease.RegisterOnLMBRelease(entry.IncreaseOpacity);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnOpacityIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnOpacityIncrease.OnMouseButton);
			uiColumnOpacityIncrease.AddElement(entry.btnOpacityIncrease);

			entry.GetIndexMax = GetHeadOverlayIndexMax;
			entry.GetIndex = GetHeadOverlayIndex;
			entry.SetIndex = SetHeadOverlayIndex;
			entry.GetColorMax = GetHeadOverlayColorMax;
			entry.GetColor = GetHeadOverlayColor;
			entry.SetColor = SetHeadOverlayColor;

			entry.SetOpacity = SetHeadOverlayOpacity;
			entry.GetOpacity = GetHeadOverlayOpacity;

			return entry;
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
			header.SetText("Head Overlays");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			CreateColumns();

			uiBlemishes = CreateEntry(PedHeadOverlayType.Blemishes, "Blemishes");
			uiFacialHair = CreateEntry(PedHeadOverlayType.FacialHair, "FacialHair");
			uiEyebrows = CreateEntry(PedHeadOverlayType.Eyebrows, "Eyebrows");
			uiAgeing = CreateEntry(PedHeadOverlayType.Ageing, "Ageing");
			uiMakeup = CreateEntry(PedHeadOverlayType.Makeup, "Makeup");
			uiBlush = CreateEntry(PedHeadOverlayType.Blush, "Blush");
			uiComplexion = CreateEntry(PedHeadOverlayType.Complexion, "Complexion");
			uiSunDamage = CreateEntry(PedHeadOverlayType.SunDamage, "SunDamage");
			uiLipstick = CreateEntry(PedHeadOverlayType.Lipstick, "Lipstick");
			uiMolesFreckles = CreateEntry(PedHeadOverlayType.MolesAndFreckles, "MolesFreckles");
			uiChestHair = CreateEntry(PedHeadOverlayType.ChestHair, "ChestHair");
			uiBodyBlemishes = CreateEntry(PedHeadOverlayType.BodyBlemishes, "BodyBlemishes");
		}

		public int GetHeadOverlayIndex(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return character.PedHeadOverlays.Blemishes.Index;
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.Index;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.Index;
				case PedHeadOverlayType.Ageing:
					return character.PedHeadOverlays.Ageing.Index;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.Index;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.Index;
				case PedHeadOverlayType.Complexion:
					return character.PedHeadOverlays.Complexion.Index;
				case PedHeadOverlayType.SunDamage:
					return character.PedHeadOverlays.SunDamage.Index;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.Index;
				case PedHeadOverlayType.MolesAndFreckles:
					return character.PedHeadOverlays.MolesAndFreckles.Index;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.Index;
				case PedHeadOverlayType.BodyBlemishes:
					return character.PedHeadOverlays.BodyBlemishes.Index;
				default:
					return 0;
			}
		}

		public int GetHeadOverlayIndexMax(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Blemishes);
				case PedHeadOverlayType.FacialHair:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.FacialHair);
				case PedHeadOverlayType.Eyebrows:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Eyebrows);
				case PedHeadOverlayType.Ageing:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Ageing);
				case PedHeadOverlayType.Makeup:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Makeup);
				case PedHeadOverlayType.Blush:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Blush);
				case PedHeadOverlayType.Complexion:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Complexion);
				case PedHeadOverlayType.SunDamage:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.SunDamage);
				case PedHeadOverlayType.Lipstick:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.Lipstick);
				case PedHeadOverlayType.MolesAndFreckles:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.MolesAndFreckles);
				case PedHeadOverlayType.ChestHair:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.ChestHair);
				case PedHeadOverlayType.BodyBlemishes:
					return API.GetNumHeadOverlayValues((int)PedHeadOverlayType.BodyBlemishes);
				default:
					return 0;
			}
		}

		public void SetHeadOverlayIndex(PedHeadOverlayType type, int index)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					character.PedHeadOverlays.Blemishes.Index = index;
					break;
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.Index = index;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.Index = index;
					break;
				case PedHeadOverlayType.Ageing:
					character.PedHeadOverlays.Ageing.Index = index;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.Index = index;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.Index = index;
					break;
				case PedHeadOverlayType.Complexion:
					character.PedHeadOverlays.Complexion.Index = index;
					break;
				case PedHeadOverlayType.SunDamage:
					character.PedHeadOverlays.SunDamage.Index = index;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.Index = index;
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					character.PedHeadOverlays.MolesAndFreckles.Index = index;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.Index = index;
					break;
				case PedHeadOverlayType.BodyBlemishes:
					character.PedHeadOverlays.BodyBlemishes.Index = index;
					break;
				default:
					break;
			}
			ApplyOverlayToPed(type);
		}

		public int GetHeadOverlayColor(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.ColorId;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.ColorId;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.ColorId;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.ColorId;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.ColorId;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.ColorId;
				default:
					return 0;
			}
		}

		public int GetHeadOverlayColorMax(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
				case PedHeadOverlayType.Eyebrows:
				case PedHeadOverlayType.ChestHair:
					return API.GetNumHairColors();
				case PedHeadOverlayType.Makeup:
				case PedHeadOverlayType.Blush:
				case PedHeadOverlayType.Lipstick:
					return API.GetNumMakeupColors();
				default:
					return 0;
			}
		}

		public void SetHeadOverlayColor(PedHeadOverlayType type, int colorId)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.ColorId = colorId;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.ColorId = colorId;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.ColorId = colorId;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.ColorId = colorId;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.ColorId = colorId;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.ColorId = colorId;
					break;
				default:
					return;
			}

			ApplyOverlayColorToPed(type);
		}

		public int GetHeadOverlaySecondaryColor(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.SecondColorId;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.SecondColorId;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.SecondColorId;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.SecondColorId;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.SecondColorId;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.SecondColorId;
				default:
					return 0;
			}
		}

		public void SetHeadOverlaySecondaryColor(PedHeadOverlayType type, int colorId)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.SecondColorId = colorId;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.SecondColorId = colorId;
					break;
				default:
					return;
			}
			ApplyOverlayColorToPed(type);
		}

		public float GetHeadOverlayOpacity(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					return character.PedHeadOverlays.Blemishes.Opacity;
				case PedHeadOverlayType.FacialHair:
					return character.PedHeadOverlays.FacialHair.Opacity;
				case PedHeadOverlayType.Eyebrows:
					return character.PedHeadOverlays.Eyebrows.Opacity;
				case PedHeadOverlayType.Ageing:
					return character.PedHeadOverlays.Ageing.Opacity;
				case PedHeadOverlayType.Makeup:
					return character.PedHeadOverlays.Makeup.Opacity;
				case PedHeadOverlayType.Blush:
					return character.PedHeadOverlays.Blush.Opacity;
				case PedHeadOverlayType.Complexion:
					return character.PedHeadOverlays.Complexion.Opacity;
				case PedHeadOverlayType.SunDamage:
					return character.PedHeadOverlays.SunDamage.Opacity;
				case PedHeadOverlayType.Lipstick:
					return character.PedHeadOverlays.Lipstick.Opacity;
				case PedHeadOverlayType.MolesAndFreckles:
					return character.PedHeadOverlays.MolesAndFreckles.Opacity;
				case PedHeadOverlayType.ChestHair:
					return character.PedHeadOverlays.ChestHair.Opacity;
				case PedHeadOverlayType.BodyBlemishes:
					return character.PedHeadOverlays.BodyBlemishes.Opacity;
				default:
					return 0f;
			}
		}

		public void SetHeadOverlayOpacity(PedHeadOverlayType type, float opacity)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					character.PedHeadOverlays.Blemishes.Opacity = opacity;
					break;
				case PedHeadOverlayType.FacialHair:
					character.PedHeadOverlays.FacialHair.Opacity = opacity;
					break;
				case PedHeadOverlayType.Eyebrows:
					character.PedHeadOverlays.Eyebrows.Opacity = opacity;
					break;
				case PedHeadOverlayType.Ageing:
					character.PedHeadOverlays.Ageing.Opacity = opacity;
					break;
				case PedHeadOverlayType.Makeup:
					character.PedHeadOverlays.Makeup.Opacity = opacity;
					break;
				case PedHeadOverlayType.Blush:
					character.PedHeadOverlays.Blush.Opacity = opacity;
					break;
				case PedHeadOverlayType.Complexion:
					character.PedHeadOverlays.Complexion.Opacity = opacity;
					break;
				case PedHeadOverlayType.SunDamage:
					character.PedHeadOverlays.SunDamage.Opacity = opacity;
					break;
				case PedHeadOverlayType.Lipstick:
					character.PedHeadOverlays.Lipstick.Opacity = opacity;
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					character.PedHeadOverlays.MolesAndFreckles.Opacity = opacity;
					break;
				case PedHeadOverlayType.ChestHair:
					character.PedHeadOverlays.ChestHair.Opacity = opacity;
					break;
				case PedHeadOverlayType.BodyBlemishes:
					character.PedHeadOverlays.BodyBlemishes.Opacity = opacity;
					break;
				default:
					break;
			}
			ApplyOverlayToPed(type);
		}

		public void ApplyOverlayToPed(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.Blemishes:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blemishes.Type, character.PedHeadOverlays.Blemishes.Index, character.PedHeadOverlays.Blemishes.Opacity);
					break;
				case PedHeadOverlayType.FacialHair:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, character.PedHeadOverlays.FacialHair.Index, character.PedHeadOverlays.FacialHair.Opacity);
					break;
				case PedHeadOverlayType.Eyebrows:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, character.PedHeadOverlays.Eyebrows.Index, character.PedHeadOverlays.Eyebrows.Opacity);
					break;
				case PedHeadOverlayType.Ageing:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Ageing.Type, character.PedHeadOverlays.Ageing.Index, character.PedHeadOverlays.Ageing.Opacity);
					break;
				case PedHeadOverlayType.Makeup:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, character.PedHeadOverlays.Makeup.Index, character.PedHeadOverlays.Makeup.Opacity);
					break;
				case PedHeadOverlayType.Blush:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, character.PedHeadOverlays.Blush.Index, character.PedHeadOverlays.Blush.Opacity);
					break;
				case PedHeadOverlayType.Complexion:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Complexion.Type, character.PedHeadOverlays.Complexion.Index, character.PedHeadOverlays.Complexion.Opacity);
					break;
				case PedHeadOverlayType.SunDamage:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.SunDamage.Type, character.PedHeadOverlays.SunDamage.Index, character.PedHeadOverlays.SunDamage.Opacity);
					break;
				case PedHeadOverlayType.Lipstick:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, character.PedHeadOverlays.Lipstick.Index, character.PedHeadOverlays.Lipstick.Opacity);
					break;
				case PedHeadOverlayType.MolesAndFreckles:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.MolesAndFreckles.Type, character.PedHeadOverlays.MolesAndFreckles.Index, character.PedHeadOverlays.MolesAndFreckles.Opacity);
					break;
				case PedHeadOverlayType.ChestHair:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, character.PedHeadOverlays.ChestHair.Index, character.PedHeadOverlays.ChestHair.Opacity);
					break;
				case PedHeadOverlayType.BodyBlemishes:
					API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.BodyBlemishes.Type, character.PedHeadOverlays.BodyBlemishes.Index, character.PedHeadOverlays.BodyBlemishes.Opacity);
					break;
				default:
					break;
			}
		}

		public void ApplyOverlayColorToPed(PedHeadOverlayType type)
		{
			switch (type)
			{
				case PedHeadOverlayType.FacialHair:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.ColorType, (int)character.PedHeadOverlays.FacialHair.ColorType, character.PedHeadOverlays.FacialHair.ColorId, character.PedHeadOverlays.FacialHair.SecondColorId);
					break;
				case PedHeadOverlayType.Eyebrows:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, (int)character.PedHeadOverlays.Eyebrows.ColorType, character.PedHeadOverlays.Eyebrows.ColorId, character.PedHeadOverlays.Eyebrows.SecondColorId);
					break;
				case PedHeadOverlayType.Makeup:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, (int)character.PedHeadOverlays.Makeup.ColorType, character.PedHeadOverlays.Makeup.ColorId, character.PedHeadOverlays.Makeup.SecondColorId);
					break;
				case PedHeadOverlayType.Blush:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, (int)character.PedHeadOverlays.Blush.ColorType, character.PedHeadOverlays.Blush.ColorId, character.PedHeadOverlays.Blush.SecondColorId);
					break;
				case PedHeadOverlayType.Lipstick:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, (int)character.PedHeadOverlays.Lipstick.ColorType, character.PedHeadOverlays.Lipstick.ColorId, character.PedHeadOverlays.Lipstick.SecondColorId);
					break;
				case PedHeadOverlayType.ChestHair:
					API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, (int)character.PedHeadOverlays.ChestHair.ColorType, character.PedHeadOverlays.ChestHair.ColorId, character.PedHeadOverlays.ChestHair.SecondColorId);
					break;
				default:
					break;
			}
		}

		public async Task ApplyToPed()
		{
			Logger.Debug($"Head Overlays: Applying...");

			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blemishes.Type, character.PedHeadOverlays.Blemishes.Index, character.PedHeadOverlays.Blemishes.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, character.PedHeadOverlays.FacialHair.Index, character.PedHeadOverlays.FacialHair.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, character.PedHeadOverlays.Eyebrows.Index, character.PedHeadOverlays.Eyebrows.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Ageing.Type, character.PedHeadOverlays.Ageing.Index, character.PedHeadOverlays.Ageing.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, character.PedHeadOverlays.Makeup.Index, character.PedHeadOverlays.Makeup.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, character.PedHeadOverlays.Blush.Index, character.PedHeadOverlays.Blush.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Complexion.Type, character.PedHeadOverlays.Complexion.Index, character.PedHeadOverlays.Complexion.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.SunDamage.Type, character.PedHeadOverlays.SunDamage.Index, character.PedHeadOverlays.SunDamage.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, character.PedHeadOverlays.Lipstick.Index, character.PedHeadOverlays.Lipstick.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.MolesAndFreckles.Type, character.PedHeadOverlays.MolesAndFreckles.Index, character.PedHeadOverlays.MolesAndFreckles.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, character.PedHeadOverlays.ChestHair.Index, character.PedHeadOverlays.ChestHair.Opacity);
			API.SetPedHeadOverlay(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.BodyBlemishes.Type, character.PedHeadOverlays.BodyBlemishes.Index, character.PedHeadOverlays.BodyBlemishes.Opacity);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.FacialHair.Type, (int)character.PedHeadOverlays.FacialHair.ColorType, character.PedHeadOverlays.FacialHair.ColorId, character.PedHeadOverlays.FacialHair.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Eyebrows.Type, (int)character.PedHeadOverlays.Eyebrows.ColorType, character.PedHeadOverlays.Eyebrows.ColorId, character.PedHeadOverlays.Eyebrows.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Makeup.Type, (int)character.PedHeadOverlays.Makeup.ColorType, character.PedHeadOverlays.Makeup.ColorId, character.PedHeadOverlays.Makeup.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Blush.Type, (int)character.PedHeadOverlays.Blush.ColorType, character.PedHeadOverlays.Blush.ColorId, character.PedHeadOverlays.Blush.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.Lipstick.Type, (int)character.PedHeadOverlays.Lipstick.ColorType, character.PedHeadOverlays.Lipstick.ColorId, character.PedHeadOverlays.Lipstick.SecondColorId);
			API.SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)character.PedHeadOverlays.ChestHair.Type, (int)character.PedHeadOverlays.ChestHair.ColorType, character.PedHeadOverlays.ChestHair.ColorId, character.PedHeadOverlays.ChestHair.SecondColorId);

			await WindowManager.Delay(WindowManager.delayMs);
		}
	}
}
