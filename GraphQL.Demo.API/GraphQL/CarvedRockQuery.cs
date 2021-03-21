using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository productRepository, ProductReviewRepository reviewRepository)
        {
            Field<ListGraphType<ProductType>>(
                name: "products",
                resolve: context => productRepository.GetAll()
            );

            Field<ProductType>(
                name: "product",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return productRepository.GetProductById(id);
                }
            );

            Field<ListGraphType<ProductReviewType>>(
                name: "reviews",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("productId");
                    return reviewRepository.GetForProduct(id);
                }
                );
        }
    }
}
