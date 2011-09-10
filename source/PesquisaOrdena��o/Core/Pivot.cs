using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PO
{
    class Pivot
    {
        Rectangle me;

        public Pivot(int value)
        {
            int positionY = (int)(((Options.maxValue - value) / (float)Options.maxValue) * Options.ScreenHeight * Options.safeBarHeight);
            positionY += Options.barPositionY;
            me = new Rectangle(
                0,
                positionY,
                Options.ScreenWidth,
                Options.barBorderWidth);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, me, Color.Red);
        }
    }
}
