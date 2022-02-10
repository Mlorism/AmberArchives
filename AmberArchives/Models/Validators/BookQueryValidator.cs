using AmberArchives.Entities;
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
		private string[] allowedSortByColumnNames = { nameof(Book.OriginalTitle), nameof(Book.Author.LastName), nameof(Book.AverageRating), };
		public BookQueryValidator()
		{
			RuleFor(b => b.PageNumber).GreaterThanOrEqualTo(1);
			RuleFor(b => b.PageSize).Custom((value, context) =>
			{
				if (!allowedPageSizes.Contains(value))
				{
					context.AddFailure("PageSize", $"Page size must be in [{string.Join(",", allowedPageSizes)}]");
				}
			});
			RuleFor(b => b.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
				.WithMessage($"Sort by is optional or must be in [{string.Join(",", allowedSortByColumnNames)}]");
		}
	}
} 
