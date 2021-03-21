using CarvedRock.Api.Data.Entities;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class ProductReviewInputType : InputObjectGraphType<ProductReview>
    {
        public ProductReviewInputType()
        {
            Name = "reviewInput";
            Field(t => t.Title);
            Field(t => t.Review, nullable: true);
            Field(t => t.ProductId);
            //Field<NonNullGraphType<StringGraphType>>("title");
            //Field<StringGraphType>("review");
            //Field<NonNullGraphType<IntGraphType>>("productId");
        }
    }
}
