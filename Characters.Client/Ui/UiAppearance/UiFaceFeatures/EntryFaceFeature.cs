using System.Threading.Tasks;
using Gaston11276.Fivemui;
using Gaston11276.Characters.Shared.Models;

namespace Gaston11276.Characters.Client
{
	public class EntryFaceFeature
	{
		public PedFaceFeatureType type;
		public Textbox uiLabel;
		public Textbox uiValue;
		public Textbox btnDecrease;
		public Textbox btnIncrease;

		public delegate void SetFaceFeaturValue(PedFaceFeatureType type, float value);
		public SetFaceFeaturValue SetValue;

		public delegate float GetFaceFeaturValue(PedFaceFeatureType type);
		public GetFaceFeaturValue GetValue;

		public EntryFaceFeature()
		{
			uiLabel = new Textbox();
			uiValue = new Textbox();
			btnDecrease = new Textbox();
			btnIncrease = new Textbox();
		}

		public async Task SetUi()
		{
			float value = GetValue(type);
			uiValue.SetText($"{string.Format("{0:0.0#}", value)}");
			await WindowManager.Delay(WindowManager.delayMs);
		}

		public void Increase()
		{
			float value = GetValue(type);
			value += 0.1f;

			if (value > 1f)
			{
				value = 1f;
			}

			uiValue.SetText($"{string.Format("{0:0.0#}", value)}");
			SetValue(type, value);
		}

		public void Decrease()
		{
			float value = GetValue(type);
			value -= 0.1f;

			if (value < -1f)
			{
				value = -1f;
			}

			uiValue.SetText($"{string.Format("{0:0.0#}", value)}");
			SetValue(type, value);
		}
	}
}
