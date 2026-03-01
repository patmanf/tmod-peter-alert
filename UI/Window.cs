using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.UI;

namespace PeterAlertExe.UI;

public class Window : UIElement
{
    private static readonly Asset<Texture2D> Texture = ModContent.Request<Texture2D>("PeterAlertExe/UI/window", AssetRequestMode.ImmediateLoad);
    private static readonly SoundStyle OpenSound = new("PeterAlertExe/Sounds/open");

    public override void OnInitialize()
    {
        Width.Set(Texture.Width(), 0f);
        Height.Set(Texture.Height(), 0f);

        var button = new OkButton();
        button.Activate();
        Append(button);
        
        SoundEngine.PlaySound(OpenSound);
    }

    public override void LeftMouseDown(UIMouseEvent evt) => Parent.Append(this);

    protected override void DrawSelf(SpriteBatch spriteBatch)
        => spriteBatch.Draw(Texture.Value, GetDimensions().Position(), Color.White);
    
    private class OkButton : UIElement
    {
        private static readonly Asset<Texture2D> ButtonTexture = ModContent.Request<Texture2D>("PeterAlertExe/UI/button", AssetRequestMode.ImmediateLoad);
        private static readonly SoundStyle ClickSound = new("PeterAlertExe/Sounds/click");
        private bool _pressed;
    
        public override void OnInitialize()
        {
            Left.Set(78f, 0f);
            Top.Set(81f, 0f);
            Width.Set(ButtonTexture.Width(), 0f);
            Height.Set(ButtonTexture.Height(), 0f);
        }
        
        public override void LeftClick(UIMouseEvent evt) => Parent.Remove();
        
        public override void MouseOut(UIMouseEvent evt) => _pressed = false;
        public override void LeftMouseUp(UIMouseEvent evt) => _pressed = false;
        public override void LeftMouseDown(UIMouseEvent evt)
        {
            _pressed = true;
            SoundEngine.PlaySound(ClickSound);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (_pressed) return;
            spriteBatch.Draw(ButtonTexture.Value, GetDimensions().Position(), Color.White);
        }
    }
}
