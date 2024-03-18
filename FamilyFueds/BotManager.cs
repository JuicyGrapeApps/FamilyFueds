﻿/*
 * Copyright (c) 2024 JuicyGrape Apps.
 *
 * Licensed under the MIT License, (the "License");
 * you may not use any file by JuicyGrape Apps except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     https://www.juicygrapeapps.com/terms
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Diagnostics;

namespace JuicyGrapeApps.FamilyFueds
{
	public class BotManager : IDisposable
	{
		public BotManager()
		{
			ApplicationControl.Events.Birth += OnCreateBot;
            ApplicationControl.Events.Death += OnDestroyBot;
            ApplicationControl.Events.Garbage += Dispose;
        }

		/// <summary>
		/// Handles the Birth event to create a new bot. 
		/// </summary>
		/// <param name="person"></param>
		private void OnCreateBot(Person person)
		{
			if (ApplicationControl.family.Count < ApplicationControl.MAX_BOT_COUNT) 
				ApplicationControl.family.Add(person);
		}

        /// <summary>
        /// Handles the Death event to remove a bot. 
        /// </summary>
        /// <param name="person"></param>
        private void OnDestroyBot(Person person)
        {
            if (ApplicationControl.DEBUG_MODE) Debug.Print(person.fullname + " has gone to " + (person.location.Y < 0 ? "Heaven": "Hell"));
            person.Dispose();
            ApplicationControl.family.Remove(person);
            ApplicationControl.NumberOfPeople = ApplicationControl.family.Count;
        }

        /// <summary>
        /// Called on application shutdown. 
        /// </summary>
        /// <param name="person"></param>
        public void Dispose()
		{
            ApplicationControl.Events.Garbage -= Dispose;
            ApplicationControl.Events.Birth -= OnCreateBot;
            ApplicationControl.Events.Death -= OnDestroyBot;
        }
    }
}