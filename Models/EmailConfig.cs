﻿namespace SmartTicket.Models
{
    public class EmailConfig
    {
        public string From { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
