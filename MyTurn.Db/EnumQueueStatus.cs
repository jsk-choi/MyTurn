namespace MyTurn.Db
{
    public enum EnumQueueStatus : int
    {
        InLine = 1,
        Bumped = 2,
        Done = 3,
        Cancelled = 4
    }
}
