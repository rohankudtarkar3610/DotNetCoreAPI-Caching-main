﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class AdvanceMaster
{
    public int Id { get; set; }

    public int? EntityId { get; set; }

    public int? PatientId { get; set; }

    public int? InvoiceId { get; set; }

    public decimal? AdvanceAmount { get; set; }

    public DateTime? AdvanceGivenDate { get; set; }

    public bool? IsAdvanceUtilized { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? Active { get; set; }

    public virtual EntityMaster Entity { get; set; }

    public virtual PatientMaster Patient { get; set; }
}