using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.Model.Model
{
    public class GetItemMasterModel
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public bool? IsRateApplicable { get; set; }

        public decimal? Rate { get; set; }
    }
    public class GetItemValidator : AbstractValidator<GetItemMasterModel>
    {
        public GetItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("id not empty");
            RuleFor(x => x.ItemName).NotEmpty().WithMessage("ItemName not empty");
            RuleFor(x => x.IsRateApplicable).NotEmpty().WithMessage("ItemName not empty");
        }
    }
    


}
