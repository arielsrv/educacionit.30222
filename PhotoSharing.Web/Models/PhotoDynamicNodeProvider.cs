using MvcSiteMapProvider;
using System.Collections.Generic;

namespace PhotoSharing.Web.Models
{
    public class PhotoDynamicNodeProvider : DynamicNodeProviderBase
    {
        private PhotoSharingContext context = new PhotoSharingContext();

        /// <summary>
        /// Gets the dynamic node collection.
        /// </summary>
        /// <param name="node">The current node.</param>
        /// <returns>
        /// A dynamic node collection.
        /// </returns>
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
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