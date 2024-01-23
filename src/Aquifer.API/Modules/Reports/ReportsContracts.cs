public record MonthlyAquiferiationStartsAndCompletionsResponse(
    List<StatusCountPerMonth> Starts,
    List<StatusCountPerMonth> Completions
);
public record StatusCountPerMonth
    (
        DateTime Date,
        int StatusCount
    );

