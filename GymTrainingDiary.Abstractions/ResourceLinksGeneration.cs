using GymTrainingDiary.DTO;
using GymTrainingDiary.Utilities.HATEOAS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Utilities.Abstractions
{
    public static class ResourceLinksGeneration
    {
        public static List<Link> CreateLinksForResource(this LinkGenerator linkGenerator, 
            HttpContext context, 
            int resourceID, 
            Dictionary<string, string> methods)
        {
            return new List<Link>
            {
                new Link
                {
                    HttpMethod = "GET",
                    Description = "self",
                    Url = linkGenerator.GetUriByAction(context, methods[nameof(DefaultApiConventions.Get)], values: new { id = resourceID } )
                },

                new Link
                {
                    HttpMethod = "POST",
                    Description = "add",
                    Url = linkGenerator.GetUriByAction(context, methods[nameof(DefaultApiConventions.Post)])
                },

                new Link
                {
                    HttpMethod = "PUT",
                    Description = "update",
                    Url = linkGenerator.GetUriByAction(context, methods[nameof(DefaultApiConventions.Put)])
                },

                new Link
                {
                    HttpMethod = "DELETE",
                    Description = "delete",
                    Url = linkGenerator.GetUriByAction(context, methods[nameof(DefaultApiConventions.Delete)], values: new { id = resourceID })
                }
            };

        }
    }
}
