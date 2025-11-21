using CubeCloud.Common.Constants;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace SaleAPI.Extensions
{
    public class TenantIdHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (string.Equals(context.ApiDescription.HttpMethod, HttpMethod.Post.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = QLSX.Shared.Contansts.RequestHeaders.XTenantId,
                    In = ParameterLocation.Header,
                    Required = true,
                    Example = new OpenApiString(QLSX.Shared.Contansts.TenantId)
                });
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = QLSX.Shared.Contansts.RequestHeaders.XUserId,
                    In = ParameterLocation.Header,
                    Required = true,
                    Example = new OpenApiString(QLSX.Shared.Contansts.UserId)
                });
            }
        }
    }
}
