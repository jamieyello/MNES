﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mnes.Ui.MonoGame;

public sealed class Game1 : Game {
   public string StartupRom;

   GraphicsDeviceManager _graphics;
   SpriteBatch _spriteBatch;

   public Game1() {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
   }

   protected override void LoadContent() =>
      _spriteBatch = new SpriteBatch(GraphicsDevice);

   protected override void Update(GameTime gameTime) {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
      base.Update(gameTime);
   }

   protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);
      base.Draw(gameTime);
   }
}
