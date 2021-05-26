using System.Collections.Generic;

namespace LicensingApi.Models
{
    public class License
    {
        public string Application{get;set;}
        public string Key{get;set;}
        public int NumberOfUsers{get;set;}
        public List<string> Modules{get;set;}
        public string Type{get;set;}//TODO: Define type of licenses as enum in models.
    }
}