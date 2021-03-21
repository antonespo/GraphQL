using CarvedRock.Api;
using CarvedRock.Api.Data.Entities;
using CarvedRock.Api.Repositories;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL
{
    public class CarvedRockMutation : ObjectGraphType
    {
        public CarvedRockMutation(ProductReviewRepository reviewRepository, ReviewMessageService messageService)
        {
            FieldAsync<ProductReviewType>(
                name: "createReview",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductReviewInputType>> { Name = "review" }),
                resolve: async context =>
                {
                    var review = context.GetArgument<ProductReview>("review");
                    await reviewRepository.AddReview(review);
                    messageService.AddReviewAddedMessage(review);
                    return review;
                }
                );
        }

    }
}
