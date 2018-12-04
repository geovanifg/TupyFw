namespace Tupy.Logger
{
    public enum ProviderTypes
    {
        TextFile,
        EventViewer,
        LocalDatabase,
        ServerDatabase
    }

    public enum EventEntryTypes
    {
        //
        // Summary:
        //     An error event. This indicates a significant problem the user should know about;
        //     usually a loss of functionality or data.
        Error = 1,
        //
        // Summary:
        //     A warning event. This indicates a problem that is not immediately significant,
        //     but that may signify conditions that could cause future problems.
        Warning = 2,
        //
        // Summary:
        //     An information event. This indicates a significant, successful operation.
        Information = 4
    }

    //public enum TimePeriodTypes
    //{
    //    Minute,
    //    Hour,
    //    Day
    //}
}
