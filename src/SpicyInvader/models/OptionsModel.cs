// Author : Henoc Sese
// Description : An .NET Implementation of the Space Invader
// Lieu : ETML - Lausanne
// Date : 03.01.2019

using Newtonsoft.Json;
using SpicyInvader.data;
using SpicyInvader.presenters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader.models
{
    /// <summary>
    /// Model that will store user's preference for the game
    /// </summary>
    class OptionsModel : Model
    {

        public Presenter Presenter { get; set; }   // Reference of the Presenter

        // Variable
        public Level CurrentLevel { get; set; }                     // The current level choose by the player
        public bool IsSoundActivated { get; set;  }                  // Indicates the sound's mode

        public OptionsModel()
        {
            CurrentLevel = Level.PADAWAN;
            IsSoundActivated = false;
        }

        public void updateGameOptions(String soundMode, Level level)
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            JsonWriter writer = new JsonTextWriter(sw);
            
            writer.Formatting = Formatting.Indented;
           
            // Start writing the object
            writer.WriteStartObject();

            // Add key-value pair   (Sound)
            writer.WritePropertyName("Sound");
            writer.WriteValue(soundMode);

            // Add key-value pair   (Level)
            writer.WritePropertyName("Level");
            writer.WriteValue(level);

            // End writing the object
            writer.WriteEndObject();

            TextFileManager.Write("../../data/preferences.json", sb.ToString());
            Debug.WriteLine(sb.ToString());

            
            // Output (samples) :
            //  {
            //      "Sound": "ON",
            //      "Level": 1
            //  }
        }
    }
}
