using MvcSiteMapProvider.Extensibility;
using System.Collections.Generic;

namespace PhotoSharing.Web.Models
{
    public class PhotoDynamicNodeProvider : DynamicNodeProviderBase
    {
        private PhotoSharingContext context = new PhotoSharingContext();

        /// <summary>
        /// Gets the dynamic node collection.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection()
        {
            List<DynamicNode> returnList = new List<DynamicNode>();

            foreach (Photo item in context.Photos)
            {
                DynamicNode newNode = new DynamicNode();
                newNode.Title = item.Title;
                newNode.ParentKey = "AllPhotos";
                newNode.RouteValues.Add("id", item.Id);
                returnList.Add(newNode);
            }

            return returnList;
        }
    }
}