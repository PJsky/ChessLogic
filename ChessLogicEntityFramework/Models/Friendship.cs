using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ChessLogicEntityFramework.Models
{
    public class Friendship
    {
        public int User1ID { get; set; }
        public int User2ID { get; set; }
        public bool? isAccepted { get; set; }
    }

}
