using HT.Core.Shared.ValueObjects;

namespace HT.Core.Shared
{
    public static class Roles
    {
        public static Role DocumentWriter => new Role("HT DocumentWriter");
        public static Role Admin => new Role("HT Admin");
        public static Role User => new Role("Nutzer");
        public static Role ArticleWriter => new Role("HT ArticleWriter");
    }
}