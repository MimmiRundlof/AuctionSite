using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackowskisApp.Constants
{
    public class ErrorConstants
    {

        public const string BidToLow = "Your bid has to be higher than current max bid.";

        public const string Error = "Something went wrong, try again.";

        public const string CantDeleteAuction = "You can't delete an auction with bids.";

        public const string InvalidLogin = "Invalid login attempt.";

        public const string NotAllowed = "You're not allowed to do this action.";
    }
}
