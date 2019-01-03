// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 03.01.2019

using SpicyInvader.models;
using SpicyInvader.presenters;
using SpicyInvader.views;

namespace SpicyInvader
{
    /// <summary>
    /// Create all dependency necessary for a "client" object
    /// </summary>
    class DependencyFactory
    {

        /// <summary>
        /// Return the appropriate Presenter to use for the Program object
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static Presenter getDependency(View view)
        {
            if (view is MenuView)
            {
                Model model = new MenuModel();
                return new MenuPresenter(view, model);
            }
            else if (view is OptionsView)
            {
                Model model = new OptionsModel();
                return new OptionsPresenter(view, model);
            }
            else if(view is HighscoresView)
            {
                return null;
            }
            else if(view is PlayView)
            {
                Model model = new PlayModel();
                return new PlayPresenter(view, model);
            }

            return null;
        }
    }
}
