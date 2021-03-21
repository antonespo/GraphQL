using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.Repositories;
using GraphQL.DataLoader;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;
using System.Security.Claims;

namespace CarvedRock.Api.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepo, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name).Description("The name of the product");
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypeEnumType>("Type", "The type of product");
            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                // Con Data Loader: approccio performante
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    //..

                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                        "GetReviewByProductId", reviewRepo.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
