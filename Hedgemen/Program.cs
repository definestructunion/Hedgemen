﻿using System;
using System.Collections.Generic;
using Hgm.API.Areas;
using Hgm.API.Entities;
using Hgm.API.Modding;
using Hgm.Content;
using Hgm.Content.Items;
using Hgm.Engine.Assets;
using Hgm.Engine.Graphics;
using Hgm.Engine.IO;
using Hgm.Engine.Scenes;
using Hgm.Engine.Utilities;
using Maths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Hgm
{
	internal sealed class Program : Game, IGame
	{
		internal static void Main(string[] args)
		{
			using Game game = new Program();
			game.Run();
		}

		public ResourcePack ResourcePack { get; private set; }

		private AssetManager assets;

		public AssetManager Assets => assets;

		private GraphicsDeviceManager graphics;

		public GraphicsDeviceManager Graphics => graphics;

		public Scene currentScene;
		
		public Program()
		{
			graphics = new GraphicsDeviceManager(this);
		}

		private SpriteBatch spriteBatch;
		private Song music;
		private Texture2D texture;
		private Sprite sprite;
		
		private void GraphicsSetBorderless(bool val)
		{
			Window.IsBorderlessEXT = val;
		}
		
		protected override void Initialize()
		{
			IsMouseVisible = true;
			
			Graphics.PreferredBackBufferWidth = 1920;
			Graphics.PreferredBackBufferHeight = 1080;
			Graphics.IsFullScreen = true;
			GraphicsSetBorderless(true);
			Graphics.PreferMultiSampling = false;
			Graphics.ApplyChanges();
			
			spriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
			assets = new AssetManager(this, Graphics);

			Console.WriteLine(new FileHandle("asset_manifest.json").FullName);
			AssetManifest assetManifest = new FileHandle("asset_manifest.json").ReadString().FromJson<AssetManifest>();

			foreach (var loadPass in assetManifest.AsAssetLoadPasses())
			{
				assets.Load<object>(loadPass);
			}

			music = assets.Load<object>("hedgemen:music/main_menu") as Song;
			
			MediaPlayer.Play(music);

			texture = assets.Load<Texture2D>("hedgemen:textures/main_menu_background");

			Color[] colorData = new Color[112 * 63];
			texture.GetData(colorData);

			ResourcePack = new ResourcePack
			{
				Button = new ButtonData(
					new TextureData("hedgemen:ui/button_regular", 5, 5),
					new TextureData("hedgemen:ui/button_hover", 5, 5),
					new TextureData("hedgemen:ui/button_down", 5, 5)),

				FontDefault = new FontData("hedgemen:ui/font_regular", "hedgemen:ui/font_bold", "hedgemen:ui/font_italic"),

				MinimapBackground = new TextureData("hedgemen:ui_minimap_background"),
				
				TileSize = 16
			};
			
			sprite = new Sprite(new UnsafeTextureInfo
			{
				ColorData = colorData,
				Graphics = Graphics,
				Width = 112,
				Height = 63
			});

			var hedgemenArgs = new HedgemenArgs
			{
				ForgeArgs = new HedgemenForgeArgs
				{
					DirectLoadMods = new List<ForgeMod> { new HedgemenMod() },
					ModPackDirectory = new DirectoryHandle("mods")
				},

				Game = this
			};
			
			Hedgemen.HedgemenStart(hedgemenArgs);

			currentScene = new SceneMainMenu();
			currentScene.Initialize();

			var areaArgs = new UAreaArgs
			{
				TypeInfo = HedgemenMod.AreaOverworld
			};
			
			var area = areaArgs.TypeInfo.Create(areaArgs);
			Console.WriteLine(area.Name);

			var entityArgs = new UEntityArgs
			{
				TypeInfo = Hedgemen.Libraries.EntityTypes.Get("hedgemen:entities/human_archer")
			};
			
			area.Generate();

			var entity = entityArgs.TypeInfo.Create(entityArgs);
			Console.WriteLine(entity.Name);
			
			Console.WriteLine(area.AreaMap.GetCellAt(new MapPos(0, 0)));

			var cell = area.AreaMap.GetCellAt(new MapPos(0, 0));
			cell.Sector.GetRegion(0).Item = new RegionItemChest();

			base.Initialize();
		}

		protected override void Draw(GameTime gameTime)
		{
			currentScene.Draw(gameTime);
			base.Draw(gameTime);
		}

		protected override void Update(GameTime gameTime)
		{
			currentScene.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			sprite.Dispose();
			base.OnExiting(sender, args);
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			base.OnDeactivated(sender, args);
		}
	}
}