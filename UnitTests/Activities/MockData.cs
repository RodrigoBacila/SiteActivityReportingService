using Bogus;
using Domain.Entities;
using Service.Activities.Arguments;
using System;

namespace UnitTests.Activities
{
    public static class MockData
    {
        private static readonly Faker faker = new Faker("pt_BR");

        public static GetTotalActivityDurationResponse GetTotalActivityDurationResponse()
        {
            return new GetTotalActivityDurationResponse()
            {
                Value = faker.Random.Int(min: 0)
            };
        }

        public static int AnyPositiveValue()
        {
            return faker.Random.Int(min: 0);
        }

        public static int AnyNegativeValue()
        {
            return faker.Random.Int(max: 0);
        }

        public static string RandomKey()
        {
            return faker.Random.String2(30);
        }

        public static Event RandomEvent()
        {
            return new Event(new Duration(faker.Random.Int(min: 0, max: 43200)), DateTime.Now);
        }
    }
}