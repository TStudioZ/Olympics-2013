using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Pexeso
{
    class Grid : IPaint
    {
        public const int SMALL = 8;
        public const int LARGE = 32;
        private int size;
        public const int colWidth = 60;
        public const int colHeight = 60;
        public const int xBegin = 70;
        public const int yBegin = 70;
        public int Size { get { return this.size; } }
        private Form1 form;
        private int numberOfShownCards = 0;
        private Card currentCard1;
        private Card currentCard2;

        private Card[,] cards;
        private List<Card> cardsList;
        private Bitmap[] bitmaps;
        private Bitmap bitmapHidden;
        private bool showUpwards;
        private bool waiting = false;
        private Counter counter;
        public Counter GameCounter { get { return this.counter; } }
        private bool hasWon;

        public Grid(Form1 form, int size, bool showUpwards)
        {
            this.form = form;
            this.counter = new Counter();
            this.showUpwards = showUpwards;
            if (size == Grid.SMALL)
            {
                this.size = Grid.SMALL;
                form.Size = new Size(400, 400);
                cardsList = new List<Card>();
                cards = new Card[4,4];
            }
            else if (size == Grid.LARGE)
            {
                this.size = Grid.LARGE;
                form.Size = new Size(650, 650);
                cardsList = new List<Card>();
                cards = new Card[8, 8];
            }
            LoadBitmaps();
            InitCards();
            if (showUpwards)
            {
                ShowAll();
            }
        }

        public bool HasWon()
        {
            bool won = true;
            for (int i = 0; i < cardsList.Count; i++)
            {
                if (!cardsList[i].Won)
                    won = false;
            }
            return won;
        }

        public void ShowAll()
        {
            waiting = true;
            for (int i = 0; i < cardsList.Count; i++)
            {
                cardsList[i].TurnUp();
            }
            form.EnableTimerShowingCards();
            form.Invalidate();
        }

        public void HideAll()
        {
            waiting = false;
            for (int i = 0; i < cardsList.Count; i++)
            {
                cardsList[i].TurnDown();
            }
            form.DisableTimerShowingCards();
            form.Invalidate();
        }

        public void HideNotWonCards()
        {
            for (int i = 0; i < cardsList.Count; i++)
            {
                if (!cardsList[i].Won)
                    cardsList[i].TurnDown();
            }
            numberOfShownCards = 0;
            form.DisableTimerHideNotWonCards();
            form.Invalidate();
        }

        public void MouseClick(int xForm, int yForm)
        {
            if (waiting || hasWon)
                return;
            int xGrid = (xForm - xBegin) / colWidth;
            int yGrid = (yForm - yBegin) / colHeight;
            if (xGrid >= 0 && xGrid < cards.GetLength(0) && yGrid >= 0 && yGrid < cards.GetLength(1) && numberOfShownCards < 2)
            {
                ++numberOfShownCards;
                if (numberOfShownCards == 1)
                    currentCard1 = cards[xGrid, yGrid];
                else if (numberOfShownCards == 2)
                {
                    currentCard2 = cards[xGrid, yGrid];
                    if (currentCard1.CompareTo(currentCard2))
                    {
                        currentCard1.Win();
                        currentCard2.Win();
                        counter.SuccessfulTurn(currentCard1.Uid, currentCard2.Uid);
                        hasWon = HasWon();
                        HideNotWonCards();
                    }
                    else
                    {
                        counter.Turn(currentCard1.Uid, currentCard2.Uid);
                        form.EnableTimerHideNotWonCards();
                    }
                }
                cards[xGrid, yGrid].TurnUp();
                form.Invalidate();
                if (hasWon)
                    form.ShowWonDialog(counter);
            }
        }

        public void LoadBitmaps()
        {
            bitmapHidden = BitmapsParser.LoadBitmap(@"obrazky\00.png.");
            bitmaps = new Bitmap[this.Size];
            for (int i = 0, bitmapNumber = 1; bitmapNumber < this.Size + 1; i++, bitmapNumber++)
			{
                if (bitmapNumber < 10)
                    bitmaps[i] = BitmapsParser.LoadBitmap(@"obrazky\0" + bitmapNumber.ToString() + ".png.");
                else
                    bitmaps[i] = BitmapsParser.LoadBitmap(@"obrazky\" + bitmapNumber.ToString() + ".png.");
			}
            for (int i = 0; i < bitmaps.Length; i++)
            {
                if (bitmaps[i] == null)
                {
                    MessageBox.Show(form, "Chyba při načítání zdrojových obrázků, aplikace bude ukončena.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        public void InitCards()
        {
            int uid = 0;
            int twinId = 0;
            int twins = 1;
            for (int x = 0; x < cards.GetLength(0); x++)
            {
                for (int y = 0; y < cards.GetLength(1); y++)
                {
                    cardsList.Add(new Card(x, y, uid, twinId, bitmaps[twinId], bitmapHidden));
                    if (twins == 2)
                    {
                        twinId++;
                        twins = 0;
                    }
                    twins++;
                    uid++;
                }
            }
            ShuffleCards();
            int n = 0;
            for (int x = 0; x < cards.GetLength(0); x++)
            {
                for (int y = 0; y < cards.GetLength(1); y++)
                {
                    cardsList[n].X = x;
                    cardsList[n].Y = y;
                    cards[x, y] = cardsList[n];
                    ++n;
                }
            }
        }

        public void ShuffleCards()
        {
            Random rnd = new Random();
            for (int i = cardsList.Count; i > 1; i--)
            {
                int pos = rnd.Next(i);
                var x = cardsList[i - 1];
                cardsList[i - 1] = cardsList[pos];
                cardsList[pos] = x;
            }
        }

        public void Paint(Graphics g)
        {
            for (int x = 0; x < cards.GetLength(0); x++)
            {
                for (int y = 0; y < cards.GetLength(1); y++)
                {
                    cards[x, y].Paint(g);
                }
            }
        }
    }
}
