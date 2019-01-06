// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 05.01.2018

using SpicyInvaders.domain.character;

namespace SpicyInvader.presenters.listeners
{
    /// <summary>
    /// Observe the event of shooting
    /// </summary>
    public interface ShootListener
    {

        /// <summary>
        /// Callback to prevent that an invader has shoot
        /// </summary>
        /// <param name="invader"></param>
        void OnShoot();

        void TempRemoveMissile();

        void UpdateMenu();

        void UpdateShipPosition();

        void OnInvaderKilled(Invader invader);

        void GameOver();
    }
}
