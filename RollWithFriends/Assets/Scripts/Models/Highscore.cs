using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class Highscore : BaseEntity
    {
        #region Fields and properties
        public User User { get; set; }

        public string LevelName { get; set; }

        public float Time { get; set; }
        #endregion

        public Highscore()
        {

        }

        public Highscore(
            string id,
            string levelName,
            float time,
            string unityCustomTokenAPI,
            User user)
        {
            Id = id;
            LevelName = levelName;
            Time = time;
            User = user;
            UnityCustomTokenAPI = unityCustomTokenAPI;
        }

        #region Public methods

        #endregion


        #region Private methods	


        #endregion

    }

}