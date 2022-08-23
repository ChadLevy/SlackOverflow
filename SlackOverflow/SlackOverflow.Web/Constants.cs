namespace SlackOverflow.Web
{
    public static class Constants
    {

        public static class StackOverflow
        {
            public const string BaseUrl = "https://api.stackexchange.com/2.3";
            
            public static class PresetRequests
            {
                /// <summary>
                /// Gets recent questions with the default question filter.
                /// </summary>
                public static string RecentQuestions => $"/questions?order=desc&sort=activity&site=stackoverflow&filter={RequestFilters.DefaultQuestion}";
            }

            /// <summary>
            /// 
            /// 
            /// See: https://api.stackexchange.com/docs/filters
            /// </summary>
            public static class RequestFilters
            {
                /// <summary>
                /// Returns a list of questions with answers with the minimal set of fields necessary.
                /// </summary>
                public const string DefaultQuestion = "!6CIAMFc5Awx7r*W.VU.*TAJA4KL(z_pvEydzY4Kb.Hcogx_*C6(*0mBlSg1";
            }
        }

    }
}
