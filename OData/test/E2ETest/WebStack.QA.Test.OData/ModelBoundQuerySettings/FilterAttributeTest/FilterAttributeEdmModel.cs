﻿using System.Collections.Generic;
using System.Web.OData.Builder;
using System.Web.OData.Query;
using Microsoft.OData.Edm;

namespace WebStack.QA.Test.OData.ModelBoundQuerySettings.FilterAttributeTest
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Order AutoExpandOrder { get; set; }

        public Address Address { get; set; }

        public List<Order> Orders { get; set; }
    }

    [Filter("Id", Disabled = true)]
    [Filter]
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Car> Cars { get; set; }
    }

    [Filter("Id")]
    [Filter(Disabled = true)]
    [Filter("Name")]
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CarNumber { get; set; }
    }

    public class Address
    {
        public string Name { get; set; }

        public string Street { get; set; }
    }

    public class FilterAttributeEdmModel
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<Car>("Cars");
            IEdmModel model = builder.GetEdmModel();
            return model;
        }

        public static IEdmModel GetEdmModelByModelBoundAPI()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders").EntityType.Filter().Filter(QueryOptionSetting.Disabled, "Id");
            builder.EntitySet<Car>("Cars")
                .EntityType.Filter("Name")
                .Filter(QueryOptionSetting.Disabled)
                .Filter("Id");
            IEdmModel model = builder.GetEdmModel();
            return model;
        }
    }
}