using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowComponents: Window
	{
		UiElementFiveM uiColumnIndexLabels = new UiElementFiveM();
		UiElementFiveM uiColumnIndexValues = new UiElementFiveM();
		UiElementFiveM uiColumnIndexDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnIndexIncrease = new UiElementFiveM();
		UiElementFiveM uiColumnTextureValues = new UiElementFiveM();
		UiElementFiveM uiColumnTextureDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnTextureIncrease = new UiElementFiveM();

		EntryComponent uiFace;
		EntryComponent uiMask;
		EntryComponent uiHair;
		EntryComponent uiTorso;
		EntryComponent uiLegs;
		EntryComponent uiBack;
		EntryComponent uiShoes;
		EntryComponent uiAccessory;
		EntryComponent uiUndershirt;
		EntryComponent uiKevlar;
		EntryComponent uiBadge;
		EntryComponent uiTorso2;

		EntryProp uiHat;
		EntryProp uiGlasses;
		EntryProp uiEar;
		EntryProp uiWatch;
		EntryProp uiBracelet;

		Character character;

		public WindowComponents()
		{
			defaultPadding = 0.0025f;
		}

		protected override async void OnOpen()
		{
			UiCamera.SetMode(CameraMode.Front);
			await SetUi();
			base.OnOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}

		public async Task SetUi()
		{
			if (character.ModelHash == PedHash.FreemodeMale01 || character.ModelHash == PedHash.FreemodeFemale01)
			{
				uiFace.Hide();
				
				uiMask.Show();
				await uiMask.SetUi();
				uiHair.Show();
				await uiHair.SetUi();
				uiTorso.Show();
				await uiTorso.SetUi();
				uiLegs.Show();
				await uiLegs.SetUi();
				uiBack.Show();
				await uiBack.SetUi();
				uiShoes.Show();
				await uiShoes.SetUi();
				uiAccessory.Show();
				await uiAccessory.SetUi();
				uiUndershirt.Show();
				await uiUndershirt.SetUi();
				uiKevlar.Show();
				await uiKevlar.SetUi();
				uiBadge.Show();
				await uiBadge.SetUi();
				uiTorso2.Show();
				await uiTorso2.SetUi();
				uiHat.Show();
				await uiHat.SetUi();
				uiGlasses.Show();
				await uiGlasses.SetUi();
				uiEar.Show();
				await uiEar.SetUi();
				uiWatch.Show();
				await uiWatch.SetUi();
				uiBracelet.Show();
				await uiBracelet.SetUi();
			}
			else
			{
				if (GetComponentIndexMax(PedComponentType.Face) > 1)
				{
					uiFace.Show();
					await uiFace.SetUi();
				}
				else
				{
					uiFace.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Mask) > 1)
				{
					uiMask.Show();
					await uiMask.SetUi();
				}
				else
				{
					uiMask.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Hair) > 1)
				{
					uiHair.Show();
					await uiHair.SetUi();
				}
				else
				{
					uiHair.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Torso) > 1)
				{
					uiTorso.Show();
					await uiTorso.SetUi();
				}
				else
				{
					uiTorso.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Legs) > 1)
				{
					uiLegs.Show();
					await uiLegs.SetUi();

				}
				else
				{
					uiLegs.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Back) > 1)
				{
					uiBack.Show();
					await uiBack.SetUi();
				}
				else
				{
					uiBack.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Shoes) > 1)
				{
					uiShoes.Show();
					await uiShoes.SetUi();
				}
				else
				{
					uiShoes.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Accessory) > 1)
				{
					uiAccessory.Show();
					await uiAccessory.SetUi();
				}
				else
				{
					uiAccessory.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Undershirt) > 1)
				{
					uiUndershirt.Show();
					await uiUndershirt.SetUi();
				}
				else
				{
					uiUndershirt.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Kevlar) > 1)
				{
					uiKevlar.Show();
					await uiKevlar.SetUi();
				}
				else
				{
					uiKevlar.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Badge) > 1)
				{
					uiBadge.Show();
					await uiBadge.SetUi();
				}
				else
				{
					uiBadge.Hide();
				}

				if (GetComponentIndexMax(PedComponentType.Torso2) > 1)
				{
					uiTorso2.Show();
					await uiTorso2.SetUi();
				}
				else
				{
					uiTorso2.Hide();
				}

				if (GetPropIndexMax(PedPropType.Hat) > 1)
				{
					uiHat.Show();
					await uiHat.SetUi();
				}
				else
				{
					uiHat.Hide();
				}

				if (GetPropIndexMax(PedPropType.Glasses) > 1)
				{
					uiGlasses.Show();
					await uiGlasses.SetUi();
				}
				else
				{
					uiGlasses.Hide();
				}

				if (GetPropIndexMax(PedPropType.Ear) > 1)
				{
					uiEar.Show();
					await uiEar.SetUi();
				}
				else
				{
					uiEar.Hide();
				}

				if (GetPropIndexMax(PedPropType.Watch) > 1)
				{
					uiWatch.Show();
					await uiWatch.SetUi();
				}
				else
				{
					uiWatch.Hide();
				}

				if (GetPropIndexMax(PedPropType.Bracelet) > 1)
				{
					uiBracelet.Show();
					await uiBracelet.SetUi();
				}
				else
				{
					uiBracelet.Hide();
				}
			}

			Refresh();
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;
			await WindowManager.Delay(10);
		}

		public async Task SetDefaults()
		{
			Logger.Debug("WindowComponent: SetDefaults()");

			if (character.Gender == (short) Gender.Male)
			{
				character.PedComponents.Torso.Index = 15;
				character.PedComponents.Hair.Index = 4;
				character.PedComponents.Legs.Index = 21;
				character.PedComponents.Shoes.Index = 34;
				character.PedComponents.Undershirt.Index = 15;
				character.PedComponents.Torso2.Index = 15;
				character.PedProps.Hat.Index = 11;
				character.PedProps.Glasses.Index = 6;
			}
			else if (character.Gender == (short) Gender.Female)
			{
				character.PedComponents.Torso.Index = 15;
				character.PedComponents.Hair.Index = 4;
				character.PedComponents.Legs.Index = 15;
				character.PedComponents.Shoes.Index = 35;
				character.PedComponents.Undershirt.Index = 15;
				character.PedComponents.Torso2.Index = 15;
				character.PedProps.Hat.Index = 57;
				character.PedProps.Glasses.Index = 5;
				character.PedProps.Watch.Texture = 5;
			}
			await WindowManager.Delay(WindowManager.delayMs);
		}

		public void ResetPedComponents()
		{
			character.PedComponents.Face.Index = 0;
			character.PedComponents.Face.Texture = 0;
			character.PedComponents.Mask.Index = 0;
			character.PedComponents.Mask.Texture = 0;
			character.PedComponents.Hair.Index = 0;
			character.PedComponents.Hair.Texture = 0;
			character.PedComponents.Torso.Index = 0;
			character.PedComponents.Torso.Texture = 0;
			character.PedComponents.Legs.Index = 0;
			character.PedComponents.Legs.Texture = 0;
			character.PedComponents.Back.Index = 0;
			character.PedComponents.Back.Texture = 0;
			character.PedComponents.Shoes.Index = 0;
			character.PedComponents.Shoes.Texture = 0;
			character.PedComponents.Accessory.Index = 0;
			character.PedComponents.Accessory.Texture = 0;
			character.PedComponents.Undershirt.Index = 0;
			character.PedComponents.Undershirt.Texture = 0;
			character.PedComponents.Kevlar.Index = 0;
			character.PedComponents.Kevlar.Texture = 0;
			character.PedComponents.Badge.Index = 0;
			character.PedComponents.Badge.Texture = 0;
			character.PedComponents.Torso2.Index = 0;
			character.PedComponents.Torso2.Texture = 0;
			character.PedProps.Hat.Index = 0;
			character.PedProps.Hat.Texture = 0;
			character.PedProps.Glasses.Index = 0;
			character.PedProps.Glasses.Texture = 0;
			character.PedProps.Ear.Index = 0;
			character.PedProps.Ear.Texture = 0;
			character.PedProps.Watch.Index = 0;
			character.PedProps.Watch.Texture = 0;
			character.PedProps.Bracelet.Index = 0;
			character.PedProps.Bracelet.Texture = 0;
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
			CreateColumn(panel, HGravity.Left, uiColumnTextureValues, "Texture");
			CreateColumn(panel, HGravity.Center, uiColumnTextureDecrease, "");
			CreateColumn(panel, HGravity.Center, uiColumnTextureIncrease, "");
		}

		private EntryComponent CreateEntryComponent(PedComponentType type, string label)
		{
			float defaultPadding = 0.0025f;

			EntryComponent entry = new EntryComponent();
			entry.type = type;

			entry.uiComponentLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiComponentLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentLabel.SetText(label);
			entry.uiComponentLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexLabels.AddElement(entry.uiComponentLabel);

			entry.uiComponentIndex.SetFont(Font.CharletComprimeColonge);
			entry.uiComponentIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexValues.AddElement(entry.uiComponentIndex);

			entry.btnComponentDecrease.SetText("-");
			entry.btnComponentDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnComponentDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnComponentDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnComponentDecrease);

			entry.btnComponentIncrease.SetText("+");
			entry.btnComponentIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnComponentIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnComponentIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnComponentIncrease);

			entry.uiTextureId.SetFont(Font.CharletComprimeColonge);
			entry.uiTextureId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiTextureId.SetFlags(UiElement.TRANSPARENT);
			uiColumnTextureValues.AddElement(entry.uiTextureId);

			entry.btnTextureDecrease.SetText("-");
			entry.btnTextureDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureDecrease.RegisterOnLMBRelease(entry.DecreaseTexture);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnTextureDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnTextureDecrease.OnMouseButton);
			uiColumnTextureDecrease.AddElement(entry.btnTextureDecrease);

			entry.btnTextureIncrease.SetText("+");
			entry.btnTextureIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureIncrease.RegisterOnLMBRelease(entry.IncreaseTexture);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnTextureIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnTextureIncrease.OnMouseButton);
			uiColumnTextureIncrease.AddElement(entry.btnTextureIncrease);

			entry.GetIndexMax = GetComponentIndexMax;
			entry.GetIndex = GetComponentIndex;
			entry.SetIndex = SetComponentIndex;
			entry.GetTextureMax = GetComponentTextureMax;
			entry.GetTexture = GetComponentTexture;
			entry.SetTexture = SetComponentTexture;

			return entry;
		}

		private EntryProp CreateEntryProp(PedPropType type, string label)
		{
			float defaultPadding = 0.0025f;

			EntryProp entry = new EntryProp();
			entry.type = type;

			entry.uiComponentLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiComponentLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentLabel.SetText(label);
			entry.uiComponentLabel.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexLabels.AddElement(entry.uiComponentLabel);

			entry.uiComponentIndex.SetFont(Font.CharletComprimeColonge);
			entry.uiComponentIndex.SetPadding(new UiRectangle(defaultPadding));
			entry.uiComponentIndex.SetFlags(UiElement.TRANSPARENT);
			uiColumnIndexValues.AddElement(entry.uiComponentIndex);

			entry.btnComponentDecrease.SetText("-");
			entry.btnComponentDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentDecrease.RegisterOnLMBRelease(entry.DecreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnComponentDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnComponentDecrease.OnMouseButton);
			uiColumnIndexDecrease.AddElement(entry.btnComponentDecrease);

			entry.btnComponentIncrease.SetText("+");
			entry.btnComponentIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnComponentIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnComponentIncrease.RegisterOnLMBRelease(entry.IncreaseIndex);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnComponentIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnComponentIncrease.OnMouseButton);
			uiColumnIndexIncrease.AddElement(entry.btnComponentIncrease);

			entry.uiTextureId.SetFont(Font.CharletComprimeColonge);
			entry.uiTextureId.SetPadding(new UiRectangle(defaultPadding));
			entry.uiTextureId.SetFlags(UiElement.TRANSPARENT);
			uiColumnTextureValues.AddElement(entry.uiTextureId);

			entry.btnTextureDecrease.SetText("-");
			entry.btnTextureDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureDecrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureDecrease.RegisterOnLMBRelease(entry.DecreaseTexture);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnTextureDecrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnTextureDecrease.OnMouseButton);
			uiColumnTextureDecrease.AddElement(entry.btnTextureDecrease);

			entry.btnTextureIncrease.SetText("+");
			entry.btnTextureIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnTextureIncrease.SetProperties(UiElement.CANFOCUS);
			entry.btnTextureIncrease.RegisterOnLMBRelease(entry.IncreaseTexture);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnTextureIncrease.OnMouseMove);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnTextureIncrease.OnMouseButton);
			uiColumnTextureIncrease.AddElement(entry.btnTextureIncrease);

			entry.GetIndexMax = GetPropIndexMax;
			entry.GetIndex = GetPropIndex;
			entry.SetIndex = SetPropIndex;
			entry.GetTextureMax = GetPropTextureMax;
			entry.GetTexture = GetPropTexture;
			entry.SetTexture = SetPropTexture;
			entry.DettachProp = DettachProp;

			return entry;
		}

		public override void CreateUi()
		{
			base.CreateUi();

			SetFlags(UiElement.HIDDEN);
			SetPadding(new UiRectangle(defaultPadding));
			SetAlignment(HAlignment.Right);
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetOrientation(Orientation.Vertical);

			Textbox header = new Textbox();
			header.SetText("Components");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			CreateColumns();

			uiFace = CreateEntryComponent(PedComponentType.Face, "Face");
			uiMask = CreateEntryComponent(PedComponentType.Mask, "Mask");
			uiHair = CreateEntryComponent(PedComponentType.Hair, "Hair");
			uiTorso = CreateEntryComponent(PedComponentType.Torso, "Torso");
			uiLegs = CreateEntryComponent(PedComponentType.Legs, "Legs");
			uiBack = CreateEntryComponent(PedComponentType.Back, "Back");
			uiShoes = CreateEntryComponent(PedComponentType.Shoes, "Shoes");
			uiAccessory = CreateEntryComponent(PedComponentType.Accessory, "Accessory");
			uiUndershirt = CreateEntryComponent(PedComponentType.Undershirt, "Undershirt");
			uiKevlar = CreateEntryComponent(PedComponentType.Kevlar, "Kevlar");
			uiBadge = CreateEntryComponent(PedComponentType.Badge, "Badge");
			uiTorso2 = CreateEntryComponent(PedComponentType.Torso2, "Torso2");

			uiHat = CreateEntryProp(PedPropType.Hat, "Hat");
			uiGlasses = CreateEntryProp(PedPropType.Glasses, "Glasses");
			uiEar = CreateEntryProp(PedPropType.Ear, "Ear");
			uiWatch = CreateEntryProp(PedPropType.Watch, "Watch");
			uiBracelet = CreateEntryProp(PedPropType.Bracelet, "Bracelet");
		}

		public int GetComponentIndex(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					return character.PedComponents.Face.Index;
				case PedComponentType.Mask:
					return character.PedComponents.Mask.Index;
				case PedComponentType.Hair:
					return character.PedComponents.Hair.Index;
				case PedComponentType.Torso:
					return character.PedComponents.Torso.Index;
				case PedComponentType.Legs:
					return character.PedComponents.Legs.Index;
				case PedComponentType.Back:
					return character.PedComponents.Back.Index;
				case PedComponentType.Shoes:
					return character.PedComponents.Shoes.Index;
				case PedComponentType.Accessory:
					return character.PedComponents.Accessory.Index;
				case PedComponentType.Undershirt:
					return character.PedComponents.Undershirt.Index;
				case PedComponentType.Kevlar:
					return character.PedComponents.Kevlar.Index;
				case PedComponentType.Badge:
					return character.PedComponents.Badge.Index;
				case PedComponentType.Torso2:
					return character.PedComponents.Torso2.Index;
				default:
					return 0;
			}
		}

		public int GetComponentIndexMax(PedComponentType type)
		{
			return API.GetNumberOfPedDrawableVariations(Game.PlayerPed.Handle, (int)type);
		}

		public void SetComponentIndex(PedComponentType type, int index)
		{
			switch (type)
			{
				case PedComponentType.Face:
					character.PedComponents.Face.Index = index;
					break;
				case PedComponentType.Mask:
					character.PedComponents.Mask.Index = index;
					break;
				case PedComponentType.Hair:
					character.PedComponents.Hair.Index = index;
					break;
				case PedComponentType.Torso:
					character.PedComponents.Torso.Index = index;
					break;
				case PedComponentType.Legs:
					character.PedComponents.Legs.Index = index;
					break;
				case PedComponentType.Back:
					character.PedComponents.Back.Index = index;
					break;
				case PedComponentType.Shoes:
					character.PedComponents.Shoes.Index = index;
					break;
				case PedComponentType.Accessory:
					character.PedComponents.Accessory.Index = index;
					break;
				case PedComponentType.Undershirt:
					character.PedComponents.Undershirt.Index = index;
					break;
				case PedComponentType.Kevlar:
					character.PedComponents.Kevlar.Index = index;
					break;
				case PedComponentType.Badge:
					character.PedComponents.Badge.Index = index;
					break;
				case PedComponentType.Torso2:
					character.PedComponents.Torso2.Index = index;
					break;
				default:
					break;
			}
			ApplyComponentToPed(type);
		}

		public int GetComponentTexture(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					return character.PedComponents.Face.Texture;
				case PedComponentType.Mask:
					return character.PedComponents.Mask.Texture;
				case PedComponentType.Hair:
					return character.PedComponents.Hair.Texture;
				case PedComponentType.Torso:
					return character.PedComponents.Torso.Texture;
				case PedComponentType.Legs:
					return character.PedComponents.Legs.Texture;
				case PedComponentType.Back:
					return character.PedComponents.Back.Texture;
				case PedComponentType.Shoes:
					return character.PedComponents.Shoes.Texture;
				case PedComponentType.Accessory:
					return character.PedComponents.Accessory.Texture;
				case PedComponentType.Undershirt:
					return character.PedComponents.Undershirt.Texture;
				case PedComponentType.Kevlar:
					return character.PedComponents.Kevlar.Texture;
				case PedComponentType.Badge:
					return character.PedComponents.Badge.Texture;
				case PedComponentType.Torso2:
					return character.PedComponents.Torso2.Texture;
				default:
					return 0;
			}
		}

		public int GetComponentTextureMax(PedComponentType type, int drawableId)
		{
			return API.GetNumberOfPedTextureVariations(Game.PlayerPed.Handle, (int)type, drawableId);
		}

		public void SetComponentTexture(PedComponentType type, int texture)
		{
			switch (type)
			{
				case PedComponentType.Face:
					character.PedComponents.Face.Texture = texture;
					break;
				case PedComponentType.Mask:
					character.PedComponents.Mask.Texture = texture;
					break;
				case PedComponentType.Hair:
					character.PedComponents.Hair.Texture = texture;
					break;
				case PedComponentType.Torso:
					character.PedComponents.Torso.Texture = texture;
					break;
				case PedComponentType.Legs:
					character.PedComponents.Legs.Texture = texture;
					break;
				case PedComponentType.Back:
					character.PedComponents.Back.Texture = texture;
					break;
				case PedComponentType.Shoes:
					character.PedComponents.Shoes.Texture = texture;
					break;
				case PedComponentType.Accessory:
					character.PedComponents.Accessory.Texture = texture;
					break;
				case PedComponentType.Undershirt:
					character.PedComponents.Undershirt.Texture = texture;
					break;
				case PedComponentType.Kevlar:
					character.PedComponents.Kevlar.Texture = texture;
					break;
				case PedComponentType.Badge:
					character.PedComponents.Badge.Texture = texture;
					break;
				case PedComponentType.Torso2:
					character.PedComponents.Torso2.Texture = texture;
					break;
				default:
					break;
			}
			ApplyComponentToPed(type);
		}

		public int GetComponentColorMax(PedComponentType type, int index)
		{
			return API.GetNumHairColors();
		}
		public int GetComponentColor1(PedComponentType type)
		{
			return character.PedHairColor;
		}
		public void SetComponentColor1(PedComponentType type, int colorId)
		{
			character.PedHairColor = colorId;
		}
		public int GetComponentColor2(PedComponentType type)
		{
			return character.PedSecondHairColor;
		}
		public void SetComponentColor2(PedComponentType type, int colorId)
		{
			character.PedSecondHairColor = colorId;
		}

		public int GetPropIndex(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					return character.PedProps.Hat.Index;
				case PedPropType.Glasses:
					return character.PedProps.Glasses.Index;
				case PedPropType.Ear:
					return character.PedProps.Ear.Index;
				case PedPropType.Watch:
					return character.PedProps.Watch.Index;
				case PedPropType.Bracelet:
					return character.PedProps.Bracelet.Index;
				default:
					return 0;
			}
		}

		public int GetPropIndexMax(PedPropType type)
		{
			return API.GetNumberOfPedPropDrawableVariations(Game.PlayerPed.Handle, (int)type);
		}

		public void SetPropIndex(PedPropType type, int index)
		{
			switch (type)
			{
				case PedPropType.Hat:
					character.PedProps.Hat.Index = index;
					break;
				case PedPropType.Glasses:
					character.PedProps.Glasses.Index = index;
					break;
				case PedPropType.Ear:
					character.PedProps.Ear.Index = index;
					break;
				case PedPropType.Watch:
					character.PedProps.Watch.Index = index;
					break;
				case PedPropType.Bracelet:
					character.PedProps.Bracelet.Index = index;
					break;
				default:
					break;
			}
			ApplyPropToPed(type);
		}

		public int GetPropTexture(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					return character.PedProps.Hat.Texture;
				case PedPropType.Glasses:
					return character.PedProps.Glasses.Texture;
				case PedPropType.Ear:
					return character.PedProps.Ear.Texture;
				case PedPropType.Watch:
					return character.PedProps.Watch.Texture;
				case PedPropType.Bracelet:
					return character.PedProps.Bracelet.Texture;
				default:
					return 0;
			}
		}

		public int GetPropTextureMax(PedPropType type, int drawableId)
		{
			return API.GetNumberOfPedPropTextureVariations(Game.PlayerPed.Handle, (int)type, drawableId);
		}

		public void SetPropTexture(PedPropType type, int texture)
		{
			switch (type)
			{
				case PedPropType.Hat:
					character.PedProps.Hat.Texture = texture;
					break;
				case PedPropType.Glasses:
					character.PedProps.Glasses.Texture = texture;
					break;
				case PedPropType.Ear:
					character.PedProps.Ear.Texture = texture;
					break;
				case PedPropType.Watch:
					character.PedProps.Watch.Texture = texture;
					break;
				case PedPropType.Bracelet:
					character.PedProps.Bracelet.Texture = texture;
					break;

				default:
					break;
			}
			ApplyPropToPed(type);
		}

		

		public void ApplyComponentToPed(PedComponentType type)
		{
			switch (type)
			{
				case PedComponentType.Face:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Face.Index, character.PedComponents.Face.Texture, 0);
					break;
				case PedComponentType.Mask:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Mask.Index, character.PedComponents.Mask.Texture, 0);
					break;
				case PedComponentType.Hair:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
					break;
				case PedComponentType.Torso:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Torso.Index, character.PedComponents.Torso.Texture, 0);
					break;
				case PedComponentType.Legs:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Legs.Index, character.PedComponents.Legs.Texture, 0);
					break;
				case PedComponentType.Back:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Back.Index, character.PedComponents.Back.Texture, 0);
					break;
				case PedComponentType.Shoes:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Shoes.Index, character.PedComponents.Shoes.Texture, 0);
					break;
				case PedComponentType.Accessory:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Accessory.Index, character.PedComponents.Accessory.Texture, 0);
					break;
				case PedComponentType.Undershirt:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Undershirt.Index, character.PedComponents.Undershirt.Texture, 0);
					break;
				case PedComponentType.Kevlar:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Kevlar.Index, character.PedComponents.Kevlar.Texture, 0);
					break;
				case PedComponentType.Badge:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Badge.Index, character.PedComponents.Badge.Texture, 0);
					break;
				case PedComponentType.Torso2:
					API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)type, character.PedComponents.Torso2.Index, character.PedComponents.Torso2.Texture, 0);
					break;
				default:
					break;
			}
		}

		public void ApplyPropToPed(PedPropType type)
		{
			switch (type)
			{
				case PedPropType.Hat:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Hat.Index, character.PedProps.Hat.Texture, true);
					break;
				case PedPropType.Glasses:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Glasses.Index, character.PedProps.Glasses.Texture, true);
					break;
				case PedPropType.Ear:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Ear.Index, character.PedProps.Ear.Texture, true);
					break;
				case PedPropType.Watch:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Watch.Index, character.PedProps.Watch.Texture, true);
					break;
				case PedPropType.Bracelet:
					API.SetPedPropIndex(Game.PlayerPed.Handle, (int)type, character.PedProps.Bracelet.Index, character.PedProps.Bracelet.Texture, true);
					break;
				default:
					break;
			}
		}

		public void DettachProp(PedPropType type)
		{
			// Todo
		}

		public async Task ApplyToPed()
		{
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Face, character.PedComponents.Face.Index, character.PedComponents.Face.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Mask, character.PedComponents.Mask.Index, character.PedComponents.Mask.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Hair, character.PedComponents.Hair.Index, character.PedComponents.Hair.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Torso, character.PedComponents.Torso.Index, character.PedComponents.Torso.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Legs, character.PedComponents.Legs.Index, character.PedComponents.Legs.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Back, character.PedComponents.Back.Index, character.PedComponents.Back.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Shoes, character.PedComponents.Shoes.Index, character.PedComponents.Shoes.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Accessory, character.PedComponents.Accessory.Index, character.PedComponents.Accessory.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Undershirt, character.PedComponents.Undershirt.Index, character.PedComponents.Undershirt.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Kevlar, character.PedComponents.Kevlar.Index, character.PedComponents.Kevlar.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Badge, character.PedComponents.Badge.Index, character.PedComponents.Badge.Texture, 0);
			API.SetPedComponentVariation(Game.PlayerPed.Handle, (int)PedComponentType.Torso2, character.PedComponents.Torso2.Index, character.PedComponents.Torso2.Texture, 0);

			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Hat, character.PedProps.Hat.Index, character.PedProps.Hat.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Glasses, character.PedProps.Glasses.Index, character.PedProps.Glasses.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Ear, character.PedProps.Ear.Index, character.PedProps.Ear.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Watch, character.PedProps.Watch.Index, character.PedProps.Watch.Texture, true);
			API.SetPedPropIndex(Game.PlayerPed.Handle, (int)PedPropType.Bracelet, character.PedProps.Bracelet.Index, character.PedProps.Bracelet.Texture, true);

			await WindowManager.Delay(WindowManager.delayMs);
		}
	}
}
