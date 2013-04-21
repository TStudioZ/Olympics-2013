using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pexeso
{
    public class Counter
    {
        private int numberOfTurns = 0;
        public int NumberOfTurns { get { return this.numberOfTurns; } }
        private int numberOfSuccessfulTurns = 0;
        public int NumberOfSuccessfulTurns { get { return this.numberOfSuccessfulTurns; } }
        private List<Turn> turns;
        public List<Turn> Turns { get { return this.turns; } }
        public Counter()
        {
            turns = new List<Turn>();
        }
        public void Turn(int card1Uid, int card2Uid)
        {
            this.numberOfTurns++;
            turns.Add(new Turn(card1Uid, card2Uid, false));
        }
        public void SuccessfulTurn(int card1Uid, int card2Uid)
        {
            this.numberOfTurns++;
            this.numberOfSuccessfulTurns++;
            turns.Add(new Turn(card1Uid, card2Uid, true));
        }
    }
}
