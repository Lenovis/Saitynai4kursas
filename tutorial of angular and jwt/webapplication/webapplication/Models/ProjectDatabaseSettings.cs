﻿using System;
namespace webapplication.Models
{
    public class ProjectDatabaseSettings : IProjectDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string EventsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IProjectDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string EventsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
