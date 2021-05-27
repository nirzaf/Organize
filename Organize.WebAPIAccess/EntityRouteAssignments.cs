using Organize.Shared.Enitites;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.WebAPIAccess
{
    class EntityRouteAssignments
    {
        public static Dictionary<Type, string> GetEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            {typeof(TextItem), "api/items?type="+(int)ItemTypeEnum.Text},
            {typeof(UrlItem), "api/items?type="+(int)ItemTypeEnum.Url},
            {typeof(ParentItem), "api/items?type="+(int)ItemTypeEnum.Parent},
            {typeof(ChildItem), "api/items?type="+(int)ItemTypeEnum.Child},
        };

        public static Dictionary<Type, string> PostEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            {typeof(TextItem), "api/items"},
            {typeof(UrlItem), "api/items"},
            {typeof(ParentItem), "api/items"},
            {typeof(ChildItem), "api/items"},
            {typeof(User), "api/users"},
        };

        public static Dictionary<Type, string> PutEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            {typeof(TextItem), "api/items"},
            {typeof(UrlItem), "api/items"},
            {typeof(ParentItem), "api/items"},
            {typeof(ChildItem), "api/items"}
        };

        public static Dictionary<Type, string> DeleteEntityRouteAssignment { get; } = new Dictionary<Type, string>
        {
            {typeof(TextItem), "api/items"},
            {typeof(UrlItem), "api/items"},
            {typeof(ParentItem), "api/items"},
            {typeof(ChildItem), "api/items"},
        };
    }
}
