using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.Model.Model
{
    
    public class UpdateItem
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public bool? IsRateApplicable { get; set; }

        public decimal? Rate { get; set; }
    }
    public class UpdateItemValidator : AbstractValidator<UpdateItem>
    {
        public UpdateItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id not empty");
            RuleFor(x => x.ItemName).NotEmpty().WithMessage("ItemName not empty");
            RuleFor(x => x.IsRateApplicable).NotEmpty().WithMessage("ItemName not empty");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("rate is not empty");
        }
    }
}
