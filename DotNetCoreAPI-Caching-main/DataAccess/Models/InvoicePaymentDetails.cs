﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class InvoicePaymentDetails
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public int? PaymentModeId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string PaymentNo { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentBank { get; set; }

    public string Narration { get; set; }

    public decimal? Rate { get; set; }

    public int? AdvanceId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? Active { get; set; }

    public virtual InvoiceHeader Invoice { get; set; }

    public virtual PaymentMode PaymentMode { get; set; }
}