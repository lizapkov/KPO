namespace HSEKAT.Models;

public class InspectionRes
{
    public bool Approval { get; }
    public string Reason { get; }

    public InspectionRes(bool approval, string reason)
    {
        Approval = approval;
        Reason = reason;
    }
}
