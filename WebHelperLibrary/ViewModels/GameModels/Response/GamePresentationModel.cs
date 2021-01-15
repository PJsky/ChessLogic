using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedWebObjectsLibrary.ViewModels.GameModels.Response
{
    public class GamePresentationModel
    {
        public int GameID { get; set; }
        public string PlayerWhite { get; set; }
        public string PlayerBlack { get; set; }
    }
}
