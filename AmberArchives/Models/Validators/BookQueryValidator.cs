using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models.Validators
{
    public class BookQueryValidator : AbstractValidator<BookQuery>
    {
		private int[] allowedPageSizes = new[] { 5, 10, 15 };
		public BookQueryValidator()
		{
			RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
			RuleFor(r => r.PageSize).Custom((value, context) =>
			{
				if (!allowedPageSizes.Contains(value))
				{
					context.AddFailure("PageSize", $"Page size must be in [{string.Join(",", allowedPageSizes)}]");
				}
			});
		}
    }
}
