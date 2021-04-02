using System;
using System.Collections.Generic;
using System.Linq;
using Hgm.Engine.Scenes.Nodes;

namespace Hgm.Engine.Assets
{
	public enum ButtonState
	{
		Regular,
		Hover,
		Down
	}
    
	public struct ButtonData
	{
		private TextureData[] buttonData;

		public ButtonData(TextureData buttonRegular, TextureData buttonHover, TextureData buttonDown)
		{
			buttonData = new TextureData[Enum.GetValues(typeof(ButtonState)).Length];
			buttonData[0] = buttonRegular;
			buttonData[1] = buttonHover;
			buttonData[2] = buttonDown;
		}
        
		public TextureData GetButton(ButtonState state) => buttonData[(int) state];

		public TextureData GetButton(NodeState state)
		{
			switch (state)
			{
				case NodeState.Regular: return buttonData[(int) ButtonState.Regular];
				case NodeState.MouseHover: return buttonData[(int) ButtonState.Hover];
				case NodeState.MouseDown: return buttonData[(int) ButtonState.Down];
			}
            
			return buttonData[0];
		}

		public List<TextureData> GetButtons()
		{
			return buttonData.ToList();
		}
	}
}