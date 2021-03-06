﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicEntityFramework.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<UserGames> UserGames { get; set; }

    }
}
