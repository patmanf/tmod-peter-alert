using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace PeterAlertExe.UI;

public class PeterSystem : ModSystem
{
    private UIState _state;
    private UserInterface _interface;

    private void ResetTimer() => _timer = Main.rand.Next(120, 240);
    private float _timer;
    private static int _stack = Main.rand.Next(8);
    
    public override void Load()
    {
        _state = new UIState();
        _state.Activate();
        _interface = new UserInterface();
        _interface.SetState(_state);
        ResetTimer();
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Cursor"));
        layers.Insert(index, new LegacyGameInterfaceLayer(
            "PeterAlert.exe",
            delegate
            {
                _interface.Draw(Main.spriteBatch, new GameTime());
                return true;
            },
            InterfaceScaleType.UI
        ));
    }

    public override void UpdateUI(GameTime gameTime)
    {
        _interface.Update(gameTime);
        
        _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_timer > 0) return;
        
        ResetTimer();
        if (Main.rand.NextBool(3)) AddWindow();
    }
    
    public void AddWindow()
    {
        var window = new Window();
        
        _stack = (_stack + 1) % 8;
        window.Left.Set(64 + (20 * _stack), 0f);
        window.Top.Set(96 + (20 * _stack), 0f);
        
        window.Activate();
        _state.Append(window);
    }
}
