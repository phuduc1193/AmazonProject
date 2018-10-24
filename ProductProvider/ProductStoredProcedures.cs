﻿namespace ProductProviders
{
    internal class ProductStoredProcedures
    {
        public static string Insert => "spInsertProduct";

        public static string Update => "spUpdateProduct";

        public static string Search => "spSearchProduct";

        public static string FeaturedProduct => "spGetFeaturedProduct";
    }
}