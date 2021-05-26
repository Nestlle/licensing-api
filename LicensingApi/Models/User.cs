using System.Collections.Generic;

namespace LicensingApi.Models
{
    public class User
    {
        //TODO: Do we need other fields from DB?
        public string Username{get;set;}
        public List<License> Licenses{get;set;}
    }
}