using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pexeso
{
    class Card : IPaint
    {
        private int x;
        public int X { get { return this.x; } set { this.x = value; } }
        private int y;
        public int Y { get { return this.y; } set { this.y = value; } }
        private int uid;
        public int Uid { get { return this.uid; } }
        private int twinId;
        public int TwinId { get { return this.twinId; } }
        
        private Bitmap bitmap;
        private Bitmap bitmapHidden;

        private bool turned;
        private bool won;
        public bool Won { get { return this.won; } }

        public Card(int x, int y, int uid, int twinId, Bitmap bitmap, Bitmap bitmapHidden)
        {
            this.x = x;
            this.y = y;
            this.uid = uid;
            this.twinId = twinId;

            this.turned = false;
            this.won = false;

            this.bitmap = bitmap;
            this.bitmapHidden = bitmapHidden;
        }

        public void Win()
        {
            this.won = true;
        }

        public void TurnUp()
        {
            this.turned = true;
        }

        public void TurnDown()
        {
            this.turned = false;
        }

        public bool CompareTo(Card card)
        {
            if (card.TwinId == this.TwinId)
                return true;
            else
                return false;
        }

        public void Paint(Graphics g)
        {
            if (turned || won)
                g.DrawImage(bitmap, x * Grid.colWidth + Grid.xBegin + (int)((Grid.colWidth - bitmap.Width) * 0.5),
                    y * Grid.colHeight + Grid.yBegin + (int)((Grid.colHeight - bitmap.Height) * 0.5));
            else
                g.DrawImage(bitmapHidden, x * Grid.colWidth + Grid.xBegin + (int)((Grid.colWidth - bitmap.Width) * 0.5),
                    y * Grid.colHeight + Grid.yBegin + (int)((Grid.colHeight - bitmap.Height) * 0.5));
        }
    }
}
