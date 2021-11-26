﻿using System.Collections.Generic;
using VehiclesAccounting.Core.ProjectAggregate;

namespace VehiclesAccounting.Web.ViewModels.Owners
{
    public class OwnerViewModel
    {
        public IEnumerable<Owner> Owners { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
