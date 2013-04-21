using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pexeso
{
    public class Turn
    {
        private int card1Uid;
        private int card2Uid;
        private bool successful;
        public Turn(int card1Uid, int card2Uid, bool successful)
        {
            this.card1Uid = card1Uid;
            this.card2Uid = card2Uid;
            this.successful = successful;
        }

        public override string ToString()
        {
            string returnString = card1Uid.ToString() + ", " + card2Uid.ToString();
            if (successful)
                returnString += " +";
            returnString += Environment.NewLine;
            return returnString;
        }
    }
}
