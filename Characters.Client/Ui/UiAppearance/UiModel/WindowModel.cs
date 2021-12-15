using System.Threading.Tasks;
using CitizenFX.Core;
using Gaston11276.SimpleUi;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Client.Models;
using Gaston11276.Modelmenu.Client;

namespace Gaston11276.Characters.Client
{
	class ModelData
	{
		public PedHash pedHash;
		public ModelData(PedHash pedHash)
		{
			this.pedHash = pedHash;
		}
	}

	public class WindowModel: Window
	{
		Character character;
		PanelModel panelModel = new PanelModel();

		public WindowModel()
		{
			defaultPadding = 0.0025f;			
		}

		protected override void OnOpen()
		{
			UiCamera.SetMode(CameraMode.Front);
			base.OnOpen();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}

		public async Task SetUi()
		{
			await panelModel.SetUi();
		}

		public async Task SetCharacter(Character character)
		{
			this.character = character;
			panelModel.SetCurrent(panelModel.FindModelIndex(character.ModelHash));
			await WindowManager.Delay(10);
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
			SetAlignment(HAlignment.Right);
			SetProperties(FLOATING | MOVABLE | COLLISION_PARENT);
			SetOrientation(Orientation.Vertical);

			Textbox header = new Textbox();
			header.SetText("Model");
			header.SetFont(Font.Pricedown);
			header.SetFontSize(0.4f);
			header.SetFlags(TRANSPARENT);
			header.SetPadding(new UiRectangle(defaultPadding));
			AddElement(header);

			panelModel.CreateUi();
			AddElement(panelModel);
		}

		public void RegisterOnModelChange(fpPedHash OnModelChange)
		{
			panelModel.RegisterOnModelChangeCallback(OnModelChange);
		}

		public async Task ApplyToPed()
		{
			await panelModel.ApplyModel();
		}

		public async Task SetDefaults()
		{
			if (character.Gender == (short)Gender.Male)
			{
				character.Model = ((uint)PedHash.FreemodeMale01).ToString();
			}
			else if (character.Gender == (short)Gender.Female)
			{
				character.Model = ((uint)PedHash.FreemodeFemale01).ToString();
			}

			await WindowManager.Delay(10);
		}
	}
}
