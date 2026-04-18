namespace PAS.Web.Models;

public enum ProposalStatus
{
    Pending = 0,
    UnderReview = 1,
    Matched = 2,
    Withdrawn = 3
}

public enum MatchStatus
{
    Interested = 0,
    Confirmed = 1,
    Reassigned = 2
}