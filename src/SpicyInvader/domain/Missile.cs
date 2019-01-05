﻿// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 04.01.2018

using SpicyInvader.domain;
using SpicyInvaders.domain.character;
using System;

namespace SpicyInvaders
{
    public class Missile : DisplayableObject
    {


        // Variable
        public const int SPEED = 1;             // Speed of the missile
        private Character owner;                // Owner of the missile (the one who shoot)
        public bool isMoving;
        

        public Missile(Character owner)
        {
            this.owner = owner;
            Color = ConsoleColor.Cyan;
            Drawing = "*";
        }

        /// <summary>
        /// Return the owner of the current missile
        /// </summary>
        /// <returns></returns>
        public Character GetOwner()
        {
            return this.owner;
        }

        public void Move(Direction direction)
        {
            int sens = 1;

            if (direction == Direction.Down)
            {
                Y += sens * SPEED;
            }
            else
            {
                Y -= sens * SPEED;
            }
        }
    }
}
