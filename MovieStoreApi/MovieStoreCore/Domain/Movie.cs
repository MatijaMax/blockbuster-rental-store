﻿using MovieStoreCore.Domain.Enums;

namespace MovieStoreApi.Domain
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public LicensingType LicensingType { get; set; }
    }
}