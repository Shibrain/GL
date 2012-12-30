using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public enum ChequeStatus
{
    NonDetermined = 0,
    UnderProcessing = 1,
    Processed = 2,
    RD = 3,
    Postponed = 4
};

public enum ChequeTarget
{
    NonDetermined = 0,
    OutgoingToSupplier = 1,
    IncomingFromCustomer = 2
};

public enum TransactionTypes
{
    NonDetermined = 0,
    Damaged = 1,
    SupplierReturn = 2,
    CustomerReturn = 3,
    StoreMovement = 4
};

