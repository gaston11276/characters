using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class WindowFaceFeatures : Window
	{
		UiElementFiveM uiColumnLabels = new UiElementFiveM();
		UiElementFiveM uiColumnIndex = new UiElementFiveM();
		UiElementFiveM uiColumnDecrease = new UiElementFiveM();
		UiElementFiveM uiColumnIncrease = new UiElementFiveM();

		EntryFaceFeature NoseWidth;
		EntryFaceFeature NosePeakLength;
		EntryFaceFeature NosePeakHeight;
		EntryFaceFeature NoseBoneHeight;
		EntryFaceFeature NosePeakLowering;
		EntryFaceFeature NoseBoneTwist;
		EntryFaceFeature EyeBrowHeight;
		EntryFaceFeature EyeBrowForward;
		EntryFaceFeature EyesOpening;
		EntryFaceFeature LipThickness;
		EntryFaceFeature CheekWidth;
		EntryFaceFeature CheekBoneWidth;
		EntryFaceFeature CheekBoneHeight;
		EntryFaceFeature ChinBoneWidth;
		EntryFaceFeature ChinBoneLength;
		EntryFaceFeature ChinBoneLowering;
		EntryFaceFeature ChinDimple;
		EntryFaceFeature JawBoneLength;
		EntryFaceFeature JawBoneWidth;
		EntryFaceFeature NeckThickness;

		Character character;
		
		public WindowFaceFeatures()
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
			await NoseWidth.SetUi();
			await NosePeakLength.SetUi();
			await NosePeakHeight.SetUi();
			await NoseBoneHeight.SetUi();
			await NosePeakLowering.SetUi();
			await NoseBoneTwist.SetUi();
			await EyeBrowHeight.SetUi();
			await EyeBrowForward.SetUi();
			await EyesOpening.SetUi();
			await LipThickness.SetUi();
			await CheekWidth.SetUi();
			await CheekBoneWidth.SetUi();
			await CheekBoneHeight.SetUi();
			await ChinBoneWidth.SetUi();
			await ChinBoneLength.SetUi();
			await ChinBoneLowering.SetUi();
			await ChinDimple.SetUi();
			await JawBoneLength.SetUi();
			await JawBoneWidth.SetUi();
			await NeckThickness.SetUi();
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;
			await WindowManager.Delay(10);
		}

		public async Task SetDefaults()
		{
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
			CreateColumn(panel, HGravity.Right, uiColumnIndex);
			CreateColumn(panel, HGravity.Center, uiColumnDecrease);
			CreateColumn(panel, HGravity.Center, uiColumnIncrease);
		}

		private EntryFaceFeature CreateEntry(PedFaceFeatureType type, string label)
		{
			EntryFaceFeature entry = new EntryFaceFeature();

			entry.type = type;

			entry.uiLabel.SetPadding(new UiRectangle(defaultPadding));
			entry.uiLabel.SetFont(Font.CharletComprimeColonge);
			entry.uiLabel.SetText(label);
			entry.uiLabel.SetFlags(TRANSPARENT);
			uiColumnLabels.AddElement(entry.uiLabel);

			entry.uiValue.SetFont(Font.CharletComprimeColonge);
			entry.uiValue.SetPadding(new UiRectangle(defaultPadding));
			entry.uiValue.SetFlags(TRANSPARENT);
			uiColumnIndex.AddElement(entry.uiValue);

			entry.btnDecrease.SetText("-");
			entry.btnDecrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnDecrease.SetProperties(CANFOCUS);
			entry.btnDecrease.RegisterOnLMBRelease(entry.Decrease);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnDecrease.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnDecrease.OnMouseMove);
			uiColumnDecrease.AddElement(entry.btnDecrease);

			entry.btnIncrease.SetText("+");
			entry.btnIncrease.SetPadding(new UiRectangle(defaultPadding));
			entry.btnIncrease.SetProperties(CANFOCUS);
			entry.btnIncrease.RegisterOnLMBRelease(entry.Increase);
			WindowManager.RegisterOnMouseButtonCallback(entry.btnIncrease.OnMouseButton);
			WindowManager.RegisterOnMouseMoveCallback(entry.btnIncrease.OnMouseMove);
			uiColumnIncrease.AddElement(entry.btnIncrease);

			entry.SetValue = SetFaceFeatureValue;
			entry.GetValue = GetFaceFeatureValue;

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
			header.SetText("Face Features");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			CreateColumns();

			NoseWidth = CreateEntry(PedFaceFeatureType.NoseWidth, "NoseWidth");
			NosePeakLength = CreateEntry(PedFaceFeatureType.NosePeakLength, "NosePeakLength");
			NosePeakHeight = CreateEntry(PedFaceFeatureType.NosePeakHeight, "NosePeakHeight");
			NoseBoneHeight = CreateEntry(PedFaceFeatureType.NoseBoneHeight, "NoseBoneHeight");
			NosePeakLowering = CreateEntry(PedFaceFeatureType.NosePeakLowering, "NosePeakLowering");
			NoseBoneTwist = CreateEntry(PedFaceFeatureType.NoseBoneTwist, "NoseBoneTwist");
			EyeBrowHeight = CreateEntry(PedFaceFeatureType.EyeBrowHeight, "EyeBrowHeight");
			EyeBrowForward = CreateEntry(PedFaceFeatureType.EyeBrowForward, "EyeBrowForward");
			EyesOpening = CreateEntry(PedFaceFeatureType.EyesOpening, "EyesOpening");
			LipThickness = CreateEntry(PedFaceFeatureType.LipThickness, "LipThickness");
			CheekWidth = CreateEntry(PedFaceFeatureType.CheekWidth, "CheekWidth");
			CheekBoneWidth = CreateEntry(PedFaceFeatureType.CheekBoneWidth, "CheekBoneWidth");
			CheekBoneHeight = CreateEntry(PedFaceFeatureType.CheekBoneHeight, "CheekBoneHeight");
			ChinBoneWidth = CreateEntry(PedFaceFeatureType.ChinBoneWidth, "ChinBoneWidth");
			ChinBoneLength = CreateEntry(PedFaceFeatureType.ChinBoneLength, "ChinBoneLength");
			ChinBoneLowering = CreateEntry(PedFaceFeatureType.ChinBoneLowering, "ChinBoneLowering");
			ChinDimple = CreateEntry(PedFaceFeatureType.ChinDimple, "ChinDimple");
			JawBoneLength = CreateEntry(PedFaceFeatureType.JawBoneLength, "JawBoneLength");
			JawBoneWidth = CreateEntry(PedFaceFeatureType.JawBoneWidth, "JawBoneWidth");
			NeckThickness = CreateEntry(PedFaceFeatureType.NeckThickness, "NeckThickness");
		}

		public float GetFaceFeatureValue(PedFaceFeatureType type)
		{
			switch (type)
			{
				case PedFaceFeatureType.NoseWidth:
					return character.PedFaceFeatures.NoseWidth;
				case PedFaceFeatureType.NosePeakHeight:
					return character.PedFaceFeatures.NosePeakHeight;
				case PedFaceFeatureType.NosePeakLength:
					return character.PedFaceFeatures.NosePeakLength;
				case PedFaceFeatureType.NoseBoneHeight:
					return character.PedFaceFeatures.NoseBoneHeight;
				case PedFaceFeatureType.NosePeakLowering:
					return character.PedFaceFeatures.NosePeakLowering;
				case PedFaceFeatureType.NoseBoneTwist:
					return character.PedFaceFeatures.NoseBoneTwist;
				case PedFaceFeatureType.EyeBrowHeight:
					return character.PedFaceFeatures.EyeBrowHeight;
				case PedFaceFeatureType.EyeBrowForward:
					return character.PedFaceFeatures.EyeBrowForward;
				case PedFaceFeatureType.CheekBoneHeight:
					return character.PedFaceFeatures.CheekBoneHeight;
				case PedFaceFeatureType.CheekBoneWidth:
					return character.PedFaceFeatures.CheekBoneWidth;
				case PedFaceFeatureType.CheekWidth:
					return character.PedFaceFeatures.CheekWidth;
				case PedFaceFeatureType.EyesOpening:
					return character.PedFaceFeatures.EyesOpening;
				case PedFaceFeatureType.LipThickness:
					return character.PedFaceFeatures.LipThickness;
				case PedFaceFeatureType.JawBoneWidth:
					return character.PedFaceFeatures.JawBoneWidth;
				case PedFaceFeatureType.JawBoneLength:
					return character.PedFaceFeatures.JawBoneLength;
				case PedFaceFeatureType.ChinBoneLowering:
					return character.PedFaceFeatures.ChinBoneLowering;
				case PedFaceFeatureType.ChinBoneLength:
					return character.PedFaceFeatures.ChinBoneLength;
				case PedFaceFeatureType.ChinBoneWidth:
					return character.PedFaceFeatures.ChinBoneWidth;
				case PedFaceFeatureType.ChinDimple:
					return character.PedFaceFeatures.ChinDimple;
				case PedFaceFeatureType.NeckThickness:
					return character.PedFaceFeatures.NeckThickness;
				default:
					return 0f;
			}
		}

		public void SetFaceFeatureValue(PedFaceFeatureType type, float value)
		{
			switch (type)
			{
				case PedFaceFeatureType.NoseWidth:
					character.PedFaceFeatures.NoseWidth = value;
					break;
				case PedFaceFeatureType.NosePeakHeight:
					character.PedFaceFeatures.NosePeakHeight = value;
					break;
				case PedFaceFeatureType.NosePeakLength:
					character.PedFaceFeatures.NosePeakLength = value;
					break;
				case PedFaceFeatureType.NoseBoneHeight:
					character.PedFaceFeatures.NoseBoneHeight = value;
					break;
				case PedFaceFeatureType.NosePeakLowering:
					character.PedFaceFeatures.NosePeakLowering = value;
					break;
				case PedFaceFeatureType.NoseBoneTwist:
					character.PedFaceFeatures.NoseBoneTwist = value;
					break;
				case PedFaceFeatureType.EyeBrowHeight:
					character.PedFaceFeatures.EyeBrowHeight = value;
					break;
				case PedFaceFeatureType.EyeBrowForward:
					character.PedFaceFeatures.EyeBrowForward = value;
					break;
				case PedFaceFeatureType.CheekBoneHeight:
					character.PedFaceFeatures.CheekBoneHeight = value;
					break;
				case PedFaceFeatureType.CheekBoneWidth:
					character.PedFaceFeatures.CheekBoneWidth = value;
					break;
				case PedFaceFeatureType.CheekWidth:
					character.PedFaceFeatures.CheekWidth = value;
					break;
				case PedFaceFeatureType.EyesOpening:
					character.PedFaceFeatures.EyesOpening = value;
					break;
				case PedFaceFeatureType.LipThickness:
					character.PedFaceFeatures.LipThickness = value;
					break;
				case PedFaceFeatureType.JawBoneWidth:
					character.PedFaceFeatures.JawBoneWidth = value;
					break;
				case PedFaceFeatureType.JawBoneLength:
					character.PedFaceFeatures.JawBoneLength = value;
					break;
				case PedFaceFeatureType.ChinBoneLowering:
					character.PedFaceFeatures.ChinBoneLowering = value;
					break;
				case PedFaceFeatureType.ChinBoneLength:
					character.PedFaceFeatures.ChinBoneLength = value;
					break;
				case PedFaceFeatureType.ChinBoneWidth:
					character.PedFaceFeatures.ChinBoneWidth = value;
					break;
				case PedFaceFeatureType.ChinDimple:
					character.PedFaceFeatures.ChinDimple = value;
					break;
				case PedFaceFeatureType.NeckThickness:
					character.PedFaceFeatures.NeckThickness = value;
					break;
				default:
					break;
			}
			ApplyToPed(type, value);
		}

		public void ApplyToPed(PedFaceFeatureType type, float value)
		{
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)type, value);
		}

		public async Task ApplyToPed()
		{
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseWidth, character.PedFaceFeatures.NoseWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakHeight, character.PedFaceFeatures.NosePeakHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakLength, character.PedFaceFeatures.NosePeakLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseBoneHeight, character.PedFaceFeatures.NoseBoneHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NosePeakLowering, character.PedFaceFeatures.NosePeakLowering);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NoseBoneTwist, character.PedFaceFeatures.NoseBoneTwist);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyeBrowHeight, character.PedFaceFeatures.EyeBrowHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyeBrowForward, character.PedFaceFeatures.EyeBrowForward);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekBoneHeight, character.PedFaceFeatures.CheekBoneHeight);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekBoneWidth, character.PedFaceFeatures.CheekBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.CheekWidth, character.PedFaceFeatures.CheekWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.EyesOpening, character.PedFaceFeatures.EyesOpening);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.LipThickness, character.PedFaceFeatures.JawBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.JawBoneWidth, character.PedFaceFeatures.JawBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.JawBoneLength, character.PedFaceFeatures.JawBoneLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneLowering, character.PedFaceFeatures.ChinBoneLowering);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneLength, character.PedFaceFeatures.ChinBoneLength);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinBoneWidth, character.PedFaceFeatures.ChinBoneWidth);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.ChinDimple, character.PedFaceFeatures.ChinDimple);
			API.SetPedFaceFeature(Game.PlayerPed.Handle, (int)PedFaceFeatureType.NeckThickness, character.PedFaceFeatures.NeckThickness);
			await WindowManager.Delay(WindowManager.delayMs);
		}
	}
}
