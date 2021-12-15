using System.Threading.Tasks;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class EntryHeadOverlay
	{
		public PedHeadOverlayType type;
		public Textbox uiOverlayLabel = new Textbox();
		public Textbox uiOverlayIndex = new Textbox();
		public Textbox btnIndexDecrease = new Textbox();
		public Textbox btnIndexIncrease = new Textbox();

		public Textbox uiColorId = new Textbox();
		public Textbox btnColorIdDecrease = new Textbox();
		public Textbox btnColorIdIncrease = new Textbox();

		public Textbox uiOpacity = new Textbox();
		public Textbox btnOpacityDecrease = new Textbox();
		public Textbox btnOpacityIncrease = new Textbox();

		public delegate void SetHeadOverlayIndex(PedHeadOverlayType type, int index);
		public SetHeadOverlayIndex SetIndex;
		public delegate int GetHeadOverlayIndex(PedHeadOverlayType type);
		public GetHeadOverlayIndex GetIndex;
		public GetHeadOverlayIndex GetIndexMax;

		public delegate void SetHeadOverlayColor(PedHeadOverlayType type, int colorId);
		public SetHeadOverlayColor SetColor;
		public delegate int GetHeadOverlayColor(PedHeadOverlayType type);
		public GetHeadOverlayColor GetColor;
		public GetHeadOverlayColor GetColorMax;

		public delegate void SetHeadOverlayOpacity(PedHeadOverlayType type, float opacity);
		public SetHeadOverlayOpacity SetOpacity;
		public delegate float GetHeadOverlayOpacity(PedHeadOverlayType type);
		public GetHeadOverlayOpacity GetOpacity;

		public EntryHeadOverlay()
		{ }

		public async Task SetUi()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			uiOverlayIndex.SetText($"{index}/{indexMax}");

			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			uiColorId.SetText($"{colorId}/{colorMax}");

			float opacity = GetOpacity(type);
			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");

			await WindowManager.Delay(WindowManager.delayMs);
		}

		public void IncreaseIndex()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index++;

			if (index > indexMax)
			{
				index = indexMax;
			}

			uiOverlayIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);
		}

		public void DecreaseIndex()
		{
			int index = GetIndex(type);
			int indexMax = GetIndexMax(type);
			index--;

			if (index < 0)
			{
				index = 0;
			}

			uiOverlayIndex.SetText($"{index}/{indexMax}");
			SetIndex(type, index);
		}

		public void IncreaseColor()
		{
			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			colorId++;

			if (colorId > colorMax)
			{
				colorId = colorMax;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(type, colorId);
		}

		public void DecreaseColor()
		{
			int colorId = GetColor(type);
			int colorMax = GetColorMax(type);
			colorId--;

			if (colorId < 0)
			{
				colorId = 0;
			}

			uiColorId.SetText($"{colorId}/{colorMax}");
			SetColor(type, colorId);
		}

		public void IncreaseOpacity()
		{
			float opacity = GetOpacity(type);
			opacity += 0.1f;

			if (opacity > 1f)
			{
				opacity = 1f;
			}

			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");
			SetOpacity(type, opacity);
		}

		public void DecreaseOpacity()
		{
			float opacity = GetOpacity(type);
			opacity -= 0.1f;

			if (opacity < 0f)
			{
				opacity = 0f;
			}

			uiOpacity.SetText($"{string.Format("{0:0.0#}", opacity)}");
			SetOpacity(type, opacity);
		}
	}
}
