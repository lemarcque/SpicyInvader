// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2019


namespace SpicyInvader.views.utils
{

    /// <summary>
    /// Represent the position for when drawin the Cursor
    /// </summary>
    class CursorPosition
    {

        public int Left { get; }      // position from the left, horizontally
        public int Top { get; }       // position from the top, vertically

        public CursorPosition(int left, int top)
        {
            this.Left = left;
            this.Top = top;
        }
    }
}
