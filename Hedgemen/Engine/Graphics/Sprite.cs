﻿using System;
using Hgm.Engine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hgm.Engine.Graphics
{
	public class Sprite : IDisposable
	{
		private Texture2D texture;

		internal Texture2D Texture => texture;
		
		private ResourceName resourceName;

		public Rectangle Bounds => texture.Bounds;
		
		public ResourceName ResourceName
		{
			get => resourceName;
			set
			{
				Dispose();
				isUnsafe = false;
				resourceName = value;
				texture = Hedgemen.Game.Assets.Load<Texture2D>(resourceName);
			}
		}
		
		private bool isUnsafe;

		public bool IsUnsafe => isUnsafe;

		public Sprite(ResourceName resourceName)
		{
			this.resourceName = resourceName;
			texture = Hedgemen.Game.Assets.Load<Texture2D>(resourceName);
			isUnsafe = false;
		}

		public Sprite(UnsafeTextureInfo info)
		{
			SetTexture(info);
		}

		public void SetTexture(UnsafeTextureInfo info)
		{
			Dispose();
			this.resourceName = ResourceName.Empty;
			texture = info.ToTexture();
			isUnsafe = true;
		}

		public void Dispose()
		{
			if (!isUnsafe) return;
			this.resourceName = ResourceName.Empty;
			texture.Dispose();
			isUnsafe = false;
		}
	}
}