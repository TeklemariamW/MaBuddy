using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace mabuddy.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Nickname { get; set; }     //This will be entered when a new user signs up
        public int Age { get; set; }            
        public int Level { get; set; }           //Integer that new users choose after they sign up to decide what content they want to see 

    }
}