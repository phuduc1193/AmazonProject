namespace ProductProviders
{
    internal class ProductStoredProcedures
    {
        public static string Insert => "spInsertProduct";

        public static string Update => "spUpdateProduct";

        public static string Search => "sqSearchProduct";

        public static string FeaturedProduct => "sqGetFeaturedProduct";
    }
}